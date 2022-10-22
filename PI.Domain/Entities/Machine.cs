using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PI.Domain.Entities
{
    public class Machine : EntityBase
    {
        public string SerialNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public double TempMim { get; set; }
        public double TempMax { get; set; }
        public double NoiseMin { get; set; }
        public double NoiseMax { get; set; }
        public double VibrationMin { get; set; }
        public double VibrationMax { get; set; }
        public MachineStatus Status { get; set; }
        public MachineCategory Category { get; set; }
        public Enterprise Enterprise { get; set; }
        public string Location { get; set; }

    }
}
