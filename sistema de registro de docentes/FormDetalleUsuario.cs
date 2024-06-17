using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ClosedXML.Excel;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace sistema_de_registro_de_docentes
{
    public partial class FormDetalleUsuario : Form
    {
        private DataRow usuarioActual;
        private string excelPath;
        private string carnetIdentidad;
        private string expedido;

        public FormDetalleUsuario()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Habilitar doble búfer para reducir el parpadeo
        }public void SetUsuarioData(string carnetIdentidad, string expedido, string excelPath)
        {
            this.carnetIdentidad = carnetIdentidad;
            this.expedido = expedido;
            this.excelPath = excelPath;

            CargarDatosUsuario();
        }
        public string carnet;

        private void CargarDatosUsuario()
        {
            try
            {
                using (var workbook = new XLWorkbook(excelPath))
                {
                    var worksheet = workbook.Worksheet("Credenciales");
                    var rows = worksheet.RangeUsed().RowsUsed();
                    DataTable dt = new DataTable();

                    foreach (var row in rows)
                    {
                        var carnetDeIdentidadColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Carnet de identidad")?.Address.ColumnNumber ?? -1;
                        var expedidoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Expedido")?.Address.ColumnNumber ?? -1;
                        var nombreColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Nombre")?.Address.ColumnNumber ?? -1;
                        var apellidoPaternoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Apellido Paterno")?.Address.ColumnNumber ?? -1;
                        var apellidoMaternoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Apellido Materno")?.Address.ColumnNumber ?? -1;
                        var direccionColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Direccion")?.Address.ColumnNumber ?? -1;
                        var correoInstitucionalColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Correo Institucional")?.Address.ColumnNumber ?? -1;
                        var emailPersonalColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Email Personal")?.Address.ColumnNumber ?? -1;
                        var unidadAcademicaColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Unidad Academica")?.Address.ColumnNumber ?? -1;
                        var nivelAcademicoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Nivel Academico")?.Address.ColumnNumber ?? -1;
                        var telefonoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Telefono")?.Address.ColumnNumber ?? -1;
                        var celularColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Celular")?.Address.ColumnNumber ?? -1;
                        var rolColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "rol")?.Address.ColumnNumber ?? -1;
                        var estadoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Estado")?.Address.ColumnNumber ?? -1;

                        if (carnetDeIdentidadColIndex == -1 || expedidoColIndex == -1 || nombreColIndex == -1 || apellidoPaternoColIndex == -1 || apellidoMaternoColIndex == -1 || direccionColIndex == -1 || correoInstitucionalColIndex == -1 || emailPersonalColIndex == -1 || unidadAcademicaColIndex == -1 || nivelAcademicoColIndex == -1 || telefonoColIndex == -1 || celularColIndex == -1 || rolColIndex == -1 || estadoColIndex == -1)
                        {
                            throw new Exception("No se encontraron todas las columnas necesarias en el archivo Excel.");
                        }

                        if (row.Cell(carnetDeIdentidadColIndex).GetValue<string>() == carnetIdentidad && row.Cell(expedidoColIndex).GetValue<string>() == expedido)
                        {
                            lblCI.Text = $"{row.Cell(carnetDeIdentidadColIndex).GetValue<string>()} {row.Cell(expedidoColIndex).GetValue<string>()}";
                            lblNombre.Text = $"{row.Cell(nombreColIndex).GetValue<string>()} {row.Cell(apellidoPaternoColIndex).GetValue<string>()} {row.Cell(apellidoMaternoColIndex).GetValue<string>()}";
                            lblDireccion.Text = row.Cell(direccionColIndex).GetValue<string>();
                            lblEmail.Text = row.Cell(correoInstitucionalColIndex).GetValue<string>();
                            lblEmailPersonal.Text = row.Cell(emailPersonalColIndex).GetValue<string>();
                            lblUnidadAcademica.Text = row.Cell(unidadAcademicaColIndex).GetValue<string>();
                            lblNivelAcademico.Text = row.Cell(nivelAcademicoColIndex).GetValue<string>();
                            lblTelefono.Text = row.Cell(telefonoColIndex).GetValue<string>();
                            lblCelular.Text = row.Cell(celularColIndex).GetValue<string>();
                            lblRol.Text = $"{row.Cell(rolColIndex).GetValue<string>()} DEL SISTEMA";

                            string fileName = $"{row.Cell(nombreColIndex).GetValue<string>().ToLower()}_{row.Cell(apellidoPaternoColIndex).GetValue<string>().ToLower()}.jpg";
                            string rutaImagen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\imagenes_usuarios", fileName);

                            if (File.Exists(rutaImagen))
                            {
                                pictureBoxFoto.Image = Image.FromFile(rutaImagen);
                            }
                            else
                            {
                                pictureBoxFoto.Image = Properties.Resources.perfilPorDefecto;
                            }

                            dt.Columns.Clear();
                            foreach (var cell in worksheet.FirstRow().Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }

                            var newRow = dt.NewRow();
                            foreach (var cell in row.Cells())
                            {
                                newRow[cell.Address.ColumnNumber - 1] = cell.Value;
                            }
                            dt.Rows.Add(newRow);
                            usuarioActual = dt.Rows[0];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los datos del usuario: {ex.Message}");
            }
        }

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            CambiarEstadoUsuario("INACTIVO");
        }

        private void CambiarEstadoUsuario(string nuevoEstado)
        {
            try
            {
                usuarioActual["Estado"] = nuevoEstado;
                GuardarCambiosEnExcel();
                MessageBox.Show("Estado del usuario actualizado correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el estado del usuario: {ex.Message}");
            }
        }
        
        private void GuardarCambiosEnExcel()
        {
            try
            {
                using (var workbook = new XLWorkbook(excelPath))
                {
                    var worksheet = workbook.Worksheet("Credenciales");
                    var rows = worksheet.RangeUsed().RowsUsed();

                    var carnetDeIdentidadColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Carnet de identidad")?.Address.ColumnNumber ?? -1;
                    var expedidoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Expedido")?.Address.ColumnNumber ?? -1;
                    var estadoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Estado")?.Address.ColumnNumber ?? -1;

                    if (carnetDeIdentidadColIndex == -1 || expedidoColIndex == -1 || estadoColIndex == -1)
                    {
                        throw new Exception("No se encontraron todas las columnas necesarias en el archivo Excel.");
                    }

                    foreach (var row in rows)
                    {
                        if (row.Cell(carnetDeIdentidadColIndex).GetValue<string>() == usuarioActual["Carnet de identidad"].ToString() &&
                            row.Cell(expedidoColIndex).GetValue<string>() == usuarioActual["Expedido"].ToString())
                        {
                            row.Cell(estadoColIndex).Value = usuarioActual["Estado"].ToString();
                            break;
                        }
                    }

                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios en el archivo Excel: {ex.Message}");
            }
        }

        private void buttonCambiarContraseña_Click(object sender, EventArgs e)
        {
            this.Close();
            transparente transparentForm = new transparente();
            cambioContraseña CambioDeContraseña = new cambioContraseña();
            transparentForm.Show();
            CambioDeContraseña.SetUsuarioData(usuarioActual, excelPath);
            CambioDeContraseña.ShowDialog();
            transparentForm.Close();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            this.Close();
            transparente transparentForm = new transparente();
            editarDatosUsuario editarUsuario = new editarDatosUsuario();
            transparentForm.Show();

            // Pasa los datos del usuario actual al formulario de edición
            editarUsuario.SetUsuarioData(usuarioActual, excelPath);

            editarUsuario.ShowDialog();
            transparentForm.Close();
          
        }

    }
}
