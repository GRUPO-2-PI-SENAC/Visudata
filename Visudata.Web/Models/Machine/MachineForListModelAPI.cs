using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Machine
{
    public class MachineForListModelAPI
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public double NoiseMax { get; set; }
        public double NoiseMin { get; set; }
        public double VibrationMax { get; set; }
        public double VibrationMin { get; set; }
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public string SerialNumber { get; set; }
        public string CategoryName { get; set; }
    }
}
