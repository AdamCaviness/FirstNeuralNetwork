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

        // public void Train (NeuralData x, NeuralData y, int iterations, double learningRate = 0.1)
        // {

        // }

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

        public List<NeuralLayer> Layers { get; set; }
    }
}