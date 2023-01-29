using PI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PI.Web.ViewModel.Machine;

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

    public AddMachineViewModel()
    {
        Id = new Random().Next(1000, 100000);
    }

    public void ConvertToEntity(Domain.Entities.Machine machineEntity)
    {
        machineEntity.NoiseMax = this.MaxNoise;
        machineEntity.NoiseMin = this.MinNoise;
        machineEntity.Model = this.Model;
        machineEntity.Brand = this.Brand;
        machineEntity.VibrationMax = this.MaxVibration;
        machineEntity.VibrationMin = this.MinVibration;
        machineEntity.TempMax = this.MaxTemp;
        machineEntity.TempMin = this.MinTemp;
        machineEntity.Tag = this.Tag;
        machineEntity.SerialNumber = this.SerialNumber;
        machineEntity.Created_at = DateTime.Now;
    }

}