using Microsoft.CodeAnalysis.CSharp.Syntax;
using PI.Domain.Entities;
using PI.Web.ViewModel.Machine;
using System.Reflection.PortableExecutable;

namespace PI.Web.Util
{
    public class HelperFunction
    {
        public static MachineForListViewModel ConvertMachineToListViewModel(Domain.Entities.Machine machineEntity)
        {
            double lastNoise = machineEntity.Logs.OrderBy(log => log.Created_at).First().Noise;
            double lastTemp = machineEntity.Logs.OrderBy(log => log.Created_at).First().Temp;
            double lastVibration = machineEntity.Logs.OrderBy(log => log.Created_at).First().Vibration;

            MachineForListViewModel model = new MachineForListViewModel()
            {
                Id = machineEntity.Id,
                Model = machineEntity.Model,
                Brand = machineEntity.Brand,
                SerialNumber = machineEntity.SerialNumber,
                Noise = lastNoise,
                Vibration = lastVibration,
                Temp = lastTemp,
                category = machineEntity.Category.Name,
                Status = GetMachineStatusAsString(machineEntity.Status),
            };
            if (machineEntity.Logs.Count > 0)
            {
                Log lastLogOfMachine = machineEntity.Logs.MaxBy(log => log.Created_at);

                model.Noise = lastLogOfMachine.Noise;
                model.Vibration = lastLogOfMachine.Vibration;
                model.Temp = lastLogOfMachine.Temp;

                int amountOfVibrationOccurrences = machineEntity.Logs.Where(log => log.Created_at.Hour > DateTime.Now.Hour - 6).Count(log => log.Vibration < machineEntity.VibrationMin
                || log.Vibration > machineEntity.VibrationMax);
                int amountOfTempOccurrences = machineEntity.Logs.Where(log => log.Created_at.Hour > DateTime.Now.Hour - 6).Count(log => log.Temp < machineEntity.TempMin
                || log.Temp > machineEntity.TempMax);

                int amountOfNoiseOccurrences = machineEntity.Logs.Where(log => log.Created_at.Hour > DateTime.Now.Hour - 6).Count(log => log.Noise < machineEntity.NoiseMin
                || log.Noise > machineEntity.NoiseMax);

                if (amountOfVibrationOccurrences >= 3)
                {
                    model.VibrationStyle = "danger";
                }
                else
                {
                    if (amountOfNoiseOccurrences < 3 && amountOfNoiseOccurrences > 0)
                    {
                        model.VibrationStyle = "warning";
                    }
                    else
                    {
                        model.VibrationStyle = "success";
                    }
                }

                if (amountOfTempOccurrences >= 3)
                {
                    model.TempStyle = "danger";
                }
                else
                {
                    if (amountOfTempOccurrences < 3 && amountOfTempOccurrences > 0)
                    {
                        model.TempStyle = "warning";
                    }
                    else
                    {
                        model.TempStyle = "success";
                    }
                }

                if (amountOfNoiseOccurrences >= 3)
                {
                    model.NoiseStyle = "danger";
                }
                else
                {
                    if (amountOfNoiseOccurrences < 3 && amountOfNoiseOccurrences > 0)
                    {
                        model.NoiseStyle = "warning";
                    }
                    else
                    {
                        model.NoiseStyle = "success";
                    }
                }

            }
            return model;
        }


        private static string GetMachineStatusAsString(MachineStatus machineStatus)
        {
            switch (machineStatus)
            {
                case MachineStatus.Good:
                    return "Bom";
                case MachineStatus.Warning:
                    return "Cuidado";
                case MachineStatus.Critical:
                    return "Critico";
                default:
                    return "Indeterminado";
            }
        }

    }
}
