using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Entities
{
    public class Machine : EntityBase
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double TempMin { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double TempMax { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double NoiseMin { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double NoiseMax { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double VibrationMin { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double VibrationMax { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Tag { get; set; }

        #region Relationship
        public MachineCategory Category { get; set; }
        public MachineStatus Status { get; set; }
        public Enterprise Enterprise { get; set; }

        #endregion

    }
}
