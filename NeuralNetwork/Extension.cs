using NeuralNetIntro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    internal static class Extension
    {

        public static double SumError(this NeuralNetwork net, double[][] inputs, double[][] desiredOutputs, ErrorFunction errorFunc)
        {
            double sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += net.GetError(inputs[i], desiredOutputs[i], errorFunc);
            }
            return sum / inputs.Length;
        }
        public static double GetError(this NeuralNetwork net, double[] inputs, double[] desiredOutputs, ErrorFunction errorFunc)
        {
            double[] computed = net.Compute(inputs);
            double total = 0;
            for (int i = 0; i < desiredOutputs.Length; i++)
            {
                total += errorFunc.Function(computed[i], desiredOutputs[i]);
            }
            return total / inputs.Length;

        }
    }
}
