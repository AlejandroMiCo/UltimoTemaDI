﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NuevosComponentes
{
    [DefaultProperty("TextLbl"), DefaultEvent("Load")]
    public partial class LabelTextBox : UserControl
    {
        public enum EPosicion
        {
            IZQUIERDA,
            DERECHA,
        }

        private EPosicion posicion = EPosicion.IZQUIERDA;

        [Category("Mis Propiedades")]
        [Description("Indica si la Label se sitúa a la IZQUIERDA o DERECHA del Textbox")]
        public EPosicion Posicion
        {
            set
            {
                if (Enum.IsDefined(typeof(EPosicion), value))
                {
                    posicion = value;
                    this.Refresh();
                    OnPosicionChanged(EventArgs.Empty);
                }
                else
                {
                    throw new InvalidEnumArgumentException();
                }
            }
            get { return posicion; }
        }

        private void recolocar()
        {
            this.Width = txt.Width + lbl.Width + Separacion;
            switch (posicion)
            {
                case EPosicion.IZQUIERDA:
                    //Establecemos posición del componente lbl
                    lbl.Location = new Point(0, 0);
                    // Establecemos posición componente txt
                    txt.Location = new Point(lbl.Width + Separacion, 0);
                    //Establecemos ancho del Textbox
                    //(la label tiene ancho por autosize)
                    //Establecemos altura del componente
                    this.Height = Math.Max(txt.Height, lbl.Height);
                    break;
                case EPosicion.DERECHA:
                    //Establecemos posición del componente txt
                    txt.Location = new Point(0, 0);
                    //Establecemos ancho del Textbox 
                    //Establecemos posición del componente lbl
                    lbl.Location = new Point(txt.Width + Separacion, 0);
                    //Establecemos altura del componente (Puede sacarse del switch)
                    this.Height = Math.Max(txt.Height, lbl.Height);
                    break;
            }
        }

        // Esta función has de enlazarla con el evento SizeChanged.
        // Sería necesario también tener en cuenta otros eventos como FontChanged
        // que aquí nos saltamos.
        private void LabelTextBox_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //Pixeles de separación entre label y textbox
        private int separacion = 0;

        [Category("Mis Propiedades")] // O se puede meter en categoría Design.
        [Description("Píxels de separación entre Label y Textbox")]
        public int Separacion
        {
            set
            {
                if (value >= 0)
                {
                    separacion = value;
                    this.Refresh();
                    OnSeparacionChanged(EventArgs.Empty);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            get { return separacion; }
        }

        [Category("Mis Propiedades")] // O se puede meter en categoría Appearance.
        [Description("Texto asociado a la Label del control")]
        public string TextLbl
        {
            set
            {
                lbl.Text = value;
                this.Refresh();
            }
            get { return lbl.Text; }
        }

        [Category("Mis Propiedades")]
        [Description("Texto asociado al TextBox del control")]
        public string TextTxt
        {
            set { txt.Text = value; }
            get { return txt.Text; }
        }

        public LabelTextBox()
        {
            InitializeComponent();
            this.Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Refresh();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            this.OnTxtChanged(e);
        }


        [Category("La propiedad cambió")]
        [Description("Se lanza cuando la propiedad Posicion cambia")]
        public event EventHandler PosicionChanged;

        protected virtual void OnPosicionChanged(EventArgs e)
        {
            if (PosicionChanged != null)
            {
                PosicionChanged(this, e);
            }
        }



        [Category("La propiedad cambió")]
        [Description("Se lanza cuando la propiedad Separacion cambia")]
        public event EventHandler SeparacionChanged;

        protected virtual void OnSeparacionChanged(EventArgs e)
        {
            if (SeparacionChanged != null)
            {
                SeparacionChanged(this, e);
            }
        }


        //[Category("La propiedad cambió")]
        //[Description("Se lanza cuando se suelta una tecla")]
        //public event EventHandler LtKeyUp;

        //protected virtual void OnKeyUp(EventArgs e)
        //{
        //    if (LtKeyUp != null)
        //    {
        //        LtKeyUp(this, e);
        //    }
        //}

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            this.BackColor = this.BackColor == DefaultBackColor ? Color.Red : DefaultBackColor;
            this.OnKeyUp(e);
        }


        [Category("La propiedad cambió")]
        [Description("Se lanza cuando la propiedad texto cambia")]
        public event EventHandler TxtChanged;

        protected virtual void OnTxtChanged(EventArgs e)
        {
            if (TxtChanged != null)
            {
                TxtChanged(this, e);
            }
        }

        private void txt_TextChanged_1(object sender, EventArgs e)
        {
            this.OnTxtChanged(e);
        }


        [Category("Mis Propiedades")]
        [Description("Password char asociado al TextBox del control")]
        public char PswChr
        {
            set { txt.PasswordChar = value; }
            get { return txt.PasswordChar; }
        }


        // Ejercicio 2 a

        private bool isUnderlined;

        [Category("Mis Propiedades")]
        [Description("Indica si labelTextBox esta subrayado")]
        public bool Underline
        {
            set
            {
                isUnderlined = Underline;
                this.Refresh();
                OnUnderlineChange(EventArgs.Empty);
            }
            get { return isUnderlined; }

        }

        [Category("Events")]
        [Description("Se lanza al cambiar la propiedad")]
        public event EventHandler UnderlineChanged;


        protected virtual void OnUnderlineChange(EventArgs e)
        {
            UnderlineChanged?.Invoke(this, e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            recolocar();
            if (isUnderlined)
            {
                e.Graphics.DrawLine(new Pen(Color.Violet), lbl.Left, this.Height - 1, lbl.Left + lbl.Width, this.Height - 1);
            }
        }
    }
}
