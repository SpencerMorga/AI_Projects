using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    public class InputDendrite : DendriteBase
    {
        public double weight { get; set; }
        public double Input { get; set; }

        public override double Compute()
        {
            return Input * weight;
        }
    }
}
