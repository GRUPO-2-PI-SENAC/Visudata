using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Machine
{
    public class RegisterMachineLogsViewModel
    {
        public double Vibration { get; set; }
        public double Noise { get; set; }
        public double Temp { get; set; }
        public string DateAsString { get; set; }
        public string HourAsString { get; set; }
    }
}
