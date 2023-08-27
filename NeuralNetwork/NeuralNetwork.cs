using Microsoft.VisualBasic;
using NeuralNetIntro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    public class NeuralNetwork
    {

        public Layer[] layers { get; set; }
        public ErrorFunction errorFunc { get; set; }
        public ActivationFunction activationFunc { get; set; }
        public NeuralNetwork(ActivationFunction activation, /* ErrorFunction errorFunc, */
        int[] neuronsPerLayer)
        {
            activationFunc = activation;
           // this.errorFunc = errorFunc;
            layers = new Layer[neuronsPerLayer.Length];

            for (int i = 0; i < neuronsPerLayer.Length; i++)
            {
                if (i > 0) layers[i] = new Layer(activation, neuronsPerLayer[i], layers[i - 1]);
                else layers[i] = new Layer(activation, neuronsPerLayer[i], null);

            }
        }
        public void Randomize(Random random, double min, double max)
        {
            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].Randomize(random, min, max);
            }
        }
        //public double[] Compute(double[] inputs)
        //{

        //    for (int i = 0; i < layers[0].Neurons.Length; i++)
        //    {
        //        Neurons neuron = layers[0].Neurons[i];
        //        for (int j = 0; j < neuron.dendrites.Length; j++)
        //        {
        //           neuron.Output = inputs[i];
        //        }
        //    }

        //    for (int i = 1; i < layers.Length; i++)
        //    {
        //        layers[i].Compute();
        //    }

        //    return layers[layers.Length - 1].Outputs;
        //}

        public double[] Compute(double[] inputs)
        {
            if (inputs == null || inputs.Length != layers[0].Neurons.Length) { throw new Exception("Inputs must be the same length as input count"); }
            for (int i = 0; i < layers[0].Neurons.Length; i++)
            {
                layers[0].Neurons[i].Output = inputs[i];
            }
            double[] outputs = null;
            for (int i = 1; i < layers.Length; i++)
            {
                outputs = layers[i].Compute();
            }

            return outputs;
        }

        public void ApplyUpdates()
        {
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].ApplyUpdates();
            }
        }

        
        //public double GetError(double[] inputs, double[] desiredOutputs)
        //{
        //    double total = 0;
        //    for (int i = 0; i < inputs.Length; i++)
        //    {
        //        total += errorFunc.Function(Compute(inputs)[i], desiredOutputs[i]);
        //    }
        //     return total / inputs.Length;

        //}


    }
}
