using System.ComponentModel.DataAnnotations;

namespace PI.Domain.ViewModel.Machine
{
    public class EditMachineViewModel
    {
        public int MachineId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [RegularExpression(@"^((?!^City$)[a-zA-Z '])+$", ErrorMessage = "Caracteres inválidos!")]
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
        public double MinTemp { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double MaxTemp { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double MinVibration { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double MaxVibration { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double MaxNoise { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public double MinNoise { get; set; }

    }
}
