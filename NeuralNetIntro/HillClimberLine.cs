using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace NeuralNetIntro
{

    internal class HillClimberLine
    {
        public void Climb(List<Point> points)
        {
            Random random = new Random();
            //double m = random.Next(0, 20);
            //double b = random.Next(0, 20);
            double m = -1;
            double b = 15;

            double mae = MAE(m, b, points);
            for (int i = 0; i < 13000; i++)
            {
                double mutatedB = MutateB(b);
                if (MAE(m, mutatedB, points) < mae)
                {
                    b = mutatedB;
                    mae = MAE(m, b, points);
                    Console.WriteLine("M = " + m + "; B = " + b + "; MAE = " + mae);
                }

                double mutatedM = MutateM(m); 
                if (MAE(mutatedM, b, points) < mae)
                {
                    m = mutatedM;
                    mae = MAE(m, b, points);
                    Console.WriteLine("M = " + m + "; B = " + b + "; MAE = " + mae);
                }
            }
            //iterate m and b separately
        }

        private static double MAE(double m, double b, List<Point> points)
        {
            double total = 0;
            for (int i = 0; i < points.Count; i++)
            { 
                total += Math.Abs(points[i].Y - ((points[i].X * m) + b));
            }
            return total / points.Count;
        }

        private static double MutateM(double m)
        {
            Random r = new Random();
            return m += (r.Next(0, 2) * .1 - 0.05);
        }

        private static double MutateB(double b)
        {
            Random r = new Random();
            return b += (r.Next(0, 2) * .2 - 0.1);
        }

    }
}
