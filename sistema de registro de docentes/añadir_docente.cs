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
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace sistema_de_registro_de_docentes
{
    public partial class añadir_docente : Form
    {
        public DataTable tabla;
        public añadir_docente()
        {
            InitializeComponent();
            tabla = new DataTable();
            // Agregar las columnas necesarias a la tabla
            tabla.Columns.Add("Grdo", typeof(string));
            tabla.Columns.Add("Apellido Paterno", typeof(string));
            tabla.Columns.Add("Apellido Materno", typeof(string));
            tabla.Columns.Add("Nombres", typeof(string));
            tabla.Columns.Add("CI", typeof(string));
            tabla.Columns.Add("Asignatura", typeof(string));
            tabla.Columns.Add("Semestre Academico", typeof(string));
            tabla.Columns.Add("Paralelo", typeof(string));
        }

        private bool GuardarDatosEnExcel(string grado, string apellidoPaterno, string apellidoMaterno, string nombres, string ci, string asignatura, string semestreAcademico, string paralelo)
        {
            string rutaExcel = @"E:\PROYECTO\proyecto registro de asistencia profesores\sistema de registro de docentes\docentes.xlsx";
            Excel.Application excel = new Excel.Application();
            Excel.Workbook libro = null;
            Excel.Worksheet hoja = null;

            try
            {
                // Verificar si el archivo existe, si no, crearlo
                if (!File.Exists(rutaExcel))
                {
                    libro = excel.Workbooks.Add();
                    hoja = libro.ActiveSheet;

                    // Escribir encabezados de columna
                    hoja.Cells[1, 1] = "Nº";
                    hoja.Cells[1, 2] = "Gdo";
                    hoja.Cells[1, 3] = "Apellido Paterno";
                    hoja.Cells[1, 4] = "Apellido Materno";
                    hoja.Cells[1, 5] = "Nombres";
                    hoja.Cells[1, 6] = "CI";
                    hoja.Cells[1, 7] = "Asignatura";
                    hoja.Cells[1, 8] = "Semestre Académico";
                    hoja.Cells[1, 9] = "Paralelo";
                }
                else
                {
                    libro = excel.Workbooks.Open(rutaExcel);
                    hoja = libro.ActiveSheet;
                }

                // Encontrar la siguiente fila vacía
                int filaVacia = hoja.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row + 1;

                // Escribir los datos en la fila vacía
                hoja.Cells[filaVacia, 1] = filaVacia - 1; // Número de fila
                hoja.Cells[filaVacia, 2] = grado;
                hoja.Cells[filaVacia, 3] = apellidoPaterno;
                hoja.Cells[filaVacia, 4] = apellidoMaterno;
                hoja.Cells[filaVacia, 5] = nombres;
                hoja.Cells[filaVacia, 6] = ci;
                hoja.Cells[filaVacia, 7] = asignatura;
                hoja.Cells[filaVacia, 8] = semestreAcademico;
                hoja.Cells[filaVacia, 9] = paralelo;

                libro.Save();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos en Excel: " + ex.Message);
                return false;
            }
            finally
            {
                if (libro != null)
                {
                    libro.Close();
                    excel.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(libro);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                }
            }
        }

        private void btnAñadirDocenteExcel_Click(object sender, EventArgs e)
        {
            string grado = text_grado.Text;
            string apellidoPaterno = text_ap_paterno.Text;
            string apellidoMaterno = text_ap_materno.Text;
            string nombres = text_nombres.Text;
            string ci = text_carnet.Text;
            string asignatura = text_asignatura.Text;
            string semestreAcademico = text_semestre.Text;
            string paralelo = text_paralelo.Text;

            bool registroExitoso = GuardarDatosEnExcel(grado, apellidoPaterno, apellidoMaterno, nombres, ci, asignatura, semestreAcademico, paralelo);

            if (registroExitoso)
            {
                label_mensaje_estado.Text = "Se agregó un nuevo docente con éxito.";
                label_mensaje_estado.ForeColor = Color.Green;
            }
            else
            {
                label_mensaje_estado.Text = "Error al agregar el docente.";
                label_mensaje_estado.ForeColor = Color.Red;
            }
        }
    }
}
