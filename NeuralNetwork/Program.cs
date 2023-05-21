using NeuralNetIntro;

namespace NeuralNetworks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] input =
             {
               new double[] { 0, 1 },
               new double[] { 0, 0 },
               new double[] { 1, 0 },
               new double[] { 1, 1 },
            };

            double[] test = { 1, 0, 1, 0 };
            double error = 0;

            Func<double, double, double> mse = (actual, expected) => Math.Pow(expected - actual, 2);

            Func<double, double, double> mseDeriv = (actual, expected) => -2 * (expected - actual);

            ErrorFunction errorFunc = new ErrorFunction(mse, mseDeriv);
            ActivationFunction actFunc = new ActivationFunction(ActivationFunction.Identity, ActivationFunction.Identity_deriv);
            NeuralNetwork net = new NeuralNetwork(actFunc, errorFunc, new int[] {2, 2, 1});
            do
            {
                net.
            }
        }
    }
}