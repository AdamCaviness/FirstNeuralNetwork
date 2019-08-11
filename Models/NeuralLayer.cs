using System;
using System.Collections.Generic;

namespace FirstNeuralNetwork
{
    public class NeuralLayer
    {
        public NeuralLayer(int neuronCount, double initialWeight, string name = "")
        {
            Name = name;
            Weight = initialWeight;
            Neurons = new List<Neuron>();
            for (var i = 0; i < neuronCount; i++)
                Neurons.Add(new Neuron());
        }

        public void Log()
        {
            Console.WriteLine($"{Name}, Weight: {Weight}");
        }
        public void Forward()
        {
            foreach (var neuron in Neurons)
                neuron.Fire();
        }
        public void Optimize(double learningRate, double delta)
        {
            Weight += learningRate * delta;

            foreach (var neuron in Neurons)
                neuron.UpdateWeights(Weight);
        }

        public string Name { get; set; }
        public double Weight { get; set; }
        public List<Neuron> Neurons { get; set; }
    }
}