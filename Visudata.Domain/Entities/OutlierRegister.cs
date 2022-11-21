using System.ComponentModel.DataAnnotations;

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
