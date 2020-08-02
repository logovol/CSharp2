using System;

namespace task_1
{
    class FixedRateWorker : Worker
    {
        public double FixedRate { get; }

        public FixedRateWorker(string name, string lastName, double rate) : base(name, lastName)
        {
            FixedRate = rate;
        }

        public override void AveragePayment()
        {
            double av = FixedRate;
            Console.WriteLine($"Среднемесячная заработная плата: {av}");
        }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }


    }
}
