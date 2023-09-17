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

        public double WeightUpdate { get; set; } = 0;
        double previousWeightUpdate = 0;
        public void ApplyUpdates(double momentum)
        {
            WeightUpdate += previousWeightUpdate * momentum;
            weight += WeightUpdate;
            previousWeightUpdate = WeightUpdate;
            WeightUpdate = 0;
        }
    }
}
