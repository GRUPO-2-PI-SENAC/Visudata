using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}
