using System.ComponentModel.DataAnnotations;

namespace Fiap.Api.Donation1.ViewModel
{
    public class LoginRequestVM
    {
        [Required(ErrorMessage = "Email do usuário é obrigatório!")]
        public string EmailUsuario { get; set; }
        [Required(ErrorMessage = "A senha do usuário é obrigatória!")]
        public string Senha { get; set; }

        public LoginRequestVM()
        {

        }

    }
}
