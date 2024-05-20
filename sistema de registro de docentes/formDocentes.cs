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
    public partial class formDocentes : Form
    {
        public DataTable tabla;
        public formDocentes()
        {
            InitializeComponent();
            abriFormHijo(new actualizarDocente());
        }

        public void GuardarDatosEnExcel()
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

                // Limpiar el contenido de la hoja
                hoja.UsedRange.Clear();

                // Escribir los datos del DataTable en la hoja
                int filaInicio = 1;
                int columnaInicio = 1;

                for (int columna = 0; columna < tabla.Columns.Count; columna++)
                {
                    hoja.Cells[filaInicio, columnaInicio + columna] = tabla.Columns[columna].ColumnName;
                }

                for (int fila = 0; fila < tabla.Rows.Count; fila++)
                {
                    for (int columna = 0; columna < tabla.Columns.Count; columna++)
                    {
                        hoja.Cells[fila + filaInicio + 1, columnaInicio + columna] = tabla.Rows[fila][columna];
                    }
                }

                // Guardar y cerrar el libro de trabajo
                libroTrabajo.Save();
                libroTrabajo.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos en Excel: " + ex.Message);
            }
            finally
            {
                // Cerrar la aplicación Excel
                excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                excel = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
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
            panel_Docente2.Controls.Add(childForm);
            panel_Docente2.Tag = childForm;
            childForm.BringToFront();

            // Si el formulario hijo es FormNuevoDocente, asignar la referencia al DataTable

            childForm.Show();
        }

        private void buttonAgregarDocente_Click(object sender, EventArgs e)
        {
            abriFormHijo(new añadir_docente());

        }

        private void buttonElminarDocente_Click(object sender, EventArgs e)
        {

        }

        private void buttonImportarDatosDocente_Click(object sender, EventArgs e)
        {

        }
    }
}
