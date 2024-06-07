
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_de_registro_de_docentes
{
    public partial class FormPrincipal : Form
    {
        private User currentUser;

        public FormPrincipal(User user)
        {
            InitializeComponent();
            this.currentUser = user;

            // Mostrar u ocultar el botón basado en el rol del usuario
            btnUsuarios.Visible = user.Role == "ADMINISTRADOR";

            // Mostrar la información del usuario
            MostrarInformacionUsuario(user);
        }

        private void MostrarInformacionUsuario(User user)
        {
            // Mostrar el nombre, apellido y rol del usuario
            lblNombreApellido.Text = $"{user.Nombres} {user.ApellidoPaterno} {user.ApellidoMaterno}";
            lblRol.Text = user.Role;

            // Cargar y mostrar la imagen del usuario
            string fileName = $"{user.Nombres}_{user.ApellidoPaterno}.jpg";
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\imagenes_usuarios", fileName);

            if (File.Exists(imagePath))
            {
                pictureBoxUsuario.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("Imagen no encontrada para el usuario.");
            }
        }

        private Form activateForm = null;
        private void abriFormHijo(Form childForm)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelHijo.Controls.Add(childForm);
            panelHijo.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_Docentes_Click(object sender, EventArgs e)
        {
            // Cambiar color del texto y la imagen del botón
            btn_Docentes.ForeColor = Color.Blue; // Cambia al color que desees
            iconodocente.Image = Properties.Resources.usuariocolor; // Cambia a la imagen que desees

            // Restaurar el color y la imagen de otros botones
            ResetButtons(btn_Docentes);
            abriFormHijo(new formDocentes());
        }

        private void buttonHorarios_Click(object sender, EventArgs e)
        {
            // Cambiar color del texto y la imagen del botón
            buttonHorarios.ForeColor = Color.Blue; // Cambia al color que desees
            iconohorario.Image = Properties.Resources.horariocolor; // Cambia a la imagen que desees

            // Restaurar el color y la imagen de otros botones
            ResetButtons(buttonHorarios);
            abriFormHijo(new formHorarios());
            
        }

        private void buttonRetrasos_Click(object sender, EventArgs e)
        {
            // Cambiar color del texto y la imagen del botón
            buttonRetrasos.ForeColor = Color.Blue; // Cambia al color que desees
            iconoretraso.Image = Properties.Resources.horacaducadocolor; // Cambia a la imagen que desees

            // Restaurar el color y la imagen de otros botones
            ResetButtons(buttonRetrasos);
            abriFormHijo(new formRetrasos());
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            // Cambiar color del texto y la imagen del botón
            button5.ForeColor = Color.Blue; // Cambia al color que desees
            iconomanual.Image = Properties.Resources.manualcolor; // Cambia a la imagen que desees

            // Restaurar el color y la imagen de otros botones
            ResetButtons(button5);
        }

        private void ResetButtons(Button activeButton)
        {
            // Restablecer el color y la imagen de otros botones
            foreach (Control control in panelMenuLateral.Controls)
            {
                if (control is Button && control != activeButton)
                {
                    Button button = (Button)control;
                    button.ForeColor = Color.Black; // Color original

                    // Imagen original
                    if (button == btn_Docentes)
                        iconodocente.Image = Properties.Resources.usuario;
                    else if (button == buttonHorarios)
                        iconohorario.Image = Properties.Resources.horario;
                    else if (button == buttonRetrasos)
                        iconoretraso.Image = Properties.Resources.horacaducado;
                    else if (button == button5)
                        iconomanual.Image = Properties.Resources.manual;
                }
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            abriFormHijo(new formUsuarios());
        }
       
        public class User
        {
            public string Username { get; set; }
            public string Role { get; set; }
            public string Nombres { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
        }
    }
}
