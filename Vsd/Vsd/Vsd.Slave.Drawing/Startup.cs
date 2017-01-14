namespace Vsd.Slave.Drawing
{
    using System.Windows.Forms;

    using Vsd.Slave.Drawing.Bus;
    using Vsd.Slave.Drawing.Slaves.Utils;

    internal static class Startup
    {
        private static void Main(string[] args)
        {
            string[] argsParam = args[0].Split('-');

            var parrentId = int.Parse(argsParam[0]);
            var stringParam = argsParam[1];

            var slaveKey = int.Parse(stringParam[0].ToString());
            var drawType = int.Parse(stringParam[1].ToString());
            var drawRotation = int.Parse(stringParam[2].ToString());
            var drawColor = int.Parse(stringParam[3].ToString());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var settings = new Settings
                               {
                                   SlaveKey = slaveKey,
                                   DrawType = drawType,
                                   DrawColor = drawColor,
                                   DrawRotation = drawRotation,
                                   ParrentId = parrentId
                               };

            var slave = SlaveFactory.GetSlave(settings);

            MassTransitBus.Start(settings);

            var displayForm = new DisplayForm(slave, MassTransitBus.Instance);
            Application.Run(displayForm);

            MassTransitBus.Stop();
        }
    }
}