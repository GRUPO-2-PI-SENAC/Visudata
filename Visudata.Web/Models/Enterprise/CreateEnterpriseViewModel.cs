using PI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PI.Web.ViewModel.Enterprise;

public class CreateEnterpriseViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Esse campo é obrigatório")]
    public string CNPJ { get; set; }
    public string SocialReason { get; set; }
    public string FantasyName { get; set; }
    [Required(ErrorMessage = "Esse campo é obrigatório")]
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "As senhas devem ser iguais !")]
    [Required(ErrorMessage = "Esse campo é obrigatório")]
    public string ConfirmPassword { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string NumberOfLocation { get; set; }
    public string Sector { get; set; }


    internal void ConvertToEntity(Domain.Entities.Enterprise entity)
    {
        entity.City = this.City;
        entity.SocialReason = this.SocialReason;
        entity.NumberOfLocation = this.NumberOfLocation;
        entity.Address = this.Address;
        entity.Cnpj = this.CNPJ;
        entity.FantasyName = this.FantasyName;
        entity.Sector = this.Sector;
        entity.State = this.State;
        entity.Created_at = DateTime.Now;
        entity.Password = this.Password;
    }
}