using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.ViewModel.Machine
{
    public class RegisterMachineLogsViewModel
    {
        public double Vibration { get; set; }
        public double Noise { get; set; }
        public double Temp { get; set; }
        public DateTime Created_at { get; set; }
    }
}
