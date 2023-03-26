using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class GeneticLearning
    {

        Random random;
        double mutationRate;
        int neuronsPerLayer;

        (NeuralNetwork, int)[] population;
        // population going to be looped through, each int value assigned to the return value (score) of the function in flappy bird. net will be created locally
        // function details: takes the net (created in this class), returns fitness value (done in flappy bird)
        // from then, create train

        public GeneticLearning(double mutationRate, int neuronsPerLayer, int populationCount, Random? random = null)
        {
            this.random = random == null ? new Random() : random;
            this.mutationRate = mutationRate;
            this.neuronsPerLayer = neuronsPerLayer;
            population = new (NeuralNetwork, int)[populationCount];
        }

        public void Mutate(NeuralNetwork net, double mutationRate, Random random)
        {
            //propogate through layer, neurons, mutate weights list in neurons
            //with a rnd double less than mutation rate and either flip sign or mutate by that amount
            //mutate bias
           
        }

        public void Crossover(NeuralNetwork winner, NeuralNetwork loser, Random random)
        {
            //loop through winner net (layers)
            //create references to each layer
            //calculate cut point and flip (0 or 1)
            //either copy from 0 to cutpoint or cutpoint to end based on flip value (use conditionals)
            //loop through neurons
            //create neuron references
            //copy weights with copyto and bias with =, copy loser shit to winner shit

        }
    }
}
