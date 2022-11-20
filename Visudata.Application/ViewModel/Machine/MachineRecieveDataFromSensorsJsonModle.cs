using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Application.ViewModel.Machine
{
    public class MachineDataRecieveFromSensorsJsonModel
    {
        public int MachineId { get; set; }
        public double Temp { get; set; }
        public double Noise { get; set; }
        public double Vibration { get; set; }

    }
}
