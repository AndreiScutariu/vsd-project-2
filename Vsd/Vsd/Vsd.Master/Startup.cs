namespace Vsd.Master
{
    using System;
    using System.Windows.Forms;

    using Vsd.Master.Bus;

    internal static class Startup
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var id = int.Parse(args[0]);

            var settings = new LocalSettings { Id = id };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MassTransitBus.Start(settings);

            var displayForm = new DisplayForm(settings);

            PixelsAggregatedEventConsumer.PixelsContainer = displayForm.PixelsBuffer;

            Application.Run(displayForm);

            MassTransitBus.Stop();
        }
    }
}