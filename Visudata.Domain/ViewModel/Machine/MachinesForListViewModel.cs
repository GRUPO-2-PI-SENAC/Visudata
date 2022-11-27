namespace PI.Domain.ViewModel.Machine;

public class MachineForListViewModel
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public string SerialNumber { get; set; }
    public double Noise { get; set; }
    public string NoiseStyle { get; set; }
    public double Vibration { get; set; }
    public string VibrationStyle { get; set; }
    public double Temp { get; set; }
    public string TempStyle { get; set; }
    public string Status { get; set; }
    public string category { get; set; }
}