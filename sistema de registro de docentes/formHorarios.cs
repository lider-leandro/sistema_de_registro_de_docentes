using System;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Data;
using ExcelDataReader;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Horarios
{
    public partial class ASIGNACION_HORARIOS : Form
    {
        private DataSet originalDataSet;
        private System.Data.DataTable originalDataTable;

        private System.Data.DataTable semestresDataTable;
        private System.Data.DataTable docentesDataTable;
        private System.Data.DataTable materiasDataTable;
        private Excel.Application excelApp;


        public ASIGNACION_HORARIOS()
        {
            InitializeComponent();
            excelApp = excelApp;
            semestreBox.SelectedIndexChanged += semestreBox_SelectedIndexChanged;
            docenteBox.SelectedIndexChanged += docenteBox_SelectedIndexChanged;


        }

        private void ASIGNACION_HORARIOS_Load(object sender, EventArgs e)
        {

            //CargarSemestres();

            // Cargar datos de los docentes desde el archivo Excel
            //CargarDatosDesdeExcel("docentes");

            // Cargar datos de las materias desde el archivo Excel
            //CargarDatosDesdeExcel("materias");
        }


        private void ImportButton_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Archivos de Excel|*.xlsx|Archivos de Excel 97-2003|*.xls" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
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

                            // Crear una copia de la tabla original
                            originalDataTable = dataSet.Tables[0].Copy();

                            // Agregar columnas de hora de entrada y salida
                            originalDataTable.Columns.Add("Hora de Entrada", typeof(string));
                            originalDataTable.Columns.Add("Hora de Salida", typeof(string));

                            // Asignar valores a las columnas de hora de entrada y salida
                            foreach (DataRow row in originalDataTable.Rows)
                            {
                                row["Hora de Entrada"] = ""; // Puedes asignar valores o dejar en blanco según tu lógica
                                row["Hora de Salida"] = ""; // Puedes asignar valores o dejar en blanco según tu lógica
                            }

                            // Asignar la tabla actualizada al origen de datos del DataGridView
                            registro.DataSource = originalDataTable;

                            // Llenar los ComboBox de Semestre, Docentes y Materias
                            LlenarComboBoxSemestre();
                            LlenarComboBoxDocentes();
                            LlenarComboBoxMaterias();
                        }
                    }

                    // Configuraciones adicionales del DataGridView
                    // Puedes modificar los encabezados aquí si lo deseas
                    registro.Columns[0].HeaderText = "N°.";
                    registro.Columns[1].HeaderText = "Grado";
                    registro.Columns[2].HeaderText = "Apellido Paterno";
                    registro.Columns[3].HeaderText = "Apellido Materno";
                    registro.Columns[4].HeaderText = "Nombres";
                    registro.Columns[5].HeaderText = "CI";
                    registro.Columns[6].HeaderText = "Asignatura";
                    registro.Columns[7].HeaderText = "Semestre Académico";
                    registro.Columns[8].HeaderText = "Paralelo";
                    registro.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    registro.AutoResizeColumns();
                    registro.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11);
                }
            }
        }


        private void docenteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el nombre del docente seleccionado en el ComboBox de Docentes
            string docenteSeleccionado = docenteBox.SelectedItem?.ToString();

            if (docenteSeleccionado != null)
            {
                // Filtrar las materias para el docente seleccionado
                var materiasFiltradas = originalDataTable.AsEnumerable()
                                                         .Where(row => string.Join(" ", row.Field<string>("Apellido Paterno"), row.Field<string>("Apellido Materno"), row.Field<string>("Nombres")) == docenteSeleccionado)
                                                         .Select(row => row.Field<string>("Asignatura"))
                                                         .Distinct()
                                                         .ToList();

                // Actualizar el ComboBox de Materias con las materias filtradas
                materiaBox.DataSource = materiasFiltradas;
            }
        }

        private void LlenarComboBoxSemestre()
        {
            // Obtener semestres únicos del DataGridView
            var semestres = originalDataTable.AsEnumerable()
                                             .Select(row => row.Field<object>("Semestre Académico")?.ToString())
                                             .Where(semestre => !string.IsNullOrEmpty(semestre))
                                             .Distinct()
                                             .ToList();

            // Llenar ComboBox de Semestre
            semestreBox.DataSource = semestres;
        }




        private void LlenarComboBoxDocentes()
        {
            // Obtener nombres únicos de los docentes del DataGridView
            var nombresDocentes = originalDataTable.AsEnumerable()
                                                   .Select(row => string.Join(" ", row.Field<string>("Apellido Paterno"), row.Field<string>("Apellido Materno"), row.Field<string>("Nombres")))
                                                   .Distinct()
                                                   .ToList();

            // Llenar ComboBox de Docentes
            docenteBox.DataSource = nombresDocentes;
        }

        private void LlenarComboBoxMaterias()
        {
            // Obtener materias únicas del DataGridView
            var materias = originalDataTable.AsEnumerable()
                                            .Select(row => row.Field<string>("Asignatura"))
                                            .Distinct()
                                            .ToList();

            // Llenar ComboBox de Materias
            materiaBox.DataSource = materias;
        }







        private void Export_Click(object sender, EventArgs e)
        {
            ExportToExcel(registro);
        }

        private void ExportToExcel(DataGridView dataGridView)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;

            Workbook workbook = excelApp.Workbooks.Add();
            Worksheet worksheet = (Worksheet)workbook.Sheets[1];

            int columnCount = dataGridView.ColumnCount;
            int rowCount = dataGridView.RowCount;

            for (int i = 0; i < columnCount; i++)
            {
                worksheet.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value;
                }
            }

            // Aquí guardamos el archivo Excel en la ubicación deseada
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files|.xlsx;.xls;*.xlsm";
            saveFileDialog1.Title = "Guardar archivo Excel";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
            }
            else
            {
                MessageBox.Show("Debe especificar un nombre de archivo válido.");
            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            // Lógica para guardar cambios en el archivo Excel
            GuardarCambios();
        }

        private void QuitarButton_Click(object sender, EventArgs e)
        {
            // Verificar si hay una celda seleccionada
            if (registro.SelectedCells.Count > 0)
            {
                // Obtener la celda seleccionada y borrar su contenido
                DataGridViewCell selectedCell = registro.SelectedCells[0];
                selectedCell.Value = string.Empty;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una celda para borrar su contenido.");
            }
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            // Lógica para cancelar los cambios y restaurar los datos originales
            RestaurarDatosOriginales();
        }

        private void GuardarCambios()
        {


            // Verificar si hay datos para guardar
            if (registro.RowCount == 0)
            {
                MessageBox.Show("No hay datos para guardar.");
                return;
            }

            // Obtener la instancia del libro de Excel existente
            Workbook workbook = excelApp.ActiveWorkbook;

            // Agregar una nueva hoja de trabajo al libro
            Worksheet newWorksheet = workbook.Worksheets.Add();

            // Copiar los datos del DataGridView a la nueva hoja de trabajo
            for (int i = 0; i < registro.Rows.Count; i++)
            {
                for (int j = 0; j < registro.Columns.Count; j++)
                {
                    newWorksheet.Cells[i + 1, j + 1] = registro.Rows[i].Cells[j].Value.ToString();
                }
            }

            // Guardar el libro de Excel con la nueva hoja de trabajo
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel Files|.xlsx;.xls;*.xlsm";
            saveFileDialog1.Title = "Guardar archivo Excel";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                workbook.SaveAs(saveFileDialog1.FileName);
                MessageBox.Show("Datos guardados correctamente en una nueva hoja de trabajo del archivo Excel.");
            }
            else
            {
                MessageBox.Show("Debe especificar un nombre de archivo válido.");
            }
        }
        private void RestaurarDatosOriginales()
        {
            // Lógica para restaurar los datos originales en el DataGridView
            if (originalDataSet != null)
            {
                registro.DataSource = originalDataSet.Copy(); // Restaurar los datos originales
            }
        }

        private void semestreBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el semestre seleccionado en el ComboBox de Semestre
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();

            if (semestreSeleccionado != null)
            {
                // Filtrar los docentes que tienen materias asignadas para el semestre seleccionado
                var docentesFiltrados = originalDataTable.AsEnumerable()
                                                         .Where(row => row.Field<object>("Semestre Académico")?.ToString() == semestreSeleccionado)
                                                         .Select(row => string.Join(" ", row.Field<string>("Apellido Paterno"), row.Field<string>("Apellido Materno"), row.Field<string>("Nombres")))
                                                         .Distinct()
                                                         .ToList();

                // Actualizar el ComboBox de Docentes con los docentes filtrados
                docenteBox.DataSource = docentesFiltrados;

                // Filtrar las materias para el semestre seleccionado
                var materiasFiltradas = originalDataTable.AsEnumerable()
                                                         .Where(row => row.Field<object>("Semestre Académico")?.ToString() == semestreSeleccionado)
                                                         .Select(row => row.Field<string>("Asignatura"))
                                                         .Distinct()
                                                         .ToList();

                // Actualizar el ComboBox de Materias con las materias filtradas
                materiaBox.DataSource = materiasFiltradas;
            }
        }

   
    }
}