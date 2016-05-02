using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Reuse2.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Lembrar este navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ser ao menos de {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Endereço")]
        public string endereco { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "CEP")]
        public string cep { get; set; }
        [Display(Name = "Bairro")]
        public string bairro { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Cidade")]
        public string cidade { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Estado")]
        public string estado { get; set; }
        [Display(Name = "Telefone")]
        public string telefone { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Display(Name = "Celular")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Itens Doados")]
        public int itensDoados { get; set; }
        [Display(Name = "Itens Pedidos")]
        public int itensPedidos { get; set; }
        [Display(Name = "Avatar")]
        public HttpPostedFileBase File { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ser ao menos de {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        public string UserId { get; set; }
    }
}

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "E-mail")]
    public string Email { get; set; }
}
