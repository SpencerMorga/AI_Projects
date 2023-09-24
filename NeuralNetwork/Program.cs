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

            NeuralNetwork net = new NeuralNetwork(actFunc, errorFunc, new int[] { 2, 2, 1 });
            net.Randomize(random, -1, 1);
            //for (int i = 0; i < 1000; i++)
            //{
            //    Console.WriteLine(net.Compute(input, output, 0.125));
            //}

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < input.Length; i++)
                {
                    Console.Write("Inputs: ");
                    for (int j = 0; j < input[i].Length; j++)
                    {
                        if (j != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write(input[i][j]);
                    }

                    Console.Write(" Output: " + Math.Round(net.Compute(input[i])[0], 3));
                    Console.WriteLine();
                }
                double error = net.BatchTrain(input, output, 1, 0.002, 0.4);
                Console.WriteLine("Error: " + Math.Round(error, 3));
            }
            
        }
    }
}