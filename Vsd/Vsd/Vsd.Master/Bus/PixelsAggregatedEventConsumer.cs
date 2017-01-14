namespace Vsd.Master.Bus
{
    using System;
    using System.Threading.Tasks;

    using MassTransit;

    using Vsd.Common;
    using Vsd.Messages;

    public class PixelsAggregatedEventConsumer : IConsumer<PixelsAndDepthReadEvent>
    {
        public static byte[] PixelsContainer { get; set; }

        public Task Consume(ConsumeContext<PixelsAndDepthReadEvent> context)
        {
            var drawImageCommand = context.Message;

            byte[] received = drawImageCommand.ImageDetails.Pixels;
            Buffer.BlockCopy(received, 0, PixelsContainer, 0, Resources.Rps);

            return Task.FromResult(0);
        }
    }
}