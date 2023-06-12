using NeuralNetIntro;
using System.Transactions;


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

            double[][] output =
              {
               new double[] { 1 },
               new double[] { 0 },
               new double[] { 1 },
               new double[] { 0 },
            };


            Func<double, double, double> mse = (actual, expected) => Math.Pow(expected - actual, 2);

            Func<double, double, double> mseDeriv = (actual, expected) => -2 * (expected - actual);

            ErrorFunction errorFunc = new ErrorFunction(mse, mseDeriv);
            ActivationFunction actFunc = new ActivationFunction(ActivationFunction.TanH, ActivationFunction.TanH_deriv);
            Random random = new Random(2);
            (NeuralNetwork, int)[] nets = new (NeuralNetwork, int)[100];
            for (int i = 0; i < 100; i++)
            {
                nets[i].Item1 = (new NeuralNetwork(actFunc, new int[] { 2, 2, 1 }));
                nets[i].Item2 = (int)(-nets[i].Item1.SumError(input, output, errorFunc) * 1000);
            }

            GeneticLearning geneticLearning = new GeneticLearning(0.1, random);
            while (nets[0].Item2 < -0.01)
            {      
                geneticLearning.Train(nets);
                for (int i = 0; i < 100; i++)
                {
                    nets[i].Item2 = (int)(-nets[i].Item1.SumError(input, output, errorFunc) * 1000);
                }
            }
            ;

        }
    }
}