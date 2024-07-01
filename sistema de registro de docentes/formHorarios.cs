using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;


namespace sistema_de_registro_de_docentes
{
    public partial class formHorarios : Form
    {
        private string[,] excelData; // Matriz para almacenar los datos del Excel
        private System.Data.DataTable originalDataTable;
        public string rutaExcel = "";
        private Dictionary<string, Dictionary<string, TableLayoutPanel>> horariosPorCarrera = new Dictionary<string, Dictionary<string, TableLayoutPanel>>();
        private Dictionary<string, Color> colorPorMateria = new Dictionary<string, Color>();
        private Random random = new Random();

        public formHorarios()
        {
            InitializeComponent();
            carreraBox.SelectedIndexChanged += carreraBox_SelectedIndexChanged;
            semestreBox.SelectedIndexChanged += semestreBox_SelectedIndexChanged;
            semPanel.SelectedIndexChanged += SemPanel_SelectedIndexChanged;
            // Ruta del archivo Excel
            rutaExcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\lista_doc.xlsx");

            if (!File.Exists(rutaExcel))
            {
                MessageBox.Show("El archivo especificado no existe: " + rutaExcel);
                return;
            }

            CargarDatosDesdeExcel(rutaExcel);
            CargarComponentes();
            foreach (var carrera in carreraBox.Items)
            {
                CrearHorariosPorCarrera(carrera.ToString());
            }

        }

        private void CargarDatosDesdeExcel(string filePath)
        {
            originalDataTable = LeerArchivoExcel(filePath);

            if (originalDataTable != null)
            {
                ConvertirDataTableAMatriz();

            }
        }

