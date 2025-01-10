using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestControles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.DrawString("Prueba de escritura de texto", this.Font, Brushes.BlueViolet, 10, 10);
            //e.Graphics.DrawLine(new Pen(Color.Green), 10, 10, 100, 100);
        }

        bool flag = true;

        protected override void OnPaint(PaintEventArgs e) //Mejor asi
        {
            base.OnPaint(e);
            if (flag)
            {
                this.CreateGraphics().DrawString("Prueba de escritura de texto", this.Font, Brushes.BlueViolet, 10, 10);
                this.CreateGraphics().DrawLine(new Pen(Color.Green), 10, 10, 100, 100);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = !flag;

            Refresh();
        }
    }
}
