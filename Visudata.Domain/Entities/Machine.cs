using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Entities
{
    public class Machine : EntityBase
    {
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double NoiseMin { get; set; }
        public double NoiseMax { get; set; }
        public double VibrationMin { get; set; }
        public double VibrationMax { get; set; }
        public string Tag { get; set; }

        #region Relationship
        public virtual MachineCategory Category { get; set; }
        public virtual MachineStatus Status { get; set; }
        public virtual Enterprise Enterprise { get; set; }
        public virtual List<Log> Logs { get; set; }

        #endregion

    }
}
