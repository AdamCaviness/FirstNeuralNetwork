using System.Collections.Generic;

namespace FirstNeuralNetwork
{
    public class Neuron
    {
        private double _weight;

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

        public void Compute(double learningRate, double delta)
        {
            _weight += learningRate * delta;
            foreach (var terminal in Dendrites)
                terminal.SynapticWeight = _weight;
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