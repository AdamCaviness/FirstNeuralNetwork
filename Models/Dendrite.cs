namespace FirstNeuralNetwork
{
    public class Dendrite
    {
        public Dendrite()
        {
            Learnable = true;
            InputPulse = new Pulse();
        }

        public bool Learnable { get; set; }
        public Pulse InputPulse { get; set; }
        public double SynapticWeight { get; set; }
    }
}