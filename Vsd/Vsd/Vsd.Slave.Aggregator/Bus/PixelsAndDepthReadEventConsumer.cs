namespace Vsd.Slave.Aggregator.Bus
{
    using System.Threading.Tasks;

    using MassTransit;

    using Vsd.Messages;

    public class PixelsAndDepthReadEventConsumer : IConsumer<PixelsAndDepthReadEvent>
    {
        public static IBus Bus { get; set; }

        public Task Consume(ConsumeContext<PixelsAndDepthReadEvent> context)
        {
            Bus.Publish(new PixelsAggregatedEvent { ImageDetails = context.Message.ImageDetails });

            return Task.FromResult(0);
        }
    }
}