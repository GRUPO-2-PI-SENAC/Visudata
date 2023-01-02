namespace PI.Web.ViewModel.Machine
{
    public class MachineDataRecieveFromSensorsJsonModel
    {
        public int MachineId { get; set; }
        public double Temp { get; set; }
        public double Noise { get; set; }
        public double Vibration { get; set; }

    }
}
