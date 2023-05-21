using NeuralNetIntro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworks
{
    internal class GeneticLearning
    {

        Random random;
        double mutationRate;
        int neuronsPerLayer;
        ErrorFunction errorFunc;
        ActivationFunction actFunc;

        (NeuralNetwork, int)[] population;
        // population going to be looped through, each int value assigned to the return value (score) of the function in flappy bird. net will be created locally
        // function details: takes the net (created in this class), returns fitness value (done in flappy bird)
        // from then, create train

        public GeneticLearning(double mutationRate, int[] neuronsPerLayer, int populationCount, ErrorFunction ErrorFunc, ActivationFunction ActFunc, Random? random = null)
        {
            this.random = random == null ? new Random() : random;
            this.mutationRate = mutationRate;
            population = new (NeuralNetwork, int)[populationCount];

            for (int i = 0; i < populationCount; i++)
            {
                NeuralNetwork newNet = new NeuralNetwork(ActFunc, ErrorFunc, neuronsPerLayer);
                population[i] = (newNet, ); 
            }
        }

        public void Fitness()
        {


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

        public void Train(double[][] input)
        {
            
        }
    }
}
