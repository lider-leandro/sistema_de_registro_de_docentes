using Horarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace sistema_de_registro_de_docentes
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        

        private Form activateForm = null;
        private void abriFormHijo(Form childForm)
        {
            if (activateForm!= null)
            {
                activateForm.Close();
            }
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock= DockStyle.Fill;
            childForm.FormBorderStyle=FormBorderStyle.None;
            childForm.Dock=DockStyle.Fill;
            panelHijo.Controls.Add(childForm);
            panelHijo.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void btn_Docentes_Click(object sender, EventArgs e)
        {
            abriFormHijo(new formDocentes());
        }

        private void buttonHorarios_Click(object sender, EventArgs e)
        {
            abriFormHijo(new ASIGNACION_HORARIOS());
        }

        private void buttonRetrasos_Click(object sender, EventArgs e)
        {
            abriFormHijo(new formRetrasos());
        }
    }
}
