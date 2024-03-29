﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI.Domain.Entities
{
    [Table("Machine_Category")]
    public class MachineCategory : EntityBase
    {
        public string Name { get; set; }
        public virtual IEnumerable<Machine> Machines { get; set; }
    }
}
