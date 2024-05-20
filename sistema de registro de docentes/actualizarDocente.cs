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
    public partial class actualizarDocente : Form
    {
        public actualizarDocente()
        {
            InitializeComponent();
            CargarDatosDesdeExcel();
        }
        private void CargarDatosDesdeExcel()
        {
            string rutaExcel = @"E:\PROYECTO\proyecto registro de asistencia profesores\sistema de registro de docentes\docentes.xlsx";

            // Crear una instancia de la aplicación Excel
            Excel.Application excel = new Excel.Application();

            try
            {
                // Abrir el libro de trabajo
                Excel.Workbook libroTrabajo = excel.Workbooks.Open(rutaExcel);

                // Seleccionar la hoja deseada
                Excel.Worksheet hoja = libroTrabajo.Sheets["Docentes"];

                // Obtener el rango de datos
                Excel.Range rango = hoja.UsedRange;

                // Crear un objeto DataTable para almacenar los datos
                DataTable tabla = new DataTable();

                // Agregar columnas al DataTable según el rango de datos
                for (int columna = 1; columna <= rango.Columns.Count; columna++)
                {
                    tabla.Columns.Add(rango.Cells[1, columna].Value2.ToString());
                }

                // Agregar filas al DataTable con los datos del rango
                for (int fila = 2; fila <= rango.Rows.Count; fila++)
                {
                    DataRow dataRow = tabla.NewRow();
                    for (int columna = 1; columna <= rango.Columns.Count; columna++)
                    {
                        dataRow[columna - 1] = rango.Cells[fila, columna].Value2;
                    }
                    tabla.Rows.Add(dataRow);
                }

                // Asignar el DataTable como origen de datos del DataGridView
                dataGridView1.DataSource = tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos desde Excel: " + ex.Message);
            }
            finally
            {
                // Cerrar el libro de trabajo y la aplicación Excel
                excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                excel = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
