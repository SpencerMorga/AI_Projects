using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NeuralNetIntro;

namespace NeuralNetworks
{
    public class Neurons
    {
        public double bias;
        public DendriteBase[] dendrites;
        public double Output { get; set; }

        public double Input { get; set; }
        
        public double Delta { get; set; }
        double biasUpdate;

        public ActivationFunction activationFunction { get; set; }
        public ErrorFunction errorFunction { get; set; }
        public Neurons(ActivationFunction activationFunction, ErrorFunction errorFunction, Neurons[]? previousNeurons)
        {
            this.activationFunction = activationFunction;
            this.errorFunction = errorFunction;
           
            if (previousNeurons == null)
            {
                dendrites = new DendriteBase[1];
                dendrites[0] = new InputDendrite();
            }    
            else
            {
                dendrites = new DendriteBase[previousNeurons.Length];
                for (int i = 0; i < previousNeurons.Length; i++)
                {
                    dendrites[i] = new Dendrites(previousNeurons[i]);
                }
            }
        }
        //E.D.D.E.N. : engineered developmental dramatic electrical network
        public void Randomize(Random random, double max, double min)
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                double value = random.NextDouble();
                dendrites[i].weight = min + (value * (max - min));
            }
            bias = min + (random.NextDouble() * (max - min));
        }

        public double Compute()
        {
            Input = bias;

            for (int i = 0; i < dendrites.Length; i++)
            {
                Input += dendrites[i].Compute();
            }
            

            Output = activationFunction.Function(Input);
            return Output;
        }

        public void Backpropagation(double learningRate)
        {
            //output (first) layers, run the below line calcualtion. this will set initial delta with learningrate*-derivative to be modifided continuously
            //for all hidden layers, cycle this function with delta * act deriv * weight
            //check wiki for additional confirmation about process

            //Delta = learningRate * -(errorFunction.Derivative(activationFunction.Derivative(Compute()), Output) * activationFunction.Derivative(Compute()) * Input);

            double temp = activationFunction.Derivative(Input) * Delta;
            
            for (int i = 0; i < dendrites.Length; i++)
            {
                //previous delta add sum temp * weight
                ((Dendrites)dendrites[i]).Previous.Delta += temp * ((Dendrites)dendrites[i]).weight;

                ((Dendrites)dendrites[i]).WeightUpdate -= learningRate * temp * ((Dendrites)dendrites[i]).Previous.Output;
            }
            biasUpdate -= temp * learningRate;

            Delta = 0;
            //previous neuron delta set to weight * current delta...? access from dendrite
        }

        public void ApplyUpdates()
        {
            bias += biasUpdate;
            biasUpdate = 0;

            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].ApplyUpdates();
            }
        }
    }
}
