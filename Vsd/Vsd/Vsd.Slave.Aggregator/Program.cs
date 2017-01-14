namespace Vsd.Slave.Aggregator
{
    using System;

    using Vsd.Master.Bus;
    using Vsd.Slave.Aggregator.Bus;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var id = int.Parse(args[0]);

            MassTransitBus.Start(id);

            PixelsAndDepthReadEventConsumer.Bus = MassTransitBus.Instance;

            Console.ReadKey();
            MassTransitBus.Stop();
        }
    }
}