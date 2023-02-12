using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Dendrites : DendriteBase
    {
        public Neurons Previous { get; set; }
        public double weight {  get; set; }

        public Dendrites(Neurons previous)
        {
            Previous = previous;
        }

        public override double Compute()
        {
            return Previous.Output * weight;
        }
    }
}
