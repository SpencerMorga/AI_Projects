using System;
using System.Drawing;

namespace NeuralNetIntro
{
    using StringClimber = HillClimberLine;
    internal class Program
    {
        public static void Main(string[] args)
        {
            double[][] input =
            {
               new double[] { 0, 1 },
               new double[] { 0, 0 },
               new double[] { 1, 0 },
               new double[] { 1, 1 },
            };

            double[] test = { 0, 0, 0, 1 };
            double error = 0;

            Func<double, double, double> mse = (actual, expected) => Math.Pow(expected - actual, 2);

            Func<double, double, double> mseDeriv = (actual, expected) => -2 * (expected - actual);

            ErrorFunction errorFunc = new ErrorFunction(mse, mseDeriv);
            ActivationFunction actFunc = new ActivationFunction(ActivationFunction.Identity, ActivationFunction.Identity_deriv);
            Perceptron perceptron = new Perceptron(2, 0, 1, 0.05, actFunc, errorFunc);
            do
            {
                error = perceptron.Train(input, test);
            }
            while (error > 0.07);
           
            double[] output = perceptron.Compute(input);
            ;
        }
    }
}