        private void ConvertirDataTableAMatriz()
        {
            // Obtener dimensiones del DataTable
            int rowCount = originalDataTable.Rows.Count;
            int colCount = originalDataTable.Columns.Count;

            // Inicializar la matriz
            excelData = new string[rowCount, colCount];

            // Llenar la matriz con los datos del DataTable
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    excelData[i, j] = originalDataTable.Rows[i][j].ToString();
                }
            }
        }


        private void AgregarAsignacionHorario(TableLayoutPanel horarioTableLayoutPanel, string docente, string materia, string dia, string horaEntrada, string horaSalida)
        {
            // Mapear el día seleccionado al índice de la columna correspondiente
            int diaColumna = -1;
            switch (dia)
            {
                case "Lunes":
                    diaColumna = 1;
                    break;
                case "Martes":
                    diaColumna = 2;
                    break;
                case "Miercoles":
                    diaColumna = 3;
                    break;
                case "Jueves":
                    diaColumna = 4;
                    break;
                case "Viernes":
                    diaColumna = 5;
                    break;
            }
            if (diaColumna == -1) return; // Si el día no es válido, salir

            // Definir las horas específicas
            string[] horas = { "7:45", "8:30", "9:15", "10:15", "11:00", "12:00", "12:45", "13:30", "14:15", "15:00", "15:45" };

            // Convertir las horas de entrada y salida a TimeSpan
            TimeSpan entrada = TimeSpan.Parse(horaEntrada);
            TimeSpan salida = TimeSpan.Parse(horaSalida);

            Color colorMateria = ObtenerColorParaMateria(materia);

            // Iterar sobre las filas para encontrar las que corresponden al rango de horas
            for (int row = 1; row <= 10; row++)
            {
                TimeSpan inicioClase = TimeSpan.Parse(horas[row - 1]);
                TimeSpan finClase = (row < 10) ? TimeSpan.Parse(horas[row]) : TimeSpan.Parse("16:00");

                if ((entrada >= inicioClase && entrada < finClase) || (salida > inicioClase && salida <= finClase) || (entrada < inicioClase && salida > finClase))
                {
                    var control = horarioTableLayoutPanel.GetControlFromPosition(diaColumna, row);
                    if (control is System.Windows.Forms.Label label)
                    {
                        label.Text = $"{materia}";
                        label.BackColor = colorMateria;
                        label.ForeColor = ObtenerColorContrastante(colorMateria);
                    }
                }
            }
        }
        private Color ObtenerColorContrastante(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White;
        }

        private void CargarComponentes()
        {
            LlenarComboBoxCarreras();
            LlenarComboBoxSemestre();
            

            // Crear horarios para todas las carreras
            foreach (var carrera in carreraBox.Items)
            {
                CrearHorariosPorCarrera(carrera.ToString());
            }

            // Añadir eventos
            carreraBox.SelectedIndexChanged += carreraBox_SelectedIndexChanged;
            semestreBox.SelectedIndexChanged += semestreBox_SelectedIndexChanged;
            
        }
        private void CrearHorariosPorCarrera(string carrera)
        {
            if (!horariosPorCarrera.ContainsKey(carrera))
            {
                horariosPorCarrera[carrera] = new Dictionary<string, TableLayoutPanel>();
            }

            var semestresParalelos = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carrera)
                .Select(row => new
                {
                    Semestre = row.Field<object>("Semestre Académico")?.ToString(),
                    Paralelo = row.Field<string>("Paralelo")
                })
                .Where(sp => !string.IsNullOrEmpty(sp.Semestre) && !string.IsNullOrEmpty(sp.Paralelo))
                .Distinct()
                .OrderBy(sp => sp.Semestre)
                .ThenBy(sp => sp.Paralelo)
                .Select(sp => $"{sp.Semestre} - {sp.Paralelo}")
                .ToList();

            foreach (var semestreParalelo in semestresParalelos)
            {
                if (!horariosPorCarrera[carrera].ContainsKey(semestreParalelo))
                {
                    TableLayoutPanel horarioTableLayoutPanel = CrearHorarioTableLayoutPanel();
                    horarioTableLayoutPanel.Name = $"Horario_{ObtenerNombreSemestreParalelo(semestreParalelo)}";
                    LlenarHorarioSemestre(carrera, semestreParalelo, horarioTableLayoutPanel);
                    horariosPorCarrera[carrera][semestreParalelo] = horarioTableLayoutPanel;
                }
            }

            // Imprimir para depuración
            Console.WriteLine($"Carrera: {carrera}");
            foreach (var sp in semestresParalelos)
            {
                Console.WriteLine($"  {sp}");
            }
        }
        private void SemPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (semPanel.SelectedTab != null && semPanel.SelectedTab.Tag is Tuple<string, string> tagInfo)
            {
                string carrera = tagInfo.Item1;
                string semestreParalelo = tagInfo.Item2;

                carreraBox.SelectedItem = carrera;
                semestreBox.SelectedItem = semestreParalelo;

                var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);
                LlenarFlowMaterias(carrera, semestre, paralelo);
            }
        }
        private string ObtenerNombreSemestreParalelo(string semestreParalelo)
        {
            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);
            return $"Semestre{semestre.Replace(" ", "")}Paralelo{paralelo}";
        }
        private void LlenarComboBoxCarreras()
        {
            var carreras = originalDataTable.AsEnumerable()
                                            .Select(row => row.Field<string>("Carrera"))
                                            .Distinct()
                                            .ToList();

            carreraBox.DataSource = carreras;
        }
        private void LlenarComboBoxSemestre()
        {
            var semestresParalelos = originalDataTable.AsEnumerable()
                .Select(row => new
                {
                    Semestre = row.Field<object>("Semestre Académico")?.ToString(),
                    Paralelo = row.Field<string>("Paralelo")
                })
                .Where(sp => !string.IsNullOrEmpty(sp.Semestre) && !string.IsNullOrEmpty(sp.Paralelo))
                .Distinct()
                .OrderBy(sp => sp.Semestre)
                .ThenBy(sp => sp.Paralelo)
                .Select(sp => $"{sp.Semestre} - {sp.Paralelo}")
                .ToList();

            semestreBox.DataSource = semestresParalelos;

            foreach (var sp in semestresParalelos)
            {
                Console.WriteLine($"  {sp}");
            }
        }
        private (string semestre, string paralelo) ObtenerSemestreYParalelo(string semestreParalelo)
        {
            var partes = semestreParalelo.Split('-');
            return (partes[0].Trim(), partes[1].Trim());
        }
        private void semestreBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (semestreBox.SelectedIndex == -1) return;
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            string semestreParalelo = semestreBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(carreraSeleccionada) || string.IsNullOrEmpty(semestreParalelo)) return;

            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);

            LlenarFlowMaterias(carreraSeleccionada, semestre, paralelo);

            // Seleccionar la pestaña del TabControl correspondiente al semestre y paralelo seleccionado
            semPanel.SelectedIndexChanged -= SemPanel_SelectedIndexChanged;
            foreach (TabPage tabPage in semPanel.TabPages)
            {
                if (tabPage.Text == semestreParalelo)
                {
                    semPanel.SelectedTab = tabPage;
                    break;
                }
            }
            semPanel.SelectedIndexChanged += SemPanel_SelectedIndexChanged;
        }
        /*private void semestreBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (semestreBox.SelectedIndex == -1) return;
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            string semestreParalelo = semestreBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(carreraSeleccionada) || string.IsNullOrEmpty(semestreParalelo)) return;


            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);

            var docentesFiltrados = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carreraSeleccionada &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                              row.Field<string>("Paralelo") == paralelo)
                .Select(row => string.Join(" ", row.Field<string>("Apellido Paterno"), row.Field<string>("Apellido Materno"), row.Field<string>("Nombres")))
                .Distinct()
                .ToList();

            var materiasFiltradas = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carreraSeleccionada &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                              row.Field<string>("Paralelo") == paralelo)
                .Select(row => row.Field<string>("Asignatura"))
                .Distinct()
                .ToList();

            LlenarFlowMaterias(carreraSeleccionada, semestre, paralelo);
            // Seleccionar la pestaña del TabControl correspondiente al semestre y paralelo seleccionado
            foreach (TabPage tabPage in semPanel.TabPages)
            {
                if (tabPage.Text == semestreParalelo)
                {
                    semPanel.SelectedTab = tabPage;
                    break;
                }
            }
        }*/
        private void LlenarFlowMaterias(string carrera, string semestre, string paralelo)
        {
            flowMaterias.Controls.Clear();

            var materiasFiltradas = originalDataTable.AsEnumerable()
                .Where(row =>
                    row.Field<string>("Carrera") == carrera &&
                    row.Field<object>("Semestre Académico")?.ToString().Trim() == semestre.Trim() &&
                    row.Field<string>("Paralelo")?.Trim() == paralelo.Trim())
                .Select(row => row.Field<string>("Asignatura"))
                .Where(materia => !string.IsNullOrWhiteSpace(materia))
                .Distinct()
                .OrderBy(materia => materia)
                .ToList();

            if (materiasFiltradas.Count == 0)
            {
                Console.WriteLine($"No se encontraron materias para Carrera: {carrera}, Semestre: {semestre}, Paralelo: {paralelo}");
            }

            const int anchoLabel = 150;
            const int altoLabel = 40;

            foreach (var materia in materiasFiltradas)
            {
                Color colorMateria = ObtenerColorParaMateria(materia);

                System.Windows.Forms.Label labelMateria = new System.Windows.Forms.Label
                {
                    Text = materia,
                    Width = anchoLabel,
                    Height = altoLabel,
                    AutoSize = false,
                    Font = new System.Drawing.Font("Arial", 7),
                    BorderStyle = BorderStyle.FixedSingle,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(2),
                    Padding = new Padding(1),
                    AutoEllipsis = true,
                    BackColor = colorMateria,
                    ForeColor = ObtenerColorContrastante(colorMateria)
                };

                labelMateria.MouseDown += (sender, e) =>
                {
                    if (sender is System.Windows.Forms.Label label)
                    {
                        label.DoDragDrop(label.Text, DragDropEffects.Move);
                    }
                };
                flowMaterias.Controls.Add(labelMateria);
            }

            Console.WriteLine($"Se cargaron {materiasFiltradas.Count} materias para Carrera: {carrera}, Semestre: {semestre}, Paralelo: {paralelo}");
        }

        private TableLayoutPanel CrearHorarioTableLayoutPanel()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 6,
                RowCount = 11, // Ajustamos la cantidad de filas a 10
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            
            for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17f)); // Cada columna tendrá el 16.66% del ancho
            }
            for (int i = 1; i < tableLayoutPanel.RowCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 37F)); // Altura fija
            }

            // Agregar encabezados de columnas
            string[] dias = { "Hora", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" };

            for (int i = 1; i < dias.Length; i++)
            {
                tableLayoutPanel.Controls.Add(new System.Windows.Forms.Label
                {
                    Text = dias[i],
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    Padding = new Padding(2),
                    Font = new System.Drawing.Font("Arial", 8), // Tamaño de letra reducido
                    BackColor = System.Drawing.Color.Blue, // Aquí puedes cambiar el color a tu preferencia
                    ForeColor = System.Drawing.Color.White // Aquí puedes cambiar el color de la letra a tu preferenc
                }, i, 0);
            }


            // Horas específicas
            string[] horas = { "7:45-8:30", "8:30-9:15", "9:15-10:00", "10:15-11:00", "11:00-11:45", "12:00-12:45", "12:45-13:30", "13:30-14:15", "14:15-15:00", "15:00-15:45" };

            // Agregar horas y casillas vacías
            for (int row = 1; row <= 10; row++)
            {
                tableLayoutPanel.Controls.Add(new System.Windows.Forms.Label
                {
                    Text = horas[row - 1],
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    MinimumSize = new System.Drawing.Size(10, 0),
                    Padding = new Padding(0),
                    Font = new System.Drawing.Font("Arial", 8),
                    BackColor = System.Drawing.Color.FromArgb(224, 224, 224),
                    BorderStyle = BorderStyle.FixedSingle
                }, 0, row);

                for (int col = 1; col < 6; col++)
                {
                    System.Windows.Forms.Label materiaLabel = new System.Windows.Forms.Label
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                        AutoSize = true,
                        Padding = new Padding(2),
                        Font = new System.Drawing.Font("Arial", 7),
                        Text = "\n",
                        BorderStyle = BorderStyle.FixedSingle,
                        AllowDrop = true
                    };

                    // Agregar eventos para arrastrar y soltar
                    materiaLabel.DragEnter += Label_DragEnter;
                    materiaLabel.DragDrop += Label_DragDrop;
                    materiaLabel.MouseDown += HorarioLabel_MouseDown;

                    tableLayoutPanel.Controls.Add(materiaLabel, col, row);
                }
            }
            return tableLayoutPanel;
        }
        private void Label_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void Label_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is System.Windows.Forms.Label targetLabel)
            {
                string materia = e.Data.GetData(DataFormats.Text).ToString();
                targetLabel.Text = materia;
                targetLabel.BackColor = ObtenerColorParaMateria(materia);
                targetLabel.ForeColor = ObtenerColorContrastante(targetLabel.BackColor);
            }
        }

        private void HorarioLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is System.Windows.Forms.Label label && !string.IsNullOrEmpty(label.Text) && label.Text != "\n")
            {
                label.DoDragDrop(label.Text, DragDropEffects.Move);
                label.Text = "\n";
                label.BackColor = Color.White;
            }
        }

        private System.Data.DataTable LeerArchivoExcel(string filePath)
        {
            System.Data.DataTable dataTable = null;
            FileStream stream = null;
            IExcelDataReader reader = null;

            try
            {
                stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                reader = ExcelReaderFactory.CreateReader(stream);
                var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                dataTable = dataSet.Tables[0];
            }
            finally
            {
                // Cerrar los recursos utilizados
                reader?.Close();
                stream?.Close();
            }

            return dataTable;
        }
        private int GetColumnIndex(ExcelWorksheet worksheet, string columnName)
        {
            for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
            {
                if (worksheet.Cells[1, i].Text == columnName)
                {
                    return i;
                }
            }
            return -1; // Retornar -1 si la columna no se encuentra
        }

        private void carreraBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(carreraSeleccionada)) return;

            // Desactivar temporalmente los eventos
            semestreBox.SelectedIndexChanged -= semestreBox_SelectedIndexChanged;
            semPanel.SelectedIndexChanged -= SemPanel_SelectedIndexChanged;

            // Actualizar el ComboBox de semestres
            ActualizarComboBoxSemestre(carreraSeleccionada);

            // Mostrar los horarios por carrera
            MostrarHorariosPorCarrera(carreraSeleccionada);

            // Seleccionar el primer semestre si hay alguno
            if (semestreBox.Items.Count > 0)
            {
                semestreBox.SelectedIndex = 0;
                string semestreParalelo = semestreBox.SelectedItem.ToString();
                var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);
                LlenarFlowMaterias(carreraSeleccionada, semestre, paralelo);
            }
            else
            {
                flowMaterias.Controls.Clear();
            }

            // Reactivar los eventos
            semestreBox.SelectedIndexChanged += semestreBox_SelectedIndexChanged;
            semPanel.SelectedIndexChanged += SemPanel_SelectedIndexChanged;
        }
        private void ActualizarComboBoxSemestre(string carrera)
        {
            var semestresParalelos = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carrera)
                .Select(row => new
                {
                    Semestre = row.Field<object>("Semestre Académico")?.ToString(),
                    Paralelo = row.Field<string>("Paralelo")
                })
                .Where(sp => !string.IsNullOrEmpty(sp.Semestre) && !string.IsNullOrEmpty(sp.Paralelo))
                .Distinct()
                .OrderBy(sp => sp.Semestre)
                .ThenBy(sp => sp.Paralelo)
                .Select(sp => $"{sp.Semestre} - {sp.Paralelo}")
                .ToList();

            semestreBox.DataSource = semestresParalelos;
            semestreBox.SelectedIndex = -1;
        }
        private void MostrarHorariosPorCarrera(string carrera)
        {
            semPanel.TabPages.Clear();

            if (horariosPorCarrera.ContainsKey(carrera))
            {
                foreach (var kvp in horariosPorCarrera[carrera])
                {
                    string semestreParalelo = kvp.Key;
                    TableLayoutPanel horarioTableLayoutPanel = kvp.Value;

                    TabPage tabPage = new TabPage(semestreParalelo);
                    tabPage.Name = $"{carrera}_{ObtenerNombreSemestreParalelo(semestreParalelo)}";
                    tabPage.Controls.Add(horarioTableLayoutPanel);

                    // Aquí es donde añadimos el Tag
                    tabPage.Tag = new Tuple<string, string>(carrera, semestreParalelo);

                    semPanel.TabPages.Add(tabPage);
                }
            }

            Console.WriteLine($"Mostrando horarios para carrera: {carrera}");
            foreach (TabPage tab in semPanel.TabPages)
            {
                Console.WriteLine($"  Pestaña: {tab.Text}");
            }
        }

        private void LlenarHorarioSemestre(string carrera, string semestreParalelo, TableLayoutPanel horarioTableLayoutPanel)
        {
            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);

            var horarios = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carrera &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                              row.Field<string>("Paralelo") == paralelo)
                .ToList();

            foreach (var row in horarios)
            {
                string docente = string.Join(" ", row["Apellido Paterno"], row["Apellido Materno"], row["Nombres"]);
                string materia = row["Asignatura"]?.ToString();

                // Día 1
                string dia1 = row["Dia"]?.ToString();
                string horasEntrada1 = row["Hora entrada"]?.ToString();
                AgregarAsignacionSiExiste(horarioTableLayoutPanel, docente, materia, dia1, horasEntrada1);

                // Día 2
                string dia2 = row["Dia 2"]?.ToString();
                string horasEntrada2 = row["Hora entrada 2"]?.ToString();
                AgregarAsignacionSiExiste(horarioTableLayoutPanel, docente, materia, dia2, horasEntrada2);

                // Día 3
                string dia3 = row["Dia 3"]?.ToString();
                string horasEntrada3 = row["Hora entrada 3"]?.ToString();
                AgregarAsignacionSiExiste(horarioTableLayoutPanel, docente, materia, dia3, horasEntrada3);
            }
        }

        private void AgregarAsignacionSiExiste(TableLayoutPanel horarioTableLayoutPanel, string docente, string materia, string dia, string horasEntrada)
        {
            if (!string.IsNullOrEmpty(dia) && !string.IsNullOrEmpty(horasEntrada))
            {
                var horas = horasEntrada.Split('-');
                if (horas.Length == 2)
                {
                    AgregarAsignacionHorario(horarioTableLayoutPanel, docente, materia, dia, horas[0], horas[1]);
                }
            }
        }
        private void ActualizarComboBoxes(string carrera)
        {
            var semestres = horariosPorCarrera[carrera].Keys.OrderBy(s => s).ToList();

            // Desactivar temporalmente el evento SelectedIndexChanged
            semestreBox.SelectedIndexChanged -= semestreBox_SelectedIndexChanged;

            semestreBox.DataSource = semestres;
            semestreBox.SelectedIndex = -1;

            // Reactivar el evento SelectedIndexChanged
            semestreBox.SelectedIndexChanged += semestreBox_SelectedIndexChanged;
        }


        private Color ObtenerColorParaMateria(string materia)
        {
            if (!colorPorMateria.ContainsKey(materia))
            {
                Color nuevoColor;
                do
                {
                    nuevoColor = Color.FromArgb(random.Next(100, 256), random.Next(100, 256), random.Next(100, 256));
                } while (colorPorMateria.ContainsValue(nuevoColor));

                colorPorMateria[materia] = nuevoColor;
            }
            return colorPorMateria[materia];
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(carreraSeleccionada))
            {
                MessageBox.Show("Por favor, seleccione una carrera para exportar.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Guardar horarios como Excel",
                FileName = $"Horarios_{carreraSeleccionada}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarHorariosAExcel(carreraSeleccionada, saveFileDialog.FileName);
            }
        }

        private void ExportarHorariosAExcel(string carrera, string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                foreach (var kvp in horariosPorCarrera[carrera])
                {
                    string semestreParalelo = kvp.Key;
                    TableLayoutPanel horario = kvp.Value;

                    var worksheet = package.Workbook.Worksheets.Add(semestreParalelo);

                    // Configurar el encabezado
                    ConfigurarEncabezado(worksheet, carrera, semestreParalelo);

                    // Agregar el horario
                    AgregarHorario(worksheet, horario);

                    // Agregar información de materias y docentes
                    AgregarInformacionMateriasDocentes(worksheet, carrera, semestreParalelo);

                    // Agregar firmas
                    AgregarFirmas(worksheet);

                    // Ajustar ancho de columnas
                    for (int i = 1; i <= 6; i++)
                    {
                        worksheet.Column(i).Width = 22; // Ajusta este valor según tus necesidades
                    }

                    for (int i = 7; i <= 16; i++)
                    {
                        worksheet.Row(i).Height = 45; // Ajusta este valor según tus necesidades
                    }
                    for (int i = 1; i <= 16; i++)
                    {
                        worksheet.Cells[5, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[5, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[5, i].Style.WrapText = true;
                    }
                }

                try
                {
                    FileInfo file = new FileInfo(filePath);
                    package.SaveAs(file);
                    MessageBox.Show("Horarios exportados exitosamente.", "Exportación Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo: {ex.Message}", "Error de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConfigurarEncabezado(ExcelWorksheet worksheet, string carrera, string semestreParalelo)
        {
            worksheet.Cells["A1:F1"].Merge = true;
            worksheet.Cells["A1"].Value = "ESCUELA MILITAR DE INGENIERÍA";
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["A1"].Style.Font.Size = 16;
            worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells["A2:F2"].Merge = true;
            worksheet.Cells["A2"].Value = "HORARIO DE CLASES";
            worksheet.Cells["A2"].Style.Font.Bold = true;
            worksheet.Cells["A2"].Style.Font.Size = 14;
            worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells["A3:C3"].Merge = true;
            worksheet.Cells["A3"].Value = $"CARRERA: {carrera}";
            worksheet.Cells["A3"].Style.Font.Bold = true;

            worksheet.Cells["D3:F3"].Merge = true;
            worksheet.Cells["D3"].Value = $"SEMESTRE: {semestreParalelo}";
            worksheet.Cells["D3"].Style.Font.Bold = true;
        }

        private void AgregarHorario(ExcelWorksheet worksheet, TableLayoutPanel horario)
        {
            string[] diasSemana = { "HORA", "LUNES", "MARTES", "MIÉRCOLES", "JUEVES", "VIERNES" };
            string[] horas = { "7:45-8:30", "8:30-9:15", "9:15-10:00", "10:15-11:00", "11:00-11:45", "12:00-12:45", "12:45-13:30", "13:30-14:15", "14:15-15:00", "15:00-15:45" };

            // Agregar encabezados de días
            for (int i = 0; i < diasSemana.Length; i++)
            {
                worksheet.Cells[5, i + 1].Value = diasSemana[i];
                worksheet.Cells[5, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[5, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells[5, i + 1].Style.Font.Bold = true;
            }

            // Agregar horas y contenido del horario
            for (int row = 0; row < horas.Length; row++)
            {
                worksheet.Cells[row + 6, 1].Value = horas[row];
                for (int col = 1; col < 6; col++)
                {
                    var control = horario.GetControlFromPosition(col, row + 1);
                    if (control is System.Windows.Forms.Label label)
                    {
                        worksheet.Cells[row + 6, col + 1].Value = label.Text;
                        worksheet.Cells[row + 6, col + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row + 6, col + 1].Style.Fill.BackgroundColor.SetColor(label.BackColor);

                        // Ajustar el texto
                        worksheet.Cells[row + 6, col + 1].Style.WrapText = true;
                        worksheet.Cells[row + 6, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[row + 6, col + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }
                }
            }

            // Aplicar bordes al horario
            var rangoHorario = worksheet.Cells[5, 1, 15, 6];
            rangoHorario.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            rangoHorario.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            rangoHorario.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            rangoHorario.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Ajustar el ancho de las columnas
            worksheet.Column(1).Width = 15; // Columna de horas
            for (int i = 2; i <= 6; i++)
            {
                worksheet.Column(i).Width = 25; // Columnas de días (más anchas)
            }

            // Ajustar la altura de las filas
            for (int i = 6; i <= 15; i++)
            {
                worksheet.Row(i).Height = 35; // Filas más altas para el contenido del horario
            }
        }

        private void AgregarInformacionMateriasDocentes(ExcelWorksheet worksheet, string carrera, string semestreParalelo)
        {
            int filaInicio = 17; // Comienza después del horario

            worksheet.Cells[filaInicio, 1, filaInicio, 6].Merge = true;
            worksheet.Cells[filaInicio, 1].Value = "HORAS SEMANALES";
            worksheet.Cells[filaInicio, 1].Style.Font.Bold = true;
            worksheet.Cells[filaInicio, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            worksheet.Cells[filaInicio, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[filaInicio, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

            string[] encabezados = { "MATERIA", "DOCENTE", "TEORÍA", "PRÁCTICA", "LABORATORIO", "TOTAL" };
            for (int i = 0; i < encabezados.Length; i++)
            {
                worksheet.Cells[filaInicio + 1, i + 1].Value = encabezados[i];
                worksheet.Cells[filaInicio + 1, i + 1].Style.Font.Bold = true;
                worksheet.Cells[filaInicio + 1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[filaInicio + 1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }

            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);
            var materias = ObtenerMateriasYDocentes(carrera, semestre, paralelo);

            int filaActual = filaInicio + 2;
            foreach (var materia in materias)
            {
                Color colorMateria = ObtenerColorParaMateria(materia.NombreMateria);

                // Aplicar color y estilo a la materia
                worksheet.Cells[filaActual, 1].Value = materia.NombreMateria;
                worksheet.Cells[filaActual, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[filaActual, 1].Style.Fill.BackgroundColor.SetColor(colorMateria);
                worksheet.Cells[filaActual, 1].Style.Font.Color.SetColor(ObtenerColorContrastante(colorMateria));

                // Aplicar color y estilo al docente
                worksheet.Cells[filaActual, 2].Value = materia.NombreDocente;
                worksheet.Cells[filaActual, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[filaActual, 2].Style.Fill.BackgroundColor.SetColor(colorMateria);
                worksheet.Cells[filaActual, 2].Style.Font.Color.SetColor(ObtenerColorContrastante(colorMateria));

                worksheet.Cells[filaActual, 3].Value = materia.HorasTeoria;
                worksheet.Cells[filaActual, 4].Value = materia.HorasPractica;
                worksheet.Cells[filaActual, 5].Value = materia.HorasLaboratorio;
                worksheet.Cells[filaActual, 6].Value = materia.HorasTotal;

                // Centrar y ajustar el texto en todas las celdas de la fila
                for (int i = 1; i <= 6; i++)
                {
                    worksheet.Cells[filaActual, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[filaActual, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[filaActual, i].Style.WrapText = true;
                }

                filaActual++;
            }

            // Aplicar bordes a la tabla de materias
            var rangoMaterias = worksheet.Cells[filaInicio, 1, filaActual - 1, 6];
            rangoMaterias.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            rangoMaterias.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            rangoMaterias.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            rangoMaterias.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Ajustar el ancho de las columnas
            worksheet.Column(1).Width = 30; // Materia
            worksheet.Column(2).Width = 30; // Docente
            for (int i = 3; i <= 6; i++)
            {
                worksheet.Column(i).Width = 15; // Columnas de horas
            }

            // Ajustar la altura de las filas
            for (int i = filaInicio; i < filaActual; i++)
            {
                worksheet.Row(i).Height = 30;
            }
        }

        private void AgregarFirmas(ExcelWorksheet worksheet)
        {
            int filaFirmas = worksheet.Dimension.End.Row + 7;

            worksheet.Cells[filaFirmas, 1, filaFirmas, 3].Merge = true;
            worksheet.Cells[filaFirmas, 1].Value = "My.DIM. Wilbert Victor Ponce Huanaco";
            worksheet.Cells[filaFirmas, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells[filaFirmas, 4, filaFirmas, 6].Merge = true;
            worksheet.Cells[filaFirmas, 4].Value = "Cnl. DAEN. Mario Raúl Sandoval Nava";
            worksheet.Cells[filaFirmas, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells[filaFirmas + 1, 1, filaFirmas + 1, 3].Merge = true;
            worksheet.Cells[filaFirmas + 1, 1].Value = "JEFE DE CARRERA INGENIERÍA DE SISTEMAS";
            worksheet.Cells[filaFirmas + 1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells[filaFirmas + 1, 4, filaFirmas + 1, 6].Merge = true;
            worksheet.Cells[filaFirmas + 1, 4].Value = "DIRECTOR DE GRADO DE LA";
            worksheet.Cells[filaFirmas + 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells[filaFirmas + 2, 1, filaFirmas + 2, 3].Merge = true;
            worksheet.Cells[filaFirmas + 2, 1].Value = "UNIDAD ACADÉMICA LA PAZ";
            worksheet.Cells[filaFirmas + 2, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells[filaFirmas + 2, 4, filaFirmas + 2, 6].Merge = true;
            worksheet.Cells[filaFirmas + 2, 4].Value = "UNIDAD ACADÉMICA LA PAZ";
            worksheet.Cells[filaFirmas + 2, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells[filaFirmas + 3, 1, filaFirmas + 3, 3].Merge = true;
            worksheet.Cells[filaFirmas + 3, 1].Value = "ESCUELA MILITAR DE INGENIERÍA";
            worksheet.Cells[filaFirmas + 3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells[filaFirmas + 3, 4, filaFirmas + 3, 6].Merge = true;
            worksheet.Cells[filaFirmas + 3, 4].Value = "ESCUELA MILITAR DE INGENIERÍA";
            worksheet.Cells[filaFirmas + 3, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private List<MateriaDocente> ObtenerMateriasYDocentes(string carrera, string semestre, string paralelo)
        {
            var materias = new List<MateriaDocente>();

            var materiasDocentes = originalDataTable.AsEnumerable()
                .Where(row =>
                    row.Field<string>("Carrera") == carrera &&
                    row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                    row.Field<string>("Paralelo") == paralelo)
                .Select(row => new MateriaDocente
                {
                    NombreMateria = row.Field<string>("Asignatura"),
                    NombreDocente = $"{row.Field<string>("Apellido Paterno")} {row.Field<string>("Apellido Materno")} {row.Field<string>("Nombres")}",
                    HorasTeoria = ConvertirACarga(row.Field<object>("Carga horaria")),
                    HorasPractica = 0, // Asumiendo que no tienes esta información
                    HorasLaboratorio = 0, // Asumiendo que no tienes esta información
                    HorasTotal = ConvertirACarga(row.Field<object>("Carga horaria"))
                })
                .Distinct()
                .ToList();

            return materiasDocentes;
        }

        private int ConvertirACarga(object valor)
        {
            if (valor == null) return 0;
            if (int.TryParse(valor.ToString(), out int resultado))
                return resultado;
            return 0;
        }

        public class MateriaDocente
        {
            public string NombreMateria { get; set; }
            public string NombreDocente { get; set; }
            public int HorasTeoria { get; set; }
            public int HorasPractica { get; set; }
            public int HorasLaboratorio { get; set; }
            public int HorasTotal { get; set; }
        }
        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            GuardarTodosLosHorarios();
        }
        private void GuardarTodosLosHorarios()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            try
            {
                using (var package = new ExcelPackage(new FileInfo(rutaExcel)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    // Obtener los índices de las columnas relevantes
                    int carreraCol = GetColumnIndex(worksheet, "Carrera");
                    int semestreCol = GetColumnIndex(worksheet, "Semestre Académico");
                    int paraleloCol = GetColumnIndex(worksheet, "Paralelo");
                    int asignaturaCol = GetColumnIndex(worksheet, "Asignatura");
                    int cargaHorariaCol = GetColumnIndex(worksheet, "Carga horaria");
                    int diaCol = GetColumnIndex(worksheet, "Dia");
                    int horaEntradaCol = GetColumnIndex(worksheet, "Hora entrada");
                    int dia2Col = GetColumnIndex(worksheet, "Dia 2");
                    int horaEntrada2Col = GetColumnIndex(worksheet, "Hora entrada 2");
                    int dia3Col = GetColumnIndex(worksheet, "Dia 3");
                    int horaEntrada3Col = GetColumnIndex(worksheet, "Hora entrada 3");

                    foreach (var carrera in horariosPorCarrera.Keys)
                    {
                        foreach (var semestreParalelo in horariosPorCarrera[carrera].Keys)
                        {
                            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);
                            var horarioTableLayoutPanel = horariosPorCarrera[carrera][semestreParalelo];

                            ActualizarHorarioEnExcel(worksheet, carrera, semestre, paralelo, horarioTableLayoutPanel,
                                carreraCol, semestreCol, paraleloCol, asignaturaCol, cargaHorariaCol,
                                diaCol, horaEntradaCol, dia2Col, horaEntrada2Col, dia3Col, horaEntrada3Col);
                        }
                    }

                    package.Save();
                }

                MessageBox.Show("Todos los horarios han sido actualizados exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar los horarios: {ex.Message}");
            }
        }

        private void ActualizarHorarioEnExcel(ExcelWorksheet worksheet, string carrera, string semestre, string paralelo,
            TableLayoutPanel horarioTableLayoutPanel, int carreraCol, int semestreCol, int paraleloCol, int asignaturaCol,
            int cargaHorariaCol, int diaCol, int horaEntradaCol, int dia2Col, int horaEntrada2Col, int dia3Col, int horaEntrada3Col)
        {
            string[] diasSemana = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" };
            string[] horas = { "7:45", "8:30", "9:15", "10:15", "11:00", "12:00", "12:45", "13:30", "14:15", "15:00", "15:45" };

            Dictionary<string, Dictionary<string, List<(string horaInicio, string horaFin)>>> materiaHorarios = new Dictionary<string, Dictionary<string, List<(string, string)>>>();

            // Recopilar información de horarios
            for (int col = 1; col < 6; col++)
            {
                for (int row = 1; row <= 10; row++)
                {
                    var control = horarioTableLayoutPanel.GetControlFromPosition(col, row);
                    if (control is System.Windows.Forms.Label label && !string.IsNullOrWhiteSpace(label.Text) && label.Text != "\n")
                    {
                        string materia = label.Text;
                        string dia = diasSemana[col - 1];
                        string horaInicio = horas[row - 1];
                        string horaFin = (row < 10) ? horas[row] : "16:30";

                        if (!materiaHorarios.ContainsKey(materia))
                        {
                            materiaHorarios[materia] = new Dictionary<string, List<(string, string)>>();
                        }
                        if (!materiaHorarios[materia].ContainsKey(dia))
                        {
                            materiaHorarios[materia][dia] = new List<(string, string)>();
                        }
                        materiaHorarios[materia][dia].Add((horaInicio, horaFin));
                    }
                }
            }

            // Actualizar información en Excel
            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                if (worksheet.Cells[row, carreraCol].Value?.ToString() == carrera &&
                    worksheet.Cells[row, semestreCol].Value?.ToString() == semestre &&
                    worksheet.Cells[row, paraleloCol].Value?.ToString() == paralelo)
                {
                    string materia = worksheet.Cells[row, asignaturaCol].Value?.ToString();
                    if (materiaHorarios.ContainsKey(materia))
                    {
                        var horarios = materiaHorarios[materia];
                        int cargaHoraria = horarios.Values.Sum(h => h.Count);
                        worksheet.Cells[row, cargaHorariaCol].Value = cargaHoraria;

                        int diaIndex = 0;
                        foreach (var diaHorario in horarios)
                        {
                            if (diaIndex >= 3) break; // Solo guardamos hasta 3 días

                            int currentDiaCol = diaIndex == 0 ? diaCol : (diaIndex == 1 ? dia2Col : dia3Col);
                            int currentHoraCol = diaIndex == 0 ? horaEntradaCol : (diaIndex == 1 ? horaEntrada2Col : horaEntrada3Col);

                            worksheet.Cells[row, currentDiaCol].Value = diaHorario.Key;

                            var horariosAgrupados = AgruparHorarios(diaHorario.Value);
                            worksheet.Cells[row, currentHoraCol].Value = string.Join(", ", horariosAgrupados);

                            diaIndex++;
                        }

                        // Limpiar los días y horas restantes si es necesario
                        for (int i = diaIndex; i < 3; i++)
                        {
                            int currentDiaCol = i == 0 ? diaCol : (i == 1 ? dia2Col : dia3Col);
                            int currentHoraCol = i == 0 ? horaEntradaCol : (i == 1 ? horaEntrada2Col : horaEntrada3Col);
                            worksheet.Cells[row, currentDiaCol].Value = "";
                            worksheet.Cells[row, currentHoraCol].Value = "";
                        }
                    }
                    else
                    {
                        // Si la materia ya no tiene horarios, limpiar todas las columnas
                        worksheet.Cells[row, cargaHorariaCol].Value = 0;
                        worksheet.Cells[row, diaCol].Value = "";
                        worksheet.Cells[row, horaEntradaCol].Value = "";
                        worksheet.Cells[row, dia2Col].Value = "";
                        worksheet.Cells[row, horaEntrada2Col].Value = "";
                        worksheet.Cells[row, dia3Col].Value = "";
                        worksheet.Cells[row, horaEntrada3Col].Value = "";
                    }
                }
            }
        }

        private List<string> AgruparHorarios(List<(string horaInicio, string horaFin)> horarios)
        {
            var horariosOrdenados = horarios.OrderBy(h => TimeSpan.Parse(h.horaInicio)).ToList();
            var horariosAgrupados = new List<string>();

            for (int i = 0; i < horariosOrdenados.Count; i++)
            {
                var horarioActual = horariosOrdenados[i];
                var horaFin = TimeSpan.Parse(horarioActual.horaFin);

                while (i + 1 < horariosOrdenados.Count &&
                       TimeSpan.Parse(horariosOrdenados[i + 1].horaInicio) <= horaFin)
                {
                    horaFin = TimeSpan.Parse(horariosOrdenados[i + 1].horaFin);
                    i++;
                }

                horariosAgrupados.Add($"{horarioActual.horaInicio}-{horaFin:hh\\:mm}");
            }

            return horariosAgrupados;
        }

    }
}