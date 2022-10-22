using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Entities
{
    [Table("Machine_category")]
    public class MachineCategory : EntityBase
    {
        public string Name { get; set; }
        public IEnumerable<Enterprise> Enterprises {get; set;}

    }
}
