using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation1.ViewModel
{
    public class LoginResponseVM
    {
        [Required(ErrorMessage = "Email do usuário é obrigatório!")]
        public string EmailUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public int UsuarioId { get; set; }
        public string Regra { get; set; }
        public string Token { get; set; }

        public LoginResponseVM() 
        { 
        }

        public LoginResponseVM(string emailUsuario, string nomeUsuario, int usuarioId, string regra, string token)
        {
            EmailUsuario = emailUsuario;
            NomeUsuario = nomeUsuario;
            UsuarioId = usuarioId;
            Regra = regra;
            Token = token;
        }

    }
}
