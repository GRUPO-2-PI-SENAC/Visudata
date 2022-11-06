using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Entities
{
    [Table("us_problems_category")]
    public class UserProblemsCategory : EntityBase
    {
        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }
    }
}
