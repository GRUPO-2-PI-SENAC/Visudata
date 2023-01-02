using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Enterprise
{
    public class AmountOfMachinesStatusByEnterpriseViewModel
    {
        public int AmountOfGoodStateMachines { get; set; }
        public int AmountOfWarningStateMachines { get; set; }
        public int AmountOfCriticalStateMachines { get; set; }
        public int AmountOfMachines { get; set; }
    }
}
