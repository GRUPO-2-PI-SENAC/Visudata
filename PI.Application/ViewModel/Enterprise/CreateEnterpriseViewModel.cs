using System.ComponentModel.DataAnnotations;

namespace PI.Application.ViewModel.Enterprise;

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

    public CreateEnterpriseViewModel()
    {
        Id = new Random().Next(10000, 1000000);
    }
}