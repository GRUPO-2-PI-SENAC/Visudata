using System.ComponentModel.DataAnnotations;

namespace PI.Domain.ViewModel.Machine
{
    public class EditMachineViewModel
    {
        public int MachineId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Tag { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double TempMin { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double TempMax { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double VibrationMin { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double VibrationMax { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double NoiseMax { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double NoiseMin { get; set; }

    }
}
