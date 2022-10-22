using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI.Domain.Entities;

[Table("Machine_status")]
public class MachineStatus
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [MinLength(5,ErrorMessage = "Invalid name length")]
    public string Name { get; set; }
}