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
        ErrorFunction errorFunction;
        public Layer(ActivationFunction activation, ErrorFunction error,  int neuronCount, Layer? previousLayer)
        {
            activationFunction = activation;
            errorFunction = error;
            Neurons = new Neurons[neuronCount];
            Outputs = new double[neuronCount];
            for (int i = 0; i < neuronCount; i++)
            {
                if (previousLayer != null)
                {
                    Neurons[i] = new Neurons(activationFunction, errorFunction, previousLayer.Neurons);
                }
                else
                {
                    Neurons[i] = new Neurons(activationFunction, errorFunction, null);
                }    
            }
        }

        public void Backpropagation(double learningRate)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Backpropagation(learningRate);
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
        public void ApplyUpdates()
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].ApplyUpdates();
            }
        }
    }
}
