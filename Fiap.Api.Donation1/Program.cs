using Fiap.Api.Donation1;
using Fiap.Api.Donation1.Data;
using Fiap.Api.Donation1.Repository;
using Fiap.Api.Donation1.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerUI;
using Fiap.Api.Donation1.ViewModel;
using Fiap.Api.Donation1.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



var connectionString = builder.Configuration.GetConnectionString("databaseUrl");
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging(true)
);

#region Injeção Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITipoProdutoRepository, TipoProdutoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ITrocaRepository, TrocaRepository>();
#endregion

#region automapper

var mapperConfig = new AutoMapper.MapperConfiguration(
        c =>
        {
            c.AllowNullDestinationValues = true;
            c.CreateMap<LoginRequestVM, UsuarioModel>();
            c.CreateMap<UsuarioModel, LoginResponseVM>();
            c.CreateMap<UsuarioModel, UsuarioResponseVM>();
            c.CreateMap<TrocaRequestVM, TrocaModel>();
            c.CreateMap<ProdutoModel, ProdutoResponseVM>();
            c.CreateMap<TrocaModel, TrocaResponseVM>();

        }
    );

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region autenticacao

bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
{
    if (expires != null)
    {
        return expires > DateTime.UtcNow;
    }
    return false;
}

//Chave privada
var key = Encoding.ASCII.GetBytes(Settings.SECRET_TOKEN);

builder.Services.AddAuthentication(a => {
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
    ).AddJwtBearer(options => {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            LifetimeValidator = CustomLifetimeValidator, //Forma de validar se o token está expirado
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = true
        };
    });

#endregion

#region versionamento
builder.Services.AddApiVersioning(option => {
    option.UseApiBehavior = true;
    option.ReportApiVersions = true;
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(3, 0);
    option.ApiVersionReader =
        ApiVersionReader.Combine(
            new HeaderApiVersionReader("x-api-version"),
            new QueryStringApiVersionReader(),
            new UrlSegmentApiVersionReader()
        );
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();

//Ajustando versionamento no Swagger
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        //builder.WithMethods("GET");
        //builder.WithOrigins("https://www.fiap.com.br");
        builder.AllowAnyOrigin();

    });
});

var app = builder.Build();

app.UseApiVersioning();

//Ajustando versionamento no Swagger
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //Ajustando versionamento no Swagger
    app.UseSwaggerUI(c =>
    {
        foreach (var d in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint(
                $"/swagger/{d.GroupName}/swagger.json",
                d.GroupName.ToUpperInvariant());
        }

        c.DocExpansion(DocExpansion.List);
    });
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors();  

app.UseAuthorization();

app.MapControllers();

app.Run();
