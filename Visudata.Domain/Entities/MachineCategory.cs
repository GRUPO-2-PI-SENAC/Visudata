using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI.Domain.Entities
{
    [Table("Machine_Category")]
    public class MachineCategory : EntityBase
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required enterprise for associate category")]
        public virtual Enterprise Enterprise { get; set; }
        public virtual IEnumerable<Machine> Machines { get; set; }
    }
}
