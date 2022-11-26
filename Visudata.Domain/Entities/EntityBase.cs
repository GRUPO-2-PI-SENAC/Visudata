using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}
