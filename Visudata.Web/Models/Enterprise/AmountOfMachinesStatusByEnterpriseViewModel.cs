using PI.Domain.Entities;
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

        internal void ExtractDataFromMachines(List<Domain.Entities.Machine> machines)
        {
            this.AmountOfCriticalStateMachines = 0;
            this.AmountOfGoodStateMachines = 0;
            this.AmountOfWarningStateMachines = 0;

            foreach (Domain.Entities.Machine machineEntity in machines)
            {
                int initialvalue = this.AmountOfWarningStateMachines;
                foreach (Log log in machineEntity.Logs.Where(log => log.Created_at.Hour >= DateTime.Now.Hour - 6).ToList())
                {

                    Domain.Entities.Machine machine = log.Machine;

                    if (machine.NoiseMax < log.Noise || machine.NoiseMin > log.Noise)
                    {
                        this.AmountOfWarningStateMachines++;
                    }
                    else
                    {
                        if (machine.TempMax < log.Temp || machine.TempMin > log.Temp)
                        {
                            this.AmountOfWarningStateMachines++;
                        }
                        else
                        {
                            if (machine.VibrationMax < log.Vibration || machine.VibrationMin > log.Temp)
                            {
                                this.AmountOfWarningStateMachines++;
                            }
                            else
                            {
                                this.AmountOfGoodStateMachines++;
                            }
                        }
                    }
                }

                if (initialvalue + 3 <= this.AmountOfWarningStateMachines)
                    this.AmountOfCriticalStateMachines += 1;
            }
        }
    }
}
