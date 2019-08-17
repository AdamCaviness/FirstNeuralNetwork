using System.Collections.Generic;

namespace FirstNeuralNetwork
{
    public class Neuron
    {
        public Neuron()
        {
            OutputPulse = new Pulse();
            Dendrites = new List<Dendrite>();
        }

        public Pulse OutputPulse { get; set; }
        public List<Dendrite> Dendrites { get; set; }

        public void Fire()
        {
            OutputPulse.Value = Sum();
            OutputPulse.Value = Activation(OutputPulse.Value);
        }

        public void UpdateWeights(double newWeights)
        {
            foreach (var dendrite in Dendrites)
                dendrite.SynapticWeight = newWeights;
        }

        private double Sum()
        {
            var computeValue = 0.0d;
            foreach (var dendrite in Dendrites)
                computeValue += dendrite.InputPulse.Value * dendrite.SynapticWeight;

            return computeValue;
        }
        private double Activation(double input)
        {
            var threshold = 1d;
            return input <= threshold ? 0 : threshold;
        }
    }
}