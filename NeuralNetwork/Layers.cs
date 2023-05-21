using NeuralNetIntro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    public class Layer
    {
        public Neurons[] Neurons { get; }
        public double[] Outputs { get; }
        ActivationFunction activationFunction;

        public Layer(ActivationFunction activation, int neuronCount, Layer? previousLayer)
        {
            activationFunction = activation;
            Neurons = new Neurons[neuronCount];
            
            for (int i = 0; i < neuronCount; i++)
            {
                if (previousLayer != null)
                {
                    Neurons[i] = new Neurons(activation, previousLayer.Neurons);
                }
                else
                {
                    Neurons[i] = new Neurons(activationFunction, null);
                }
                
            }
            
            
            
        }
        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Randomize(random, min, max);
            }
        }
        public double[] Compute()
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Outputs[i] = Neurons[i].Compute();
            }
            return Outputs;
        }
    }
}
