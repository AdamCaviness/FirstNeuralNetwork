using System;

namespace FirstNeuralNetwork
{
    class Program
    {
        static void Main (string[] args)
        {
            var model = new NetworkModel();
            model.Layers.Add(new NeuralLayer(2, 0.1, "INPUT"));
            model.Layers.Add(new NeuralLayer(2, 0.1, "HIDDEN"));
            model.Layers.Add(new NeuralLayer(1, 0.1, "OUTPUT"));

            model.Build();
            model.Print();
        }
    }
}