using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NuevosComponentes
{
    public enum EMarca
    {
        Nada,
        Cruz,
        Circulo,
        Imagen
    }


    [DefaultEvent("ClickEnMarca")]
    [DefaultProperty("Marca")]
    public partial class EtiquetaAviso : Control
    {
        private EMarca marca = EMarca.Nada;

        [Category("Appearance")]
        [Description("Indica el tipo de marca que aparece junto al texto")]
        public EMarca Marca
        {
            set
            {
                marca = value;
                this.Refresh();
            }
            get { return marca; }
        }

        public EtiquetaAviso()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int grosor = 0; //Grosor de las líneas de dibujo
            int offsetX = 0; //Desplazamiento a la derecha del texto
            int offsetY = 0; //Desplazamiento hacia abajo del texto
            // Altura de fuente, usada como referencia en varias partes
            int h = this.Font.Height;
            //Esta propiedad provoca mejoras en la apariencia o en la eficiencia
            // a la hora de dibujar
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //Dependiendo del valor de la propiedad marca dibujamos una
            //Cruz o un Círculo

            if (gradiente)
            {
                Rectangle rec = new Rectangle(0, 0, Width, Height);
                LinearGradientBrush lgb = new LinearGradientBrush(rec, colorInicial, colorFinal, 90);
                g.FillRectangle(lgb, rec);
            }


            switch (Marca)
            {
                case EMarca.Circulo:
                    grosor = 20;
                    g.DrawEllipse(new Pen(Color.Green, grosor), grosor, grosor, h, h);
                    offsetX = h + grosor;
                    offsetY = grosor;
                    break;
                case EMarca.Cruz:
                    grosor = 3;
                    Pen lapiz = new Pen(Color.Red, grosor);
                    g.DrawLine(lapiz, grosor, grosor, h, h);
                    g.DrawLine(lapiz, h, grosor, grosor, h);
                    offsetX = h + grosor;
                    offsetY = grosor / 2;
                    //Es recomendable liberar recursos de dibujo pues se
                    //pueden realizar muchos y cogen memoria
                    lapiz.Dispose();
                    break;
                case EMarca.Imagen:
                    if (imagenMarca == null)
                    {
                        return;
                    }
                    g.DrawImage(new Bitmap(ImagenMarca), 0, 0, h, h);
                    offsetX = h;
                    break;
                default:
                    break;
            }
            //Finalmente pintamos el Texto; desplazado si fuera necesario
            SolidBrush b = new SolidBrush(this.ForeColor);
            g.DrawString(this.Text, this.Font, b, offsetX + grosor, offsetY);
            Size tam = g.MeasureString(this.Text, this.Font).ToSize();
            this.Size = new Size(tam.Width + offsetX + grosor, tam.Height + offsetY * 2);
            b.Dispose();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Refresh();
        }

        // Ejercicio 2

        private Color colorInicial;
        [Category("Appearance")]
        [Description("Indica o establece el color inicial del gradiente")]
        public Color ColorInicial
        {
            set
            {
                colorInicial = value;
                if (gradiente)
                {
                    this.Refresh();
                }
            }
            get
            {
                return colorInicial;
            }
        }

        private Color colorFinal;
        [Category("Appearance")]
        [Description("Indica o establece el color final del gradiente")]
        public Color ColorFinal
        {
            set
            {
                colorFinal = value;
                if (gradiente)
                {
                    this.Refresh();
                }
            }
            get
            {
                return colorFinal;
            }
        }



        private bool gradiente;

        [Category("Appearance")]
        [Description("Establece si tiene color o no")]
        public bool Gradiente
        {
            set
            {
                gradiente = value;
                this.Refresh();
            }
            get
            {
                return gradiente;
            }
        }


        private Image imagenMarca;
        [Category("Appearance")]
        [Description("Establece la imagen en caso de que marca == Imagen")]
        public Image ImagenMarca
        {
            set
            {
                imagenMarca = value;
                if (marca == EMarca.Imagen)
                {
                    this.Refresh();
                }
            }
            get
            {
                return imagenMarca;
            }
        }


        [Category("Events")]
        [Description("Se lanza cuando se hace click sobre la marca")]
        public event EventHandler ClickEnMarca;

        protected virtual void OnClickEnMarca(EventArgs e)
        {
            ClickEnMarca?.Invoke(this, e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (marca != EMarca.Nada && e.Location.X > 0 && e.Location.Y < this.Height)
            {
                this.OnClickEnMarca(e);
            }
        }
    }
}
