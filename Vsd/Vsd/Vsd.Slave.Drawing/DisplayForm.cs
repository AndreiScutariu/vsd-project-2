namespace Vsd.Slave.Drawing
{
    using System;
    using System.Windows.Forms;

    using MassTransit;

    using SharpGL;

    using Vsd.Common;
    using Vsd.Messages;
    using Vsd.Messages.Models;
    using Vsd.Slave.Drawing.Slaves;

    public partial class DisplayForm : Form
    {
        private readonly ISlave slave;

        private readonly IBus bus;

        private double rotation;

        public DisplayForm(ISlave slave, IBus bus)
        {
            this.slave = slave;
            this.bus = bus;

            InitializeComponent(slave.Settings);

            PixelsBuffer = new byte[Resources.Rps];
            DepthsBuffer = new byte[Resources.Dps];
        }

        public byte[] PixelsBuffer { get; set; }

        public byte[] DepthsBuffer { get; set; }

        // ReSharper disable once InconsistentNaming
        private OpenGL OpenGL => openGlControl.OpenGL;

        private void OpenGlControlOpenGlDraw(object sender, RenderEventArgs e)
        {
            slave.Draw(OpenGL, rotation);

            rotation += 2.0f;

            OpenGL.ReadPixels(0, 0, Resources.X, Resources.Y, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, PixelsBuffer);
            OpenGL.ReadPixels(0, 0, Resources.X, Resources.Y, OpenGL.GL_DEPTH_COMPONENT, OpenGL.GL_FLOAT, DepthsBuffer);

            bus.Publish(
                new PixelsAndDepthReadEvent
                {
                    ImageDetails =
                            new ImageDetails
                            {
                                Depths = DepthsBuffer,
                                Pixels = PixelsBuffer
                            }
                })
                .GetAwaiter()
                .GetResult();
        }

        private void OpenGlControlOpenGlInitialized(object sender, EventArgs e)
        {
            OpenGL.ClearColor(0, 0, 0, 0);
        }

        private void OpenGlControlResized(object sender, EventArgs e)
        {
            OpenGL.MatrixMode(OpenGL.GL_PROJECTION);
            OpenGL.LoadIdentity();
            OpenGL.Viewport(0, 0, Width, Height);
            OpenGL.Perspective(45.0f, Width / (double)Height, 1, 200.0);
            OpenGL.LookAt(0, 0, -15, 0, 0, 0, 0, 1, 0);
            OpenGL.MatrixMode(OpenGL.GL_MODELVIEW);
        }
    }
}