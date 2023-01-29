using Newtonsoft.Json;
using PI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Web.ViewModel.Machine
{
    public class GraphicModel
    {
        public List<GraphicValues> GraphicValues { get; set; }

        internal void GetDataFromMachine(string status, Domain.Entities.Machine machineEntity)
        {
            List<Log> lastSixLogsAboutMachine = machineEntity.Logs.OrderBy(log => log.Created_at).Take(6).ToList();

            this.GraphicValues = new List<GraphicValues>();

            Dictionary<int, double> hourWithValueOfMagnitude = new Dictionary<int, double>();

            foreach (Log log in lastSixLogsAboutMachine)
            {
                if (status == "temp")
                {
                    hourWithValueOfMagnitude.Add(log.Created_at.Hour, log.Temp);
                    this.GraphicValues.Add(new GraphicValues()
                    {
                        Hour = log.Created_at.Hour,
                        Value = log.Temp
                    });
                }
                else
                {
                    if (status == "vibration")
                    {
                        hourWithValueOfMagnitude.Add(log.Created_at.Hour, log.Vibration);
                        this.GraphicValues.Add(new GraphicValues()
                        {
                            Hour = log.Created_at.Hour,
                            Value = log.Vibration
                        });
                    }
                    else
                    {
                        hourWithValueOfMagnitude.Add(log.Created_at.Hour, log.Noise);
                        this.GraphicValues.Add(new GraphicValues()
                        {
                            Hour = log.Created_at.Hour,
                            Value = log.Noise
                        });
                    }
                }
            }

        }
    }

    public class GraphicValues
    {
        public int Hour { get; set; }
        public double Value { get; set; }
    }

}
