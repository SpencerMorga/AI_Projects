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
               new double[] { 0, 0 },
               new double[] { 0, 1 },
               new double[] { 1, 0 },
               new double[] { 1, 1 },
            };

            double[][] output =
              {
               new double[] { 0 },
               new double[] { 1 },
               new double[] { 1 },
               new double[] { 0 },
            };


            Func<double, double, double> mse = (actual, expected) => Math.Pow(expected - actual, 2);

            Func<double, double, double> mseDeriv = (actual, expected) => -2 * (expected - actual);

            ErrorFunction errorFunc = new ErrorFunction(mse, mseDeriv);
            ActivationFunction actFunc = new ActivationFunction(ActivationFunction.TanH, ActivationFunction.TanH_deriv);
            Random random = new Random();
            (NeuralNetwork, int)[] nets = new (NeuralNetwork, int)[100];

            for (int i = 0; i < 100; i++)
            {
                nets[i].Item1 = (new NeuralNetwork(actFunc, new int[] { 2, 2, 1 }));
                nets[i].Item1.Randomize(random, -5, 5);

                nets[i].Item2 = (int)(nets[i].Item1.SumError(input, output, errorFunc) * 1000);
            }

            nets[0].Item1.layers[1].Neurons[0].dendrites[0].weight = -1.849;
            nets[0].Item1.layers[1].Neurons[0].dendrites[1].weight = 1.366;
            nets[0].Item1.layers[1].Neurons[0].bias = -1.885;

            nets[0].Item1.layers[1].Neurons[1].dendrites[0].weight = -4.9488;
            nets[0].Item1.layers[1].Neurons[1].dendrites[1].weight = 2.47196;
            nets[0].Item1.layers[1].Neurons[1].bias = 3.9537;

            nets[0].Item1.layers[2].Neurons[0].dendrites[0].weight = 3.504;
            nets[0].Item1.layers[2].Neurons[0].dendrites[1].weight = -1.9808;
            nets[0].Item1.layers[2].Neurons[0].bias = 4.888;



            GeneticLearning geneticLearning = new GeneticLearning(0.1, random);
            while (nets[0].Item2 > 10)
            {
                for (int i = 0; i < 100; i++)
                {
                    nets[i].Item2 = (int)(nets[i].Item1.SumError(input, output, errorFunc) * 1000);
                }
                geneticLearning.Train(nets);
            }
            ;
        }
    }
}