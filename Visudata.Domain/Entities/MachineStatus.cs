using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PI.Domain.Entities;

public enum MachineStatus
{
    Good = 0 ,
    Warning = 1 ,
    Critical = 2 ,
    Undefined = 3 ,
}