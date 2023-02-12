using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetIntro
{
    public class ErrorFunction
    {

        Func<double, double, double> function;
        Func<double, double, double> derivative;
        public ErrorFunction(Func<double, double, double> function, Func<double, double, double> derivative)
        {
            this.function = function;
            this.derivative = derivative;
        }

        public double Function(double actualOutput, double desiredOutput)
        {
            return function.Invoke(actualOutput, desiredOutput);
        }

        public double Derivative(double actualOutput, double desiredOutput)
        {
            return derivative.Invoke(actualOutput, desiredOutput);
        }
    }
}
