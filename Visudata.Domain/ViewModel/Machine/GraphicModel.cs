using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.ViewModel.Machine
{
    public class GraphicModel
    {
        public List<GraphicValues> GraphicValues { get; set; }
    }

    public class GraphicValues
    {
        public int Hour { get; set; }
        public double Value { get; set; }
    }

}
