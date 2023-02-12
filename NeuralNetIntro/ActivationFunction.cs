using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NeuralNetIntro
{
    public class ActivationFunction
    {
        Func<double, double> function;
        Func<double, double> derivative;
        public ActivationFunction(Func<double, double> function, Func<double, double> derivative)
        {
            this.function = function;
            this.derivative = derivative;
        }

        public double Function(double input)
        {
            return function.Invoke(input);
        }

        public double Derivative(double input)
        {
            return derivative.Invoke(input);
        }
        //activation functions
        public static double ReLU(double input)
        {
            if (input < 0) return 0;
            else return input;
        }
        public static double Binary(double input)
        {
            if (input < 0) return 0;
            else return 1;
        }
        public static double Sigmoid(double input)
        {
            return 1.0 / (1 + (Math.Pow(Math.E, -input)));
        }
        public static double TanH(double input)
        {
            return ((Math.Pow(Math.E, input)) - (Math.Pow(Math.E, -input))) / ((Math.Pow(Math.E, input)) + (Math.Pow(Math.E, -input)));
        }

        public static double Identity(double input)
        {
            return input;
        }
        //derivatives
        public static double ReLU_deriv(double input)
        {
            if (input <= 0) return 0;
            else return 1;
        }
        public static double Sigmoid_deriv(double input)
        {
            return ReLU(input) * (1 - ReLU(input));
        }
        public static double TanH_deriv(double input)
        {
            return 1 - (TanH(input) * TanH(input));
        }
        public static double Identity_deriv(double input)
        {
            return 1;
        }

    }
}
