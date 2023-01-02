using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Machine
{
    public class EditMachineModel
    {
        public int MachineId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Category { get; set; }
        public string Tag { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public double MinVibration { get; set; }
        public double MaxVibration { get; set; }
        public double MaxNoise { get; set; }
        public double MinNoise { get; set; }
    }
}
