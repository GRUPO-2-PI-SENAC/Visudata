using System.ComponentModel.DataAnnotations;

namespace PI.Domain.ViewModel.Machine;

public class AddMachineViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Caracteres especieis não são permitidos")]
    public string Model { get; set; }
    [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Caracteres especieis não são permitidos")]
    [Required(ErrorMessage = "Campo obrigatório")]
    public string SerialNumber { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MaxTemp { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MinTemp { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MaxNoise { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MinNoise { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MaxVibration { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double MinVibration { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Category { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Caracteres especieis não são permitidos")]
    public string Tag { get; set; }
    public string Brand { get; set; }
    public string Status { get; set; }

    public AddMachineViewModel()
    {
        Id = new Random().Next(1000, 100000);
    }

}