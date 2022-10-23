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
        [Required(ErrorMessage = "Required Field")]
        [Range(0, 10000)]
        public double Vibration { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Temp { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Noise { get; set; }
        public Log Log { get; set; }
        public Machine Machine { get; set; }
        [Required(ErrorMessage = "Required field")]
        public DateTime Time { get; set; }

        public OutlierRegister()
        {
            Time = DateTime.Now;
        }
    }
}
