using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PI.Application.ViewModel.Enterprise;

public class EnterpriseLoginViewModel
{
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Login { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Password { get; set; }
}