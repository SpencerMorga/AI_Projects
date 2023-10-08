using NeuralNetIntro;
using System.Transactions;


namespace NeuralNetworks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] input = new double[628][];

            for (int i = 0; i < 628; i++)
            {
                input[i] = new double[] { i / 100.0 };
            }

            double[][] output = new double[628][];

            for (int i = 0; i < 628; i++)
            {
                output[i] = new double[] { Math.Sin(i / 100.0) };
            }


            Func<double, double, double> mse = (actual, expected) => Math.Pow(expected - actual, 2); 

            Func<double, double, double> mseDeriv = (actual, expected) => -2 * (expected - actual);

            ErrorFunction errorFunc = new ErrorFunction(mse, mseDeriv);

            ActivationFunction actFunc = new ActivationFunction(ActivationFunction.TanH, ActivationFunction.TanH_deriv);

            Random random = new Random();

            NeuralNetwork net = new NeuralNetwork(actFunc, errorFunc, new int[] { 1, 2, 2, 1 });
            net.Randomize(random, -1, 1);

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 600; i < input.Length; i++)
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