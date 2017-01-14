namespace Vsd.Master.Bus
{
    using System;

    using MassTransit;

    public class MassTransitBus
    {
        private static IBusControl instanceControl;

        public static IBus Instance => instanceControl;

        public static void Start(LocalSettings localSettings)
        {
            instanceControl = ConfigureBus(localSettings);

            instanceControl.Start();
        }

        public static void Stop()
        {
            instanceControl.Stop();
        }

        private static IBusControl ConfigureBus(LocalSettings localSettings)
        {
            var queueName = "Vsd.Master" + localSettings.Id;

            return Bus.Factory.CreateUsingRabbitMq(
                config =>
                    {
                        var host = config.Host(new Uri("rabbitmq://localhost/"),
                            h =>
                                {
                                    h.Username("guest");
                                    h.Password("guest");
                                });

                        config.ReceiveEndpoint(host, queueName, e => { e.Consumer<PixelsAggregatedEventConsumer>(); });
                    });
        }
    }
}