using System.Collections.Generic;

namespace FirstNeuralNetwork
{
    public class Neuron
    {
        public Neuron()
        {
            Dendrites = new List<Dendrite>();
            OutputPulse = new Pulse();
        }

        public List<Dendrite> Dendrites { get; set; }
        public Pulse OutputPulse { get; set; }

        public void Fire()
        {
            OutputPulse.Value = Sum();
            OutputPulse.Value = Activation(OutputPulse.Value);
        }

        public void UpdateWeights(double newWeights)
        {
            foreach (var terminal in Dendrites)
                terminal.SynapticWeight = newWeights;
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
            return input >= threshold ? 0 : threshold;
        }
    }
}