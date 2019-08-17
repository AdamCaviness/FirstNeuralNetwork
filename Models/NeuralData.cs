namespace FirstNeuralNetwork
{
    public class NeuralData
    {
        private int _counter;

        public NeuralData(int rows)
        {
            Data = new double[rows][];
        }

        public void Add(params double[] record)
        {
            Data[_counter] = record;
            _counter++;
        }

        public double[][] Data {get;set;}
    }
}