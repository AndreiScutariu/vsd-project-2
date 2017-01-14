namespace Vsd.Master.Bus
{
    using System;

    using MassTransit;

    using Vsd.Slave.Aggregator.Bus;

    public class MassTransitBus
    {
        private static IBusControl instanceControl;

        public static IBus Instance => instanceControl;

        public static void Start(int id)
        {
            instanceControl = ConfigureBus(id);

            instanceControl.Start();
        }

        public static void Stop()
        {
            instanceControl.Stop();
        }

        private static IBusControl ConfigureBus(int id)
        {
            var queueName = "Vsd.Slave.Aggregator" + id;

            return Bus.Factory.CreateUsingRabbitMq(
                config =>
                    {
                        var host = config.Host(new Uri("rabbitmq://localhost/"),
                            h =>
                                {
                                    h.Username("guest");
                                    h.Password("guest");
                                });

                        config.ReceiveEndpoint(host, queueName, e => { e.Consumer<PixelsAndDepthReadEventConsumer>(); });
                    });
        }
    }
}