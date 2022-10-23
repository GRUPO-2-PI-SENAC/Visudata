using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Entities
{
    public class UserSupport : EntityBase
    {
        [Required(ErrorMessage = "Need a enterprise for associate this problem ")]
        public Enterprise Enterprise { get; set; }
        [Required(ErrorMessage = "Required description !")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Email Address of representative employee is required field for sent email of soluction procedures")]
        public string AddressEmailOfRepresentativeEmployee { get; set; }
        [Required(ErrorMessage = "Need the type of problem !")]
        public ProblemsCategory ProblemsCategory { get; set; }
    }
}
