using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormEjercicio1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelTextBox1.Posicion = labelTextBox1.Posicion == NuevosComponentes.LabelTextBox.EPosicion.DERECHA ? NuevosComponentes.LabelTextBox.EPosicion.IZQUIERDA : NuevosComponentes.LabelTextBox.EPosicion.DERECHA;
            labelTextBox1.Separacion += 10;
        }

        private void labelTextBox1_PosicionChanged(object sender, EventArgs e)
        {
            this.Text = labelTextBox1.Posicion.ToString();
        }

        private void labelTextBox1_SeparacionChanged(object sender, EventArgs e)
        {
            this.Text += "\tSeparacion :D";
        }

        private void labelTextBox1_TxtChanged(object sender, EventArgs e)
        {
            Text = "patata";
        }
    }
}
