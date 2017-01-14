namespace Vsd.Slave.Drawing.Slaves
{
    using SharpGL;

    using Vsd.Slave.Drawing.Slaves.Utils;

    public interface ISlave
    {
        Settings Settings { get; set; }

        void Draw(OpenGL gl, double rotation);
    }
}