using System.ComponentModel.DataAnnotations;

namespace PI.Domain.ViewModel.Machine;

public class AddMachineViewModel
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public double MaxTemp { get; set; }
    public double MinTemp { get; set; }
    public double MaxNoise { get; set; }
    public double MinNoise { get; set; }
    public double MaxVibration { get; set; }
    public double MinVibration { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public string Status { get; set; }
    public string Tag { get; set; }

    public AddMachineViewModel()
    {
        Id = new Random().Next(1000, 100000);
    }

}