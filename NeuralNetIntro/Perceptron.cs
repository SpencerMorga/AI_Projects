using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NeuralNetIntro
{
    internal class Perceptron
    {
        double[] weights;
        double bias;
        double learningRate;
        ActivationFunction activationFunction;
        ErrorFunction errorFunction;

        public Perceptron(double[] weights, double bias,double learningRate, ActivationFunction af, ErrorFunction ef)
        {
            this.weights = weights;
            this.bias = bias;
            this.learningRate = learningRate;
            activationFunction = af;
            errorFunction = ef;
        }

        public Perceptron(int numinputs, double min, double max,  double learningRate, ActivationFunction af, ErrorFunction ef)
        {
            weights = new double[numinputs];
            Random rnd = new Random();
            Randomize(rnd, min, max);
            this.learningRate = learningRate;
            activationFunction = af;
            errorFunction = ef;
        }

        public void Randomize(Random random, double max, double min)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                double value = random.NextDouble();
                weights[i] = min + (value * (max - min));
            }
            bias = min + (random.NextDouble() * (max - min));
        }

        public double Compute(double[] inputs)
        {
            double total = 0;
            if (inputs == null) throw new ArgumentNullException("input is null");
            if (inputs.Length != weights.Length) throw new ArgumentException("input number doesn't match weights");
            
            for (int i = 0; i < weights.Length; i++)
            {
                total += (weights[i] * inputs[i]);
            }
            return total + bias;
        }

        public double[] Compute(double[][] inputs)
        {
            if (inputs == null) throw new ArgumentNullException("input is null");
            double[] returnvals = new double[inputs.Length];
            for (int i = 0; i < returnvals.Length; i++)
            {
                returnvals[i] = Compute(inputs[i]);
            }
            return returnvals;
        }

        public double GetError(double[][] inputs, double[] test)
        {
            double total = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                total += errorFunction.Function(Compute(inputs[i]), test[i]);
            }
            return total / inputs.Length;
        }


        public double Train(double[] inputs, double test) 
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                //calc change in vlaue
                //add it to weight of i
                
                weights[i] += learningRate * -( errorFunction.Derivative(activationFunction.Derivative(Compute(inputs)), test) * activationFunction.Derivative(Compute(inputs)) * inputs[i]);
            }
            //calc change in value, add to bias
            bias += learningRate * -(errorFunction.Derivative(activationFunction.Derivative(Compute(inputs)), test) * activationFunction.Derivative(Compute(inputs)));
            return errorFunction.Function(Compute(inputs), test);
        }
        //batch train vvv
        public double Train(double[][] inputs, double[] test) //sums change necessary and adds it to weights
        {
            //sum all errors
            //act inputs = compute
            double[] compinputs = new double[inputs.Length];
            double[] outputs = new double[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                compinputs[i] = Compute(inputs[i]);
                outputs[i] = activationFunction.Function(compinputs[i]);
            }

            double error = GetError(inputs, test);

            for (int i = 0; i < inputs.Length; i++)
            {
                double change = learningRate * -(errorFunction.Derivative(activationFunction.Function(compinputs[i]), test[i]) * activationFunction.Derivative(compinputs[i]));
                for (int j = 0; j < weights.Length; j++)
                {
                    weights[j] +=  change * inputs[i][j];
                }
                bias += change;
            }

            return error;
        }

        /* HILL CLIMBER
        public void Train(double[][] inputs, double[] test)
        {
            double[] weightCopy = new double[weights.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                weightCopy[i] = weights[i];
            }
            double biasCopy = bias;

            double initialError = GetError(inputs, test);
            for (int i = 0; i < 13000; i++)
            {
                weights.CopyTo(weightCopy, 0);
                biasCopy = bias;
                Mutate();
                double newError = GetError(inputs, test);
                if (newError > initialError)
                {
                    weightCopy.CopyTo(weights, 0);
                    bias = biasCopy;
                }
                else
                {
                    Console.WriteLine(newError);
                    initialError = newError;
                }
            }
        }
        

        public void Mutate()
        {
            Random rnd = new Random();
            int mutated = rnd.Next(0, weights.Length + 1);
            if (mutated == 0) // bias
            {
                bias += (rnd.Next(0, 2) * 2 - 1) * (rnd.Next(0, (int)(mutateAmt * 1000000))) / 1000000.0;
                ;
            }
            else // weight
            {
                weights[rnd.Next(weights.Length)] += (rnd.Next(0, 2) * 2 - 1) * (rnd.Next(0, (int)(mutateAmt * 1000000))) / 1000000.0;
            }
        }
        */
    }
}
