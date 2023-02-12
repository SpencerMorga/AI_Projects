using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NeuralNetIntro;

namespace NeuralNetwork
{
    public class Neurons
    {
        double bias;
        public DendriteBase[] dendrites;
        public double Output { get; set; }

        public double Input { get; set; }
        
        public ActivationFunction activationFunction { get; set; }

        public Neurons(ActivationFunction activationFunction, Neurons[]? previousNeurons)
        {
            this.activationFunction = activationFunction;
            dendrites = new Dendrites[previousNeurons.Length];
            for (int i = 0; i < previousNeurons.Length; i++)
            {
                if (previousNeurons != null)
                {
                    dendrites[i] = new InputDendrite();
                }
                else
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
