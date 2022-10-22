using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Entities
{
    public class UserSupport : EntityBase
    {
        public Enterprise Enterprise { get; set; }
        public string Description { get; set; }
        public string RepresentativoOfEnterpriseAddress { get; set; }
        public ProblemsCategory ProblemsCategory { get; set; }
    }
}
