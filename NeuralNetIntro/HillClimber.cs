using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetIntro
{
    internal class HillClimber
    {
        public void Climb(string input)
        {
            Random random = new Random();
            string rnd = "";
            for (int i = 0; i < input.Length; i++)
            {
                rnd += Convert.ToChar(random.Next(32, 126));
            }
            double mae = MAE(input, rnd);
            
            while (!(mae == 0))
            {
                string mutated = Mutate(rnd);
                if (MAE(input, mutated) < mae)
                {
                    mae = MAE(input, mutated);
                    rnd = mutated;
                }
                else
                {
                    continue;
                }
            }
            
        }

        private static double MAE(string a, string b) //a = desired, b = actual
        {
            double total = 0;
            for (int i = 0; i < a.Length; i++)
            {
                total += Math.Abs(a[i] - b[i]);
            }
            return total / a.Length;
        }

        private static string Mutate(string input)
        {
            
            Random rnd = new Random();
            int index = rnd.Next(0, input.Length);
            char removed = input[index];
            input = input.Remove(index, 1);
            input = input.Insert(index, ((char)(removed + ((rnd.Next(0, 2) * 2) - 1))).ToString());

            //Console.Clear();
            Console.WriteLine(input);
            return input;
        }
    }
}
