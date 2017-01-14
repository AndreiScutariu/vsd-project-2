namespace Vsd.Slave.Drawing.Bus
{
    using System;

    using MassTransit;

    using Vsd.Slave.Drawing.Slaves.Utils;

    public class MassTransitBus
    {
        private static IBusControl instanceControl;

        public static IBus Instance => instanceControl;

        public static void Start(Settings settings)
        {
            instanceControl = ConfigureBus();
            instanceControl.Start();
        }

        public static void Stop()
        {
            instanceControl.Stop();
        }

        private static IBusControl ConfigureBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(
                cfg =>
                    {
                        cfg.Host(
                            new Uri("rabbitmq://localhost"),
                            h =>
                                {
                                    h.Username("guest");
                                    h.Password("guest");
                                });
                    });
        }
    }
}