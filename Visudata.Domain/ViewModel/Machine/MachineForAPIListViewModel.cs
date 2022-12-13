using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.ViewModel.Machine
{
    public class MachineForAPIListViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string CategoryName { get; set; }
        public double TempAvg { get; set; }
        public double VibrationAvg { get; set; }
        public double NoiseAvg { get; set; }
    }
}
