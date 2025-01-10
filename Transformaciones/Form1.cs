using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transformaciones
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            Graphics g = e.Graphics;
            ControlPaint.DrawButton(g, 0, 40, 100, 60, ButtonState.Checked);


            //Traslación
            g.TranslateTransform(100, 100);
            g.DrawLine(Pens.Red, 0, 0, 100, 0);
            g.ResetTransform();
            //Rotación de 30º en sentido horario
            g.RotateTransform(30);
            g.DrawLine(Pens.Blue, 0, 0, 100, 0);
            g.ResetTransform();
            //Traslación + rotación
            g.TranslateTransform(100, 100);
            g.RotateTransform(30);
            g.DrawLine(Pens.Green, 0, 0, 100, 0);
            g.ResetTransform();
        }
    }
}
