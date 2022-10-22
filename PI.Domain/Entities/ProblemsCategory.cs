using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Entities
{
    [Table("us_problems_category")]
    public class ProblemsCategory : EntityBase
    {
        public string Name { get; set; }
    }
}
