using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ConsoleTableExt;

namespace FirstNeuralNetwork
{
    public class NetworkModel
    {
        public NetworkModel()
        {
            Layers = new List<NeuralLayer>();
        }

        public void AddLayer(NeuralLayer layer)
        {
            var dendriteCount = 1;

            if (Layers.Count > 0)
                dendriteCount = Layers.Last().Neurons.Count;

            foreach (var neuron in layer.Neurons)
            {
                for (var i = 0; i < dendriteCount; i++)
                    neuron.Dendrites.Add(new Dendrite());
            }
        }

        public void Build()
        {
            var i = 0;
            foreach (var layer in Layers)
            {
                if (i >= Layers.Count - 1)
                    break;

                var nextLayer = Layers[i + 1];
                CreateNetwork(layer, nextLayer);

                i++;
            }
        }

        public void Train(NeuralData x, NeuralData y, int iterations, double learningRate = 0.1)
        {
            var epoch = 1;

            // Loop till the number of iterations.
            while (iterations >= epoch)
            {
                // Get the input layers.
                var inputLayer = Layers[0];
                var outputs = new List<double>();

                // Loop through the data rows.
                for (var i = 0; i < x.Data.Length; i++)
                {
                    // Set the input data into the first layer.
                    for (var j = 0; j < x.Data[i].Length; j++)
                        inputLayer.Neurons[j].OutputPulse.Value = x.Data[i][j];

                    // Fire all the neurons and collect the output.
                    ComputeOutput();
                    outputs.Add(Layers.Last().Neurons.First().OutputPulse.Value);
                }

                // Check the accuracy score against y with the actual output.
                var accuracySum = 0d;
                var yCounter = 0;
                outputs.ForEach(o =>
                {
                    if (o == y.Data[yCounter].First())
                        accuracySum++;

                    yCounter++;
                });

                // Optimize the synaptic weights.
                OptimizeWeights(accuracySum / yCounter);
                Console.WriteLine($"Epoch: {epoch}, Accuracy: {(accuracySum / yCounter) * 100}");

                epoch++;
            }
        }

        public void Print()
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Neurons");
            dt.Columns.Add("Weight");

            foreach (var layer in Layers)
            {
                var dr = dt.NewRow();
                dr[0] = layer.Name;
                dr[1] = layer.Neurons.Count;
                dr[2] = layer.Weight;

                dt.Rows.Add(dr);
            }

            var builder = ConsoleTableBuilder.From(dt);
            builder.ExportAndWrite();
        }

        public void CreateNetwork(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var to in connectingTo.Neurons)
            {
                foreach (var from in connectingFrom.Neurons)
                    to.Dendrites.Add(new Dendrite() { InputPulse = to.OutputPulse, SynapticWeight = connectingTo.Weight });
            }
        }

        private void ComputeOutput()
        {
            var first = true;
            foreach (var layer in Layers)
            {
                // Skip first layer as it is input.
                if (first)
                {
                    first = false;
                    continue;
                }

                layer.Forward();
            }
        }
        private void OptimizeWeights(double accuracy)
        {
            var lr = 0.1f;

            // Skip if the accuracy reached is 100%.
            if (accuracy == 1)
                return;

            if (accuracy > 1)
                lr = -lr;

            // Update the weights for all the layers.
            foreach (var layer in Layers)
                layer.Optimize(lr, 1);
        }

        public List<NeuralLayer> Layers { get; set; }
    }
}