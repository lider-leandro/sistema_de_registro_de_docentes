using ClosedXML.Excel;
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
using DocumentFormat.OpenXml.Spreadsheet;
namespace sistema_de_registro_de_docentes
{
    public partial class FormDetalleDocente : Form
    {
        private DataRow usuarioActual;
        private string excelPath;
        private string carnetIdentidad;
        private string carrera;
        private string materia;

        public FormDetalleDocente()
        {
            InitializeComponent();
        }
        public void SetDocenteData(string carnetIdentidad, string excelPath, string carrera, string materia)
        {
            this.carnetIdentidad = carnetIdentidad;
            this.carrera = carrera;
            this.materia = materia;
            this.excelPath = excelPath;

            CargarDatosUsuario();
        }
        public string carnet;
        private void CargarDatosUsuario()
        {
            MessageBox.Show($"{carnetIdentidad} {carrera} {materia}");
            try
            {
                using (var workbook = new XLWorkbook(excelPath))
                {
                    var worksheet = workbook.Worksheet("Hoja2");
                    var rows = worksheet.RangeUsed().RowsUsed();
                    DataTable dt = new DataTable();
                    foreach (var row in rows)
                    {
                        // Identificación de las columnas
                        var carnetDeIdentidadColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "CI")?.Address.ColumnNumber ?? -1;
                        var expedidoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Grdo")?.Address.ColumnNumber ?? -1;
                        var nombreColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Nombres")?.Address.ColumnNumber ?? -1;
                        var apellidoPaternoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Apellido Paterno")?.Address.ColumnNumber ?? -1;
                        var apellidoMaternoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Apellido Materno")?.Address.ColumnNumber ?? -1;
                        var direccionColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Carrera")?.Address.ColumnNumber ?? -1;
                        var correoInstitucionalColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Asignatura")?.Address.ColumnNumber ?? -1;
                        var emailPersonalColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Semestre Académico")?.Address.ColumnNumber ?? -1;
                        var unidadAcademicaColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Paralelo")?.Address.ColumnNumber ?? -1;
                        var nivelAcademicoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Carga horaria")?.Address.ColumnNumber ?? -1;
                        var telefonoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Dia")?.Address.ColumnNumber ?? -1;
                        var celularColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Hora entrada")?.Address.ColumnNumber ?? -1;
                        var rolColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Dia 2")?.Address.ColumnNumber ?? -1;
                        var horaEntrada2Index = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Hora entrada 2")?.Address.ColumnNumber ?? -1;
                        var estadoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Estado")?.Address.ColumnNumber ?? -1;

                        // Verificación de las columnas necesarias
                        if (carnetDeIdentidadColIndex == -1 || expedidoColIndex == -1 || nombreColIndex == -1 || apellidoPaternoColIndex == -1 || apellidoMaternoColIndex == -1 || direccionColIndex == -1 || correoInstitucionalColIndex == -1 || emailPersonalColIndex == -1 || unidadAcademicaColIndex == -1 || nivelAcademicoColIndex == -1 || telefonoColIndex == -1 || celularColIndex == -1 || rolColIndex == -1 || estadoColIndex == -1)
                        {
                            throw new Exception("No se encontraron todas las columnas necesarias en el archivo Excel.");
                        }

                        // Mensaje de depuración para verificar que las columnas fueron encontradas
                        

                        string carnetIdentidadValue = row.Cell(carnetDeIdentidadColIndex).GetValue<string>();
                        string carreraValue = row.Cell(direccionColIndex).GetValue<string>();
                        string materiaValue = row.Cell(correoInstitucionalColIndex).GetValue<string>();
                        //MessageBox.Show($"{row.Cell(carnetDeIdentidadColIndex).GetValue<string>()}  {row.Cell(direccionColIndex).GetValue<string>()}    {row.Cell(correoInstitucionalColIndex).GetValue<string>()}");

                        // Verificación de la condición de comparación
                        if (row.Cell(carnetDeIdentidadColIndex).GetValue<string>() == carnetIdentidad && row.Cell(correoInstitucionalColIndex).GetValue<string>() == carrera && row.Cell(direccionColIndex).GetValue<string>() == materia)
                        {
                            // Mensaje de depuración para verificar que la fila coincidente fue encontrada
                            MessageBox.Show("Fila coincidente encontrada.");

                            lblCI.Text = carnetIdentidadValue;
                            lblNombre.Text = $"{row.Cell(expedidoColIndex).GetValue<string>().Trim()} {row.Cell(nombreColIndex).GetValue<string>().Trim()} {row.Cell(apellidoPaternoColIndex).GetValue<string>().Trim()} {row.Cell(apellidoMaternoColIndex).GetValue<string>().Trim()}";
                            lblDireccion.Text = carreraValue;
                            lblEmail.Text = materiaValue;
                            lblEmailPersonal.Text = row.Cell(emailPersonalColIndex).GetValue<string>().Trim();
                            lblUnidadAcademica.Text = row.Cell(unidadAcademicaColIndex).GetValue<string>().Trim();
                            lblNivelAcademico.Text = row.Cell(nivelAcademicoColIndex).GetValue<string>().Trim();
                            lblTelefono.Text = row.Cell(telefonoColIndex).GetValue<string>().Trim();
                            lblCelular.Text = row.Cell(celularColIndex).GetValue<string>().Trim();
                            lblRol.Text = $"{row.Cell(rolColIndex).GetValue<string>().Trim()} DEL SISTEMA";

                            string fileName = $"{row.Cell(nombreColIndex).GetValue<string>().Trim().ToLower()}_{row.Cell(apellidoPaternoColIndex).GetValue<string>().Trim().ToLower()}.jpg";
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
                                dt.Columns.Add(cell.Value.ToString().Trim());
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


        private void button2_Click(object sender, EventArgs e)
        {
            CambiarEstadoDocente("INACTIVO");
        }
        private void CambiarEstadoDocente(string nuevoEstado)
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
                    var worksheet = workbook.Worksheet("Hoja2");
                    var rows = worksheet.RangeUsed().RowsUsed();

                    var carnetDeIdentidadColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "CI")?.Address.ColumnNumber ?? -1;
                    var materiaColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Asignatura")?.Address.ColumnNumber ?? -1;
                    var estadoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Estado")?.Address.ColumnNumber ?? -1;
                    var carreraColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Carrera")?.Address.ColumnNumber ?? -1;

                    if (carnetDeIdentidadColIndex == -1 || materiaColIndex == -1 || estadoColIndex == -1 || carreraColIndex == -1)
                    {
                        throw new Exception("No se encontraron todas las columnas necesarias en el archivo Excel.");
                    }

                    foreach (var row in rows)
                    {
                        if (row.Cell(carnetDeIdentidadColIndex).GetValue<string>() == usuarioActual["CI"].ToString() &&
                            row.Cell(materiaColIndex).GetValue<string>() == usuarioActual["AsignaturA"].ToString() &&
                            row.Cell(carreraColIndex).GetValue<string>() == usuarioActual["Carrera"].ToString())
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
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            this.Close();
            transparente transparentForm = new transparente();
            editarDatosDocente editarDocente = new editarDatosDocente();
            transparentForm.Show();

            // Pasa los datos del usuario actual al formulario de edición
            editarDocente.SetDocenteData(usuarioActual, excelPath);

            editarDocente.ShowDialog();
            transparentForm.Close();
        }
    }
}
