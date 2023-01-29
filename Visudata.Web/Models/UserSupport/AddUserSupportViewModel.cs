using PI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PI.Web.ViewModel.UserSupport;

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


    internal void ConvertToEntity(Domain.Entities.UserSupport entity)
    {
        entity.Enterprise = new Domain.Entities.Enterprise();
        entity.UserProblemsCategory = new UserProblemsCategory();

        entity.Enterprise.Id = this.EnterpriseId;
        entity.AddressEmailOfRepresentativeEmployee = this.RepresentativeEmailAddress;
        entity.UserProblemsCategory.Name = this.ProblemsCategoryName;
        entity.Description = this.ProblemDescription;
    }

}