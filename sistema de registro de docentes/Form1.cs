using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using ExcelDataReader;
using System.IO;

namespace sistema_de_registro_de_docentes
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tituloLogin_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                contrasena.UseSystemPasswordChar = false;
            }
            else
            {
                contrasena.UseSystemPasswordChar = true;
            }
            
        }

        private void contrasena_Enter(object sender, EventArgs e)
        {
                contrasena.Text = "";
                contrasena.UseSystemPasswordChar = true;
            
        }
        private void usuario_Enter(object sender, EventArgs e)
        {
           
        }

        private void usuario_Leave(object sender, EventArgs e)
        {
            
        }

        private void ingresar_Click(object sender, EventArgs e)
        {
            string rutaExcel = @"E:\sportsc.xlsx";

            using (var stream = File.Open(rutaExcel, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Líneas a reemplazar
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });
                    var dataTable = result.Tables["Credenciales"]; 

                    string Usuario = usuario.Text;
                    string Contrasena = contrasena.Text;
                    var query = from row in dataTable.AsEnumerable()
                                where row.Field<string>("usuario") == Usuario &&
                                      row.Field<string>("contrasena") == Contrasena
                                select row;

                    if (query.Any())
                    {
                        MessageBox.Show("Bienvenido " + Usuario);
                        FormPrincipal principal = new FormPrincipal();
                        principal.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Contraseña incorrecta");
                    }
                }
            }
        }

    }
}
