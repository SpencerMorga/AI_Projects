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
