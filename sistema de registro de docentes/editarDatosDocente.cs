using ClosedXML.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace sistema_de_registro_de_docentes
{
    public partial class editarDatosDocente : Form
    {
        private DataRow usuarioActual;
        private string excelPath;
        private string carnetIdentidadOriginal;
        private string materiaOriginal;
        private string carreraOriginal;
        public DataTable tabla;
        private DataSet dataSet;
        public editarDatosDocente()
        {
            InitializeComponent();
            CargarHojasDesdeExcel();
        }
        public void SetDocenteData(DataRow usuario, string excelPath)
        {
            this.usuarioActual = usuario;
            this.excelPath = excelPath;
            CargarDatosUsuario();
        }
        private void CargarDatosUsuario()
        {
            if (usuarioActual != null)
            {
                textBoxNombres.Text = usuarioActual["Nombres"].ToString();
                textBoxApellidoPaterno.Text = usuarioActual["Apellido Paterno"].ToString();
                textBoxApellidoMaterno.Text = usuarioActual["Apellido Materno"].ToString();
                textBoxGrado.Text = usuarioActual["Grdo"].ToString();
                textBoxCI.Text = usuarioActual["CI"].ToString();
                textBoxParalelo.Text = usuarioActual["Paralelo"].ToString();
                carnetIdentidadOriginal = textBoxCI.Text;
                materiaOriginal = usuarioActual["Asignatura"].ToString();
                carreraOriginal = usuarioActual["Carrera"].ToString();
                checkBoxEstado.Checked = usuarioActual["Estado"].ToString() == "ACTIVO";

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

        private void btnAñadirDocenteExcel_Click(object sender, EventArgs e)
        {
            string grado = textBoxGrado.Text;
            string apellidoPaterno = textBoxApellidoPaterno.Text;
            string apellidoMaterno = textBoxApellidoMaterno.Text;
            string nombres = textBoxNombres.Text;
            string ci = textBoxCI.Text;
            string paralelo = textBoxParalelo.Text;


            // Obtener la carrera seleccionada (el primer valor seleccionado)
            string carreraSeleccionada = "";
            if (checkedListBoxCarrera.CheckedItems.Count > 0)
            {
                carreraSeleccionada = checkedListBoxCarrera.CheckedItems[0].ToString();
            }

            // Obtener el semestre académico seleccionado (el primer valor seleccionado)
            string semestreSeleccionado = "";
            if (checkedListBoxSemestreAcademico.CheckedItems.Count > 0)
            {
                semestreSeleccionado = checkedListBoxSemestreAcademico.CheckedItems[0].ToString();
            }

            // Obtener la asignatura seleccionada (el primer valor seleccionado)
            string asignaturaSeleccionada = "";
            if (checkedListBoxAsignatura.CheckedItems.Count > 0)
            {
                asignaturaSeleccionada = checkedListBoxAsignatura.CheckedItems[0].ToString();
            }

            // Pasar los valores obtenidos a la función GuardarDatosEnExcel
            if (GuardarDatosEnExcel(grado, apellidoPaterno, apellidoMaterno, nombres, ci, carreraSeleccionada, semestreSeleccionado, paralelo, asignaturaSeleccionada))
            {
                MessageBox.Show("Datos guardados correctamente.");
            }
            else
            {
                MessageBox.Show("Error al guardar los datos.");
            }
        }

        // Actualiza la función GuardarDatosEnExcel para aceptar strings en lugar de listas
        private bool GuardarDatosEnExcel(string grado, string apellidoPaterno, string apellidoMaterno, string nombres, string ci, string carrera, string semestre, string paralelo, string asignatura)
        {
            string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\lista_doc.xlsx");
            rutaexcel = Path.GetFullPath(rutaexcel);

            Excel.Application excel = new Excel.Application();
            Excel.Workbook libro = null;
            Excel.Worksheet hoja = null;
            string materiavalida= usuarioActual["Asignatura"].ToString();
            string carreravalida = usuarioActual["Carrera"].ToString();
            string carnetvalida = usuarioActual["CI"].ToString();
            string carnetnuevo = textBoxCI.Text;
            bool isChecked = checkBoxEstado.Checked;
            string estadoactualizado = isChecked ? "ACTIVO" : "INACTIVO";
            string estado = usuarioActual["Estado"].ToString();


            //MessageBox.Show($"{materiavalida}  {carreravalida}   {carnetvalida}");
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
                    hoja.Cells[1, 7] = "Carrera";
                    hoja.Cells[1, 8] = "Asignatura";
                    hoja.Cells[1, 9] = "Semestre Académico";
                    hoja.Cells[1, 10] = "Paralelo";
                    hoja.Cells[1, 21] = "Estado";
                }
                else
                {
                    libro = excel.Workbooks.Open(rutaexcel);
                    hoja = libro.ActiveSheet;
                }
                

                // Obtener el rango de datos usados
                Excel.Range usedRange = hoja.UsedRange;
                int rowCount = usedRange.Rows.Count;

                // Buscar la fila correspondiente al CI, Carrera y Asignatura
                Excel.Range filaCoincidente = null;
                for (int i = 2; i <= rowCount; i++) // Comienza en 2 para evitar el encabezado
                {
                    if (hoja.Cells[i, 6].Value?.ToString() == carnetvalida &&
                        hoja.Cells[i, 7].Value?.ToString() == carreravalida &&
                        hoja.Cells[i, 8].Value?.ToString() == materiavalida)
                    {
                        filaCoincidente = hoja.Rows[i];
                        break;
                    }
                }

                if (filaCoincidente != null)
                {
                    // Actualizar los datos en la fila encontrada
                    filaCoincidente.Cells[2].Value = grado;
                    filaCoincidente.Cells[3].Value = apellidoPaterno;
                    filaCoincidente.Cells[4].Value = apellidoMaterno;
                    filaCoincidente.Cells[5].Value = nombres;
                    filaCoincidente.Cells[6].Value = carnetnuevo;
                    filaCoincidente.Cells[7].Value = carrera;
                    filaCoincidente.Cells[8].Value = asignatura;
                    filaCoincidente.Cells[9].Value = semestre;
                    filaCoincidente.Cells[10].Value = paralelo;
                    filaCoincidente.Cells[21].Value = estadoactualizado;


                }
                libro.Save();
                //MessageBox.Show("Guardo Correctamente");
                this.Close();
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





    }
}
