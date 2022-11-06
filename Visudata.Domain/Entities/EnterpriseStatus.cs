using System.ComponentModel.DataAnnotations.Schema;

namespace PI.Domain.Entities;

[Table("Enterprise_status")]
public class EnterpriseStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
}