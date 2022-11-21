using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI.Domain.Entities
{
    [Table("us_problems_category")]
    public class UserProblemsCategory : EntityBase
    {
        [Required(ErrorMessage = "Required field")]
        public string Name { get; set; }
    }
}
