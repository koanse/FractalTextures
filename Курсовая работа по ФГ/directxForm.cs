using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Курсовая_работа_по_ФГ
{
    public partial class directxForm : Form
    {
        private Device device = null;
        PresentParameters presentParams;
        public directxForm()
        {
            InitializeComponent();
        }
        public void InitializeGraphics()
        {
            try
            {
                this.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
                this.UpdateStyles();
                presentParams = new PresentParameters();
                presentParams.BackBufferCount = 1;
                presentParams.SwapEffect = SwapEffect.Discard;
                presentParams.Windowed = true;
                device = new Device(0, DeviceType.Hardware, this,
                    CreateFlags.HardwareVertexProcessing, presentParams);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void directXform_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (device.CheckCooperativeLevel() == false)
                    device.Reset(presentParams);

                device.Clear(ClearFlags.Target, Color.Black, 1.0f, 0);

                VertexBuffer vertexBuffer =
                    new VertexBuffer(typeof(CustomVertex.PositionColored),
                    3, device, 0, CustomVertex.PositionColored.Format, Pool.Default);
                GraphicsStream stm = vertexBuffer.Lock(0, 0, 0);
                CustomVertex.PositionColored[] verts =
                    new CustomVertex.PositionColored[3];
                verts[0].Position = new Vector3(1, 2, 1);
                verts[0].Color = System.Drawing.Color.Red.ToArgb();
                //verts[0].Normal = new Vector3(0, 0, -1);
                verts[1].Position = new Vector3(0, 1, 2);
                verts[1].Color = System.Drawing.Color.Yellow.ToArgb();
                //verts[1].Normal = new Vector3(0, 0, -1);
                verts[2].Position = new Vector3(3, 1, 2);
                verts[2].Color = System.Drawing.Color.Green.ToArgb();
                //verts[2].Normal = new Vector3(0, 0, -1);
                stm.Write(verts);
                vertexBuffer.Unlock();

                device.BeginScene();
                device.VertexFormat = CustomVertex.PositionNormalColored.Format;
                device.SetStreamSource(0, vertexBuffer, 0);
                device.DrawUserPrimitives(PrimitiveType.TriangleList, 0, 1);
                //device.Transform.View.Translate(new Vector3(2, 2, 2));
                device.EndScene();
                device.Present();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Задаём новые размеры буфера глубины
            presentParams.BackBufferWidth = ClientSize.Width;
            presentParams.BackBufferHeight = ClientSize.Height;
            // Применяем новые параметры к устройству
            device.Reset(presentParams);
        }
    }
}