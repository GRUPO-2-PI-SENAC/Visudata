using System.ComponentModel.DataAnnotations;

namespace PI.Web.ViewModel.Machine
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

        public void ConvertToEntity(Domain.Entities.Machine machineEntity)
        {
            machineEntity.NoiseMax = this.MaxNoise;
            machineEntity.NoiseMin = this.MinNoise;
            machineEntity.Model = this.Model;
            machineEntity.Brand = this.Brand;
            machineEntity.VibrationMax = this.MaxVibration;
            machineEntity.VibrationMin = this.MinVibration;
            machineEntity.TempMax = this.MaxTemp;
            machineEntity.TempMin = this.MinTemp;
            machineEntity.Tag = this.Tag;
            machineEntity.SerialNumber = this.SerialNumber;
            machineEntity.Created_at = DateTime.Now;
        }
    }
}
