using ClosedXML.Excel;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace sistema_de_registro_de_docentes
{
    public partial class cambioContraseña : Form
    {
        private DataRow usuarioActual;
        private string excelPath;

        public cambioContraseña()
        {
            InitializeComponent();
        }

        public void SetUsuarioData(DataRow usuario, string excelPath)
        {
            usuarioActual = usuario;
            this.excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\sportsc.xlsx");
        }

        private void buttonCambiarContraseña_Click(object sender, EventArgs e)
        {
            string nuevaContrasena = textBoxCambioContraseña.Text.ToString();
            if (string.IsNullOrEmpty(nuevaContrasena))
            {
                MessageBox.Show("Por favor, introduce una nueva contraseña.");
                return;
            }
            CambiarContraseñaUsuario(nuevaContrasena);
        }

        private void CambiarContraseñaUsuario(string nuevaContrasena)
        {
            if (usuarioActual == null)
            {
                MessageBox.Show("Error: El usuario no está inicializado.");
                return;
            }

            if (string.IsNullOrEmpty(excelPath) || !File.Exists(excelPath))
            {
                MessageBox.Show("Error: La ruta del archivo Excel no es válida o el archivo no existe.");
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook(excelPath))
                {
                    var worksheet = workbook.Worksheet("Credenciales");
                    var rows = worksheet.RangeUsed().RowsUsed();

                    var carnetDeIdentidadColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Carnet de identidad")?.Address.ColumnNumber ?? -1;
                    var expedidoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Expedido")?.Address.ColumnNumber ?? -1;
                    var contrasenaColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "contrasena")?.Address.ColumnNumber ?? -1;

                    if (carnetDeIdentidadColIndex == -1 || expedidoColIndex == -1 || contrasenaColIndex == -1)
                    {
                        throw new Exception("No se encontraron todas las columnas necesarias en el archivo Excel.");
                    }

                    foreach (var row in rows)
                    {
                        if (row.Cell(carnetDeIdentidadColIndex).GetValue<string>() == usuarioActual["Carnet de identidad"].ToString() &&
                            row.Cell(expedidoColIndex).GetValue<string>() == usuarioActual["Expedido"].ToString())
                        {
                            row.Cell(contrasenaColIndex).Value = nuevaContrasena;
                            break;
                        }
                    }

                    workbook.Save();
                }
                MessageBox.Show("Contraseña del usuario actualizada correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la contraseña del usuario: {ex.Message}");
            }
        }
    }
}
