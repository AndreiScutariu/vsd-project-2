namespace Vsd.Slave.Drawing
{
    using Vsd.Slave.Drawing.Slaves;
    using Vsd.Slave.Drawing.Slaves.Utils;

    internal static class SlaveFactory
    {
        public static ISlave GetSlave(Settings settings)
        {
            if (settings.DrawType == 1)
            {
                return new CircularSphere { Settings = settings };
            }

            return new StaticSphere { Settings = settings };
        }
    }
}