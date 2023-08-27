using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    public abstract class DendriteBase
    {
        public abstract double Compute();
        public double weight { get; set; }

        public double WeightUpdate { get; set; }
        public void ApplyUpdates()
        {
            weight += WeightUpdate;
            WeightUpdate = 0;
        }
    }
}
