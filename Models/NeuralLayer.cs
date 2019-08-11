using System;
using System.Collections.Generic;

namespace FirstNeuralNetwork
{
    public class NeuralLayer
    {
        public NeuralLayer (int count, double initialWeight, string name = "")
        {
            Name = name;
            Weight = initialWeight;
            Neurons = new List<Neuron> ();
            for (var i = 0; i < count; i++)
                Neurons.Add (new Neuron ());
        }

        public void Compute (double learningRate, double delta)
        {
            foreach (var neuron in Neurons)
                neuron.Compute (learningRate, delta);
        }
        public void Log ()
        {
            Console.WriteLine ($"{Name}, Weight: {Weight}");
        }

        public string Name { get; set; }
        public double Weight { get; set; }
        public List<Neuron> Neurons { get; set; }
    }
}