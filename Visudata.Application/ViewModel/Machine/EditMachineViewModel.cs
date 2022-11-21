using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Application.ViewModel.Machine
{
    public class EditMachineViewModel
    {
        public int MachineId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double VibrationMin { get; set; }
        public double VibrationMax { get; set; }
        public double NoiseMax { get; set; }
        public double NoiseMin { get; set; }


    }
}
