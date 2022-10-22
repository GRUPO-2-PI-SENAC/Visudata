using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Entities
{
    public class Log : EntityBase
    {
        public double Noise { get; set; }
        public double Temp { get; set; }
        public double Vibration { get; set; }
        public DateTime Time { get; set; }
        public Machine Machine { get; set; }
    }
}
