using System.ComponentModel.DataAnnotations;

namespace PI.Domain.ViewModel.UserSupport;

public class AddUserSupportViewModel
{
    public int EnterpriseId { get; set; }
    [Required(ErrorMessage = "Campo obrigatori")]
    public string NameOfEnterprise { get; set; }
    [Required(ErrorMessage = "Campo obrigatorio")]
    [EmailAddress(ErrorMessage = "Endereço de email inválido")]
    public string RepresentativeEmailAddress { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public string ProblemsCategoryName { get; set; }
    [Required(ErrorMessage = "Campo obrigatorio")]
    [MaxLength(400, ErrorMessage = "Limite de caracteres ultrapassado")]
    public string ProblemDescription { get; set; }
    public DateTime Created_at = DateTime.Now;

}