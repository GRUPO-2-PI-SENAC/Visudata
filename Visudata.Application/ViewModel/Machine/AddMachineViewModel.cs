using System.ComponentModel.DataAnnotations;

namespace PI.Application.ViewModel.Machine;

public class AddMachineViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [MaxLength(30, ErrorMessage = "Você ultrapassou a quantidade máxima de caracteres")]
    public string Model { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [MaxLength(80, ErrorMessage = "Você ultrapassou a quantidade máxima de caracteres")]
    public string SerialNumber { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MaxTemp { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Não é permitido valores negativos")]
    public double MimTemp { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MaxNoise { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [Range(1 , int.MaxValue, ErrorMessage = "Não é permitido valores negativos")]
    public double MimNoise { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MaxVibration { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "Não é permitido valores negativos")]
    public double MimVibration { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Category { get; set; }

    public string Brand { get; set; }
    public string Status { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [MaxLength(30, ErrorMessage = "Você ultrapassou a quantidade máxima de caracteres")]
    public string Tag { get; set; }

    public AddMachineViewModel()
    {
        Id = new Random().Next(1000, 100000);
    }
    
}