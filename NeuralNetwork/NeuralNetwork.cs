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
        public int OutputCount => layers[layers.Length - 1].Neurons.Length;
        public NeuralNetwork(ActivationFunction activation, ErrorFunction error, int[] neuronsPerLayer)
        {
            activationFunc = activation;
            errorFunc = error;
            layers = new Layer[neuronsPerLayer.Length];

            for (int i = 0; i < neuronsPerLayer.Length; i++)
            {
                if (i > 0) layers[i] = new Layer(activationFunc, errorFunc, neuronsPerLayer[i], layers[i - 1]);
                else layers[i] = new Layer(activationFunc, errorFunc, neuronsPerLayer[i], null);

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

        public void ApplyUpdates(double momentum)
        {
            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].ApplyUpdates(momentum);
            }
        }

        public void Backpropagation(double learningRate, double[] desiredOutputs)
        {
            Layer outputLayer = layers[layers.Length - 1];
            for (int i = 0; i < outputLayer.Neurons.Length; i++)
            {
                outputLayer.Neurons[i].Delta += errorFunc.Derivative(outputLayer.Neurons[i].Output, desiredOutputs[i]);
            }

            for (int i = layers.Length - 1; i > 0; i--)
            {
                layers[i].Backpropagation(learningRate);
            }
        }

        public double Train(double[][] inputs, double[][] desiredOutputs, double learningRate, double momentum)
        {
            double total = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                total += GetError(inputs[i], desiredOutputs[i]);

                Backpropagation(learningRate, desiredOutputs[i]);
            }

            ApplyUpdates(momentum);

            return total / inputs.Length;
        }

        //public double GetError(double[] inputs, double[] desiredOutputs)
        //{
        //    double total = 0;
        //    for (int i = 0; i < desiredOutputs.Length; i++)
        //    {
        //        total += errorFunc.Function(Compute(inputs)[i], desiredOutputs[i]);
        //    }
        //    return total / inputs.Length;
        //}

        public double GetError(double[] inputs, double[] desiredOutputs)
        {
            if (desiredOutputs == null || desiredOutputs.Length != OutputCount) { throw new Exception("Desired outputs must be the same length as output count"); }
            double[] outputs = Compute(inputs);
            double error = 0;
            for (int i = 0; i < outputs.Length; i++)
            {
                error += errorFunc.Function(outputs[i], desiredOutputs[i]);
            }
            return error;
        }
    }
}
