using System.ComponentModel.DataAnnotations;

namespace PI.Application.ViewModel.UserSupport;

public class AddUserSupportViewModel
{
    [Required(ErrorMessage = "Campo obrigatòrio")]
    public int EnterpriseId { get; set; }
    [Required(ErrorMessage = "Campo obrigatori")]
    public string NameOfEnterprise { get; set; }
    [Required(ErrorMessage = "Campo obrigatorio")]
    public string RepresentativeEmailAddress { get; set; }
    [Required(ErrorMessage = "Campo obrigatorio")]
    public int ProblemCategoryId { get; set; }
    [Required(ErrorMessage = "Campo obrigatorio")]
    public string ProblemDescription { get; set; }
    public DateTime Created_at = DateTime.Now;

}