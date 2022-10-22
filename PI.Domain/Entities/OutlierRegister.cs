using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Entities
{
    public class OutlierRegister : EntityBase
    {
        [Required]
        [Range(0 , 10000)]
        public double Vibration { get; set; }
        [Required]
        public double Temp { get; set; }
        [Required]
        public double Noise { get; set; }
        public Log Log { get; set; }
        public Machine Machine { get; set; }
        public DateTime Time { get; set; }

        public OutlierRegister()
        {
            Time = DateTime.Now;
        }
    }
}
