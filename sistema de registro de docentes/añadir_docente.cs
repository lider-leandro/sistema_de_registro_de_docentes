using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ExcelDataReader;
using Excel = Microsoft.Office.Interop.Excel;

namespace sistema_de_registro_de_docentes
{
    public partial class añadir_docente : Form
    {
        public DataTable tabla;
        private DataSet dataSet;
        public añadir_docente()
        {
            InitializeComponent();
            tabla = new DataTable();
            // Agregar las columnas necesarias a la tabla
            tabla.Columns.Add("Grado", typeof(string));
            tabla.Columns.Add("Apellido Paterno", typeof(string));
            tabla.Columns.Add("Apellido Materno", typeof(string));
            tabla.Columns.Add("Nombres", typeof(string));
            tabla.Columns.Add("CI", typeof(string));
            tabla.Columns.Add("Asignatura", typeof(string));
            tabla.Columns.Add("Semestre Académico", typeof(string));
            tabla.Columns.Add("Paralelo", typeof(string));
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            CargarHojasDesdeExcel();

        }

        private bool GuardarDatosEnExcel(string grado, string apellidoPaterno, string apellidoMaterno, string nombres, string ci, string carrera, List<string> semestresAcademicos, string paralelo, Dictionary<string, List<string>> asignaturasPorSemestre)
        {
            string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\lista_doc.xlsx");
            rutaexcel = Path.GetFullPath(rutaexcel);
            Excel.Application excel = new Excel.Application();
            Excel.Workbook libro = null;
            Excel.Worksheet hoja = null;

            try
            {
                if (!File.Exists(rutaexcel))
                {
                    libro = excel.Workbooks.Add();
                    hoja = libro.ActiveSheet;

                    hoja.Cells[1, 1] = "Nº";
                    hoja.Cells[1, 2] = "Grado";
                    hoja.Cells[1, 3] = "Apellido Paterno";
                    hoja.Cells[1, 4] = "Apellido Materno";
                    hoja.Cells[1, 5] = "Nombres";
                    hoja.Cells[1, 6] = "CI";
                    hoja.Cells[1, 16] = "Carrera";
                    hoja.Cells[1, 7] = "Asignatura";
                    hoja.Cells[1, 8] = "Semestre Académico";
                    hoja.Cells[1, 9] = "Paralelo";
                }
                else
                {
                    libro = excel.Workbooks.Open(rutaexcel);
                    hoja = libro.ActiveSheet;
                }

                Excel.Range ultimaCelda = hoja.Cells[hoja.Rows.Count, 5];
                Excel.Range filaVacia = ultimaCelda.End[Excel.XlDirection.xlUp].Offset[1, 0];
                int filaNumero = filaVacia.Row;

                foreach (var semestre in semestresAcademicos)
                {
                    if (asignaturasPorSemestre.ContainsKey(semestre))
                    {
                        foreach (var asignatura in asignaturasPorSemestre[semestre])
                        {
                            if (checkedListBoxAsignatura.CheckedItems.Contains(asignatura))
                            {
                                hoja.Cells[filaNumero, 1] = filaNumero - 1; // Número de fila
                                hoja.Cells[filaNumero, 2] = grado;
                                hoja.Cells[filaNumero, 3] = apellidoPaterno;
                                hoja.Cells[filaNumero, 4] = apellidoMaterno;
                                hoja.Cells[filaNumero, 5] = nombres;
                                hoja.Cells[filaNumero, 6] = ci;
                                hoja.Cells[filaNumero, 16] = carrera;
                                hoja.Cells[filaNumero, 7] = asignatura;
                                hoja.Cells[filaNumero, 8] = semestre;
                                hoja.Cells[filaNumero, 9] = paralelo;
                                filaNumero++;
                            }
                        }
                    }
                }

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

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            string grado = textBoxGrado.Text;
            string apellidoPaterno = textBoxApellidoPaterno.Text;
            string apellidoMaterno = textBoxApellidoMaterno.Text;
            string nombres = textBoxNombres.Text;
            string ci = textBoxCI.Text;
            string paralelo = textBoxParalelo.Text;

            List<string> carrerasSeleccionadas = new List<string>();
            foreach (var item in checkedListBoxCarrera.CheckedItems)
            {
                carrerasSeleccionadas.Add(item.ToString());
            }

            List<string> semestresSeleccionados = new List<string>();
            foreach (var item in checkedListBoxSemestreAcademico.CheckedItems)
            {
                semestresSeleccionados.Add(item.ToString());
            }

            var asignaturasPorSemestre = CargarAsignaturasPorSemestre(checkedListBoxCarrera.SelectedItem.ToString());

            foreach (var carrera in carrerasSeleccionadas)
            {
                GuardarDatosEnExcel(grado, apellidoPaterno, apellidoMaterno, nombres, ci, carrera, semestresSeleccionados, paralelo, asignaturasPorSemestre);
            }
        }

        private void CargarHojasDesdeExcel()
        {
            string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\materias.xlsx");

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(rutaexcel, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = false
                        }
                    });
                    dataSet = result;
                }
            }

            foreach (DataTable table in dataSet.Tables)
            {
                checkedListBoxCarrera.Items.Add(table.TableName);
            }
        }

        private Dictionary<string, List<string>> CargarAsignaturasPorSemestre(string carreraSeleccionada)
        {
            var asignaturasPorSemestre = new Dictionary<string, List<string>>();
            DataTable hoja = dataSet.Tables[carreraSeleccionada];

            for (int col = 0; col < hoja.Columns.Count; col++)
            {
                string semestre = hoja.Rows[0][col].ToString();
                if (!asignaturasPorSemestre.ContainsKey(semestre))
                {
                    asignaturasPorSemestre[semestre] = new List<string>();
                }

                for (int row = 1; row < hoja.Rows.Count; row++)
                {
                    var asignatura = hoja.Rows[row][col];
                    if (asignatura != null)
                    {
                        asignaturasPorSemestre[semestre].Add(asignatura.ToString());
                    }
                }
            }

            return asignaturasPorSemestre;
        }

        private void checkedListBoxCarrera_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkedListBoxSemestreAcademico.Items.Clear();
            checkedListBoxAsignatura.Items.Clear();

            if (e.NewValue == CheckState.Checked)
            {
                string carreraSeleccionada = checkedListBoxCarrera.Items[e.Index].ToString();
                DataTable table = dataSet.Tables[carreraSeleccionada];

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    string semestre = table.Rows[0][i].ToString();
                    if (!string.IsNullOrEmpty(semestre) && !checkedListBoxSemestreAcademico.Items.Contains(semestre))
                    {
                        checkedListBoxSemestreAcademico.Items.Add(semestre);
                    }
                }
            }
        }

        private void checkedListBoxSemestreAcademico_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            checkedListBoxAsignatura.Items.Clear();

            foreach (var checkedItem in checkedListBoxSemestreAcademico.CheckedItems)
            {
                string semestreSeleccionado = checkedItem.ToString();
                foreach (var checkedCarrera in checkedListBoxCarrera.CheckedItems)
                {
                    string carreraSeleccionada = checkedCarrera.ToString();
                    DataTable table = dataSet.Tables[carreraSeleccionada];

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (table.Rows[0][i].ToString() == semestreSeleccionado)
                        {
                            for (int j = 1; j < table.Rows.Count; j++)
                            {
                                var asignatura = table.Rows[j][i].ToString();
                                if (!string.IsNullOrEmpty(asignatura) && !checkedListBoxAsignatura.Items.Contains(asignatura))
                                {
                                    checkedListBoxAsignatura.Items.Add(asignatura);
                                }
                            }
                        }
                    }
                }
            }

            if (e.NewValue == CheckState.Checked)
            {
                string semestreSeleccionado = checkedListBoxSemestreAcademico.Items[e.Index].ToString();
                foreach (var checkedCarrera in checkedListBoxCarrera.CheckedItems)
                {
                    string carreraSeleccionada = checkedCarrera.ToString();
                    DataTable table = dataSet.Tables[carreraSeleccionada];

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (table.Rows[0][i].ToString() == semestreSeleccionado)
                        {
                            for (int j = 1; j < table.Rows.Count; j++)
                            {
                                var asignatura = table.Rows[j][i].ToString();
                                if (!string.IsNullOrEmpty(asignatura) && !checkedListBoxAsignatura.Items.Contains(asignatura))
                                {
                                    checkedListBoxAsignatura.Items.Add(asignatura);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
