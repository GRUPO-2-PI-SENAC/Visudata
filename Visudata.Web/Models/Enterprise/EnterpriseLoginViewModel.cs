using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PI.Web.ViewModel.Enterprise;

public class EnterpriseLoginViewModel
{
    [Required(ErrorMessage = "Campo obrigatório")]
    [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Caracteres especieis não são permitidos")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [PasswordPropertyText]
    public string Password { get; set; }
}