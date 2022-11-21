using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Entities
{
    public class Log 
    {
        [Key]
        [Required(ErrorMessage = "Required field")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Noise { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Temp { get; set; }
        [Required(ErrorMessage = "Required field")]
        public double Vibration { get; set; }
        [Required(ErrorMessage = "Required field")]
        public DateTime Created_at { get; set; }
        [Required(ErrorMessage = "Required field")]
        public Machine Machine { get; set; }
    }
}
