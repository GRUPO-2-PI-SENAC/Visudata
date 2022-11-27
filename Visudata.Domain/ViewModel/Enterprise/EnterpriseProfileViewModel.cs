using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.ViewModel.Enterprise
{
    public class EnterpriseProfileViewModel
    {
        public int Id { get; set; }
        public string FantasyName { get; set; }
        public string Sector { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
