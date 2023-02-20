using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class GeneticLearning
    {
        NeuralNetwork net;
        Random random;
        double mutationRate;
        public GeneticLearning(NeuralNetwork net, double mutationRate, Random random=null)
        {
            this.net = net;
            this.random = random == null ? new Random() : random;
            this.mutationRate = mutationRate;
        }

        public void 
    }
}
