using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using ClosedXML.Excel;
using System.IO;

namespace sistema_de_registro_de_docentes
{
    public partial class formDocentes : Form
    {
        private DataTable tabla;
        private string rutaexcel;

        public formDocentes()
        {
            InitializeComponent();
            rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\lista_doc.xlsx");
            CargarDatosDesdeExcel();

            // Ajustar las columnas del DataGridView para llenar el espacio disponible
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void CargarDatosDesdeExcel()
        {
            // Combina con la ruta adicional hasta llegar a la carpeta "sistema de registro de docentes"
            rutaexcel = Path.GetFullPath(rutaexcel);

            try
            {
                using (var stream = File.Open(rutaexcel, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        // Crear una copia del DataTable original
                        DataTable originalDataTable = dataSet.Tables[0].Copy();

                        // Filtrar las columnas que deseas mostrar
                        DataTable filteredDataTable = new DataTable();
                        foreach (string columnName in new string[] { "Nº", "Grdo", "Apellido Paterno", "Apellido Materno", "Nombres", "CI", "Asignatura", "Semestre Académico", "Paralelo", "Estado" })
                        {
                            if (originalDataTable.Columns.Contains(columnName))
                            {
                                filteredDataTable.Columns.Add(columnName);
                            }
                        }

                        foreach (DataRow row in originalDataTable.Rows)
                        {
                            DataRow newRow = filteredDataTable.NewRow();
                            foreach (string columnName in filteredDataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName))
                            {
                                newRow[columnName] = row[columnName];
                            }
                            filteredDataTable.Rows.Add(newRow);
                        }

                        // Asignar la tabla filtrada al origen de datos del DataGridView
                        dataGridView1.DataSource = filteredDataTable;
                        tabla = filteredDataTable; // Guardar la tabla para su posterior uso
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos desde el archivo Excel: {ex.Message}");
            }
        }

        public void GuardarDatosEnExcel()
        {
            // Combina con la ruta adicional hasta llegar a la carpeta "sistema de registro de docentes"
            rutaexcel = Path.GetFullPath(rutaexcel);

            try
            {
                using (var workbook = new XLWorkbook(rutaexcel))
                {
                    var worksheet = workbook.Worksheet("Hoja2");

                    // Limpiar el contenido de la hoja
                    worksheet.Clear();

                    // Escribir los datos del DataTable en la hoja
                    int filaInicio = 1;
                    int columnaInicio = 1;

                    // Escribir los nombres de las columnas
                    for (int columna = 0; columna < tabla.Columns.Count; columna++)
                    {
                        worksheet.Cell(filaInicio, columnaInicio + columna).Value = tabla.Columns[columna].ColumnName;
                    }

                    // Escribir los datos de las filas
                    for (int fila = 0; fila < tabla.Rows.Count; fila++)
                    {
                        for (int columna = 0; columna < tabla.Columns.Count; columna++)
                        {
                            worksheet.Cell(fila + filaInicio + 1, columnaInicio + columna).Value = tabla.Rows[fila][columna]?.ToString() ?? string.Empty;
                        }
                    }

                    // Guardar el libro de trabajo
                    workbook.Save();
                }
                MessageBox.Show("Datos guardados en el archivo Excel correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos en Excel: " + ex.Message);
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

            panel_Docente2.Controls.Add(childForm);
            panel_Docente2.Tag = childForm;
            childForm.BringToFront();

            childForm.Show();
        }

        private void buttonAgregarDocente_Click(object sender, EventArgs e)
        {
            transparente transparentForm = new transparente();
            añadir_docente emergenteDocentes = new añadir_docente();
            transparentForm.Show();
            emergenteDocentes.ShowDialog();
            transparentForm.Close();
        }

        private void buttonElminarDocente_Click(object sender, EventArgs e)
        {
            // Implementar lógica para eliminar un docente
        }

        private void buttonImportarDatosDocente_Click(object sender, EventArgs e)
        {
            // Implementar lógica para importar datos de docentes
        }

        private void leer_Click(object sender, EventArgs e)
        {
            CargarDatosDesdeExcel();
            abriFormHijo(new formDocentes());
        }
    }
}
