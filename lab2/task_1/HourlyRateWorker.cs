using System;

namespace task_1
{
    class HourlyRateWorker : Worker
    {
        public double HourlyRate { get; }

        public HourlyRateWorker(string name, string lastName, double rate) : base(name, lastName)
        {
            HourlyRate = rate;
        }

        public override void AveragePayment()
        {
            double av = 20.8 * 8 * HourlyRate;
            Console.WriteLine($"Среднемесячная заработная плата: {av}");
        }

        public override string ToString()
        {
            return $"{Name} {LastName}";
        }

    }
}
