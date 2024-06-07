using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ExcelDataReader;
using static sistema_de_registro_de_docentes.FormPrincipal;

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
            contrasena.UseSystemPasswordChar = !checkBox1.Checked;
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
            string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\sportsc.xlsx");
            rutaexcel = Path.GetFullPath(rutaexcel);

            using (var stream = File.Open(rutaexcel, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
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
                        var userRow = query.First();
                        string role = userRow.Field<string>("rol"); // Suponiendo que tienes una columna "rol" en tu Excel
                        string nombres = userRow.Field<string>("Nombre");
                        string apellidoPaterno = userRow.Field<string>("Apellido Paterno");
                        string apellidoMaterno = userRow.Field<string>("Apellido Materno");
                        User user = new User
                        {
                            Username = Usuario,
                            Role = role,
                            Nombres = nombres,
                            ApellidoPaterno = apellidoPaterno,
                            ApellidoMaterno = apellidoMaterno
                        };

                        MessageBox.Show("Bienvenido " + Usuario);
                        FormPrincipal principal = new FormPrincipal(user);
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
