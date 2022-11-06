namespace PI.Application.ViewModel.Machine;

public class AddMachineViewModel
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public double MaxTemp { get; set; }
    public double MimTemp { get; set; }
    public double MaxNoise { get; set; }
    public double MimNoise { get; set; }
    public double MaxVibration { get; set; }
    public double MimVibration { get; set; }
    public string Category { get; set; }
    public int EnterpriseId { get; set; }
    public string Brand { get; set; }
    public string Status { get; set; }
    public string Location { get; set; }

    public AddMachineViewModel()
    {
        Id = new Random().Next(1000, 100000);
    }
    
}