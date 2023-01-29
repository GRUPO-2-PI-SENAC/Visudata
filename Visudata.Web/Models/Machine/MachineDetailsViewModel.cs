using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Machine
{
    public class MachineDetailsViewModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public string Tag { get; set; }
        public string StatusName { get; set; }
        public double RealTimeTemp { get; set; }
        public string TempStyle { get; set; }
        public double RealTimeVibration { get; set; }
        public string VibrationStyle { get; set; }
        public double RealTimeNoise { get; set; }
        public string NoiseStyle { get; set; }
        public string StatusNameStyle { get; set; }

        public void ReceiveDataFromEntity(Domain.Entities.Machine machineEntity)
        {
            Domain.Entities.Log currentLogOfMachine =
                machineEntity.Logs.OrderBy(log => log.Created_at)
                .First();

            this.Id = machineEntity.Id;
            this.Model = machineEntity.Model;
            this.Category = machineEntity.Category.Name;
            this.Brand = machineEntity.Brand;
            this.SerialNumber = machineEntity.SerialNumber;
            this.Tag = machineEntity.Tag;
            this.StatusName = machineEntity.Status.ToString();


            if (currentLogOfMachine == null)
            {
                this.RealTimeNoise = 0;
                this.NoiseStyle = "badge text-bg-secondary";
                this.RealTimeTemp = 0;
                this.VibrationStyle = "badge text-bg-secondary";
                this.RealTimeVibration = 0;
                this.TempStyle = "badge text-bg-secondary";
            }
            else
            {
                if ((currentLogOfMachine.Noise > machineEntity.NoiseMax * 0.8 && currentLogOfMachine.Noise < machineEntity.NoiseMax)
                    || (currentLogOfMachine.Noise < machineEntity.NoiseMin * 1.3 && currentLogOfMachine.Noise > machineEntity.NoiseMin))
                {
                    this.NoiseStyle = "badge text-bg-warning";
                }
                else
                {
                    if (currentLogOfMachine.Noise >= machineEntity.NoiseMax ||
                        currentLogOfMachine.Noise <= machineEntity.NoiseMin)
                    {
                        this.NoiseStyle = "badge text-bg-danger";
                    }
                    else
                    {
                        this.NoiseStyle = "badge text-bg-success";
                    }
                }

                if ((currentLogOfMachine.Vibration > machineEntity.VibrationMax * 0.8 && currentLogOfMachine.Vibration < machineEntity.VibrationMax)
                   || (currentLogOfMachine.Vibration < machineEntity.VibrationMin * 1.3 && currentLogOfMachine.Vibration > machineEntity.VibrationMin))
                {
                    this.VibrationStyle = "badge text-bg-warning";
                }
                else
                {
                    if (currentLogOfMachine.Vibration >= machineEntity.VibrationMax ||
                        currentLogOfMachine.Vibration <= machineEntity.VibrationMin)
                    {
                        this.VibrationStyle = "badge text-bg-danger";
                    }
                    else
                    {
                        this.VibrationStyle = "badge text-bg-success";
                    }
                }

                if ((currentLogOfMachine.Temp > machineEntity.TempMax * 0.8 && currentLogOfMachine.Temp < machineEntity.TempMax)
                   || (currentLogOfMachine.Temp < machineEntity.TempMin * 1.3 && currentLogOfMachine.Temp > machineEntity.TempMin))
                {
                    this.TempStyle = "badge text-bg-warning";
                }
                else
                {
                    if (currentLogOfMachine.Temp >= machineEntity.TempMax ||
                        currentLogOfMachine.Temp <= machineEntity.TempMin)
                    {
                        this.TempStyle = "badge text-bg-danger";
                    }
                    else
                    {
                        this.TempStyle = "badge text-bg-success";
                    }
                }
                this.RealTimeNoise = currentLogOfMachine.Noise;
                this.RealTimeTemp = currentLogOfMachine.Temp;
                this.RealTimeVibration = currentLogOfMachine.Vibration;
            }

        }
    }

}
