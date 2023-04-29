using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Base_de_Datos
{
    public partial class Form1 : Form
    {
        //Campos privados
        //Para almacenar el boton actual y un panel
        //Para crear un borde izquierdo al boton
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        //Almacena el hijo actual (Form)
        private Form currentChildForm;

        public Form1()
        {
            InitializeComponent();
            //Inicializamos el panel(para que aparezca el color a un lado)
            leftBorderBtn = new Panel();
            //Asignando el tamano equivalente a la altura boton
            //El ancho que querramos
            leftBorderBtn.Size = new Size(7, 60);
            //agrgerar el panel a los botones del menu
            panelMenu.Controls.Add(leftBorderBtn);

            //Form, quitar la barra de menu
            this.Text = string.Empty;
            this.ControlBox = false;
            //Para evitar el parpadeo en los gráficos del formulario
            this.DoubleBuffered = true;
           
        }
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            //public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        //Metodos
        //Este resalta el boton activo,
        //como parametros un objeto del boton remitente
        //y un color
        private void ActivateButton(object senderBtn, Color color)
        {
            //Condicion diferente a nulo
            if (senderBtn != null)
            {
                //Se llama primero a este método para desactivar el
                //Boton anterior
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                //Cambiar el color de fondo del boton y el texto
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                //Se alinea el texto al centro
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                //Cambiamos la relacion de la imagen con le texto
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                //Icono a la derecha
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //borde izquierdo del boton
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //El icono de la barra de titulo cambie al icono del 
                //boton seleccionado y el color
                //ibChildForm.IconChar = currentBtn.IconChar;
                //ibChildForm.IconColor = color;
            }
        }

        //Desactivar el resaltado del boton
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                //Se regresa la configuración por defecto del boton
                currentBtn.BackColor = Color.FromArgb(37, 27, 55);
                currentBtn.ForeColor = Color.FromArgb(255, 236, 239);
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(255, 236, 239);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        //Metodo para abrir los nuevos formularios en donde
        //Se pasa como parámetro un formulario hijo
        private void OpenChildForm(Form childForm)
        {
            //Condición para abrir solo un formulario
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }

            currentChildForm = childForm;
            //Se indica que no es formulario de nivel superior
            childForm.TopLevel = false;
            //Se quitan los bordes
            childForm.FormBorderStyle = FormBorderStyle.None;
            //Llenar el contenedor
            childForm.Dock = DockStyle.Fill;
            //Agregamos el formulario hijo a los
            //controles del panel Desktop
            //y asosciamos los datos del formulario
            //al panel
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //lblTituloChildForm.Text = childForm.Text;
        }
        private void Reset()
        {
            //Deshabilita el boton al dar clic en el logo
            DisableButton();
            leftBorderBtn.Visible = false;
            //Regresa el icono del panel de titulo al 
            //inicial con los valores por defecto
            ibChildForm.IconChar = IconChar.Home;
            ibChildForm.IconColor = Color.FromArgb(255, 236, 239); ;
            ibChildForm.Text = "Inicio";
        }

        private void ibAltas_Click(object sender, EventArgs e)
        {
            
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            //Se envía como parámetros el objeto remitente del evento
            //y el color
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new Formularios.Gestion_de_Recibos());
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            //Se envía como parámetros el objeto remitente del evento
            //y el color
            ActivateButton(sender, RGBColors.color1);
            OpenChildForm(new Formularios.Home());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenChildForm(new Formularios.Home());
        }
    }
}
