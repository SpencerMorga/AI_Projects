using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class GeneticLearning
    {
        NeuralNetwork net;
        Random random;
        double mutationRate;

        public GeneticLearning(NeuralNetwork net, double mutationRate, Random? random=null)
        {
            this.net = net;
            this.random = random == null ? new Random() : random;
            this.mutationRate = mutationRate;
        }

        public void Fitness()
        {
            List<int> list = new List<int>();
            int a = list.Count();
        }

        public void Mutate(NeuralNetwork net, double mutationRate, Random random)
        {
            foreach (Layer layer in net.layers)
            {
                foreach(Neurons neuron in layer.Neurons)
                {
                    for (int i = 0;i < neuron.dendrites.Length; i++)
                    {
                        if (random.NextDouble() < mutationRate)
                        {
                            double weight = neuron.dendrites[i].weight;
                            weight = random.Next(2) == 0 ?  (random.Next(2) == 0 ? weight - random.NextDouble() : weight + random.NextDouble()) : weight * -1;
                        }
                    }
                    if (random.NextDouble() < mutationRate)
                    {
                        neuron.bias = random.Next(2) == 0 ? (random.Next(2) == 0 ? neuron.bias - random.NextDouble() : neuron.bias + random.NextDouble()) : neuron.bias * -1;
                    }
                }
            }    
        }

        public void Crossover(NeuralNetwork winner, NeuralNetwork loser, Random random)
        {
            for (int i = 0; i < winner.layers.Length; i++)
            {
                Layer winLayer = winner.layers[i];
                Layer loseLayer = loser.layers[i];

                int cutindex = random.Next(winLayer.Neurons.Length);
                bool flip = random.Next(2) == 0;

                for(int j = (flip ? 0 : cutindex); j < (flip ? cutindex : winLayer.Neurons.Length); j++)
                {
                    Neurons winNeuron = winLayer.Neurons[i];
                    Neurons loseNeuron = loseLayer.Neurons[i];

                    for (int k = 0; k < winNeuron.dendrites.Length; k++)
                    {
                        loseNeuron.dendrites[i].weight = winNeuron.dendrites[i].weight;
                    }
                    loseNeuron.bias = winNeuron.bias;
                }
            }
        }
    }
}
