using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NeuralNetIntro;

namespace NeuralNetworks
{
    public class Neurons
    {
        public double bias;
        public DendriteBase[] dendrites;
        public double Output { get; set; }

        public double Input { get; set; }
        
        public ActivationFunction activationFunction { get; set; }

        public Neurons(ActivationFunction activationFunction, Neurons[]? previousNeurons)
        {
            this.activationFunction = activationFunction;
            
            if (previousNeurons == null)
            {
                dendrites = new DendriteBase[1];
                dendrites[0] = new InputDendrite();
            }    
            else
            {
                dendrites = new DendriteBase[previousNeurons.Length];
                for (int i = 0; i < previousNeurons.Length; i++)
                {
                    dendrites[i] = new Dendrites(previousNeurons[i]);
                }
            }
        }

        public void Randomize(Random random, double max, double min)
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                double value = random.NextDouble();
                dendrites[i].weight = min + (value * (max - min));
            }
            bias = min + (random.NextDouble() * (max - min));
        }

        public double Compute()
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                Input += dendrites[i].Compute();
            }
            Input += bias;

            Output = activationFunction.Function(Input);
            return Output;
        }
    }
}
