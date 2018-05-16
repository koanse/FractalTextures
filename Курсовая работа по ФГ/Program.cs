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
    public class Program : Form
    {
        [STAThread]
        static void Main()
        {
            using (directxForm dirX = new directxForm())
            {
                dirX.Show();
                dirX.InitializeGraphics();
                Application.Run(dirX);
            }
        }
    }
}