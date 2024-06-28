using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            comboBoxDocente.SelectedIndexChanged += docenteBox_SelectedIndexChanged;
            comboBoxMateria.SelectedIndexChanged += MateriaBox_SelectedIndexChanged;

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


        private void LimpiarHorarios()
        {
            // Limpiar todos los horarios antes de volver a llenar
            foreach (TabPage tabPage in semPanel.TabPages)
            {
                TableLayoutPanel horarioTableLayoutPanel = tabPage.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
                if (horarioTableLayoutPanel != null)
                {
                    LimpiarHorarioTableLayoutPanel(horarioTableLayoutPanel);
                }
            }
        }

        private void LimpiarHorarioTableLayoutPanel(TableLayoutPanel tableLayoutPanel)
        {
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 1; col < 6; col++)
                {
                    var control = tableLayoutPanel.GetControlFromPosition(col, row);
                    if (control is System.Windows.Forms.Label label)
                    {
                        label.Text = "";
                        label.BackColor = Color.White; // Restaurar el color de fondo original
                    }
                }
            }
        }
        private void CargarComponentes()
        {
            LlenarComboBoxCarreras();
            LlenarComboBoxSemestre();
            LlenarComboBoxDocentes();
            LlenarComboBoxMaterias();

            // Crear horarios para todas las carreras
            foreach (var carrera in carreraBox.Items)
            {
                CrearHorariosPorCarrera(carrera.ToString());
            }

            // Añadir eventos
            carreraBox.SelectedIndexChanged += carreraBox_SelectedIndexChanged;
            semestreBox.SelectedIndexChanged += semestreBox_SelectedIndexChanged;
            comboBoxDocente.SelectedIndexChanged += docenteBox_SelectedIndexChanged;
            comboBoxMateria.SelectedIndexChanged += MateriaBox_SelectedIndexChanged;

            // Llenar ComboBox de Entrada y Salida
            string[] horasEntrada = { "7:45","8:30", "9:15", "10:15", "11:00", "12:00", "12:45", "14:15" };
            string[] horasSalida = { "9:15", "10:00", "11:00", "11:45", "12:45", "13:30", "14:15", "15:45" };
            comBoxEntrada.Items.AddRange(horasEntrada);
            comBoxSalida.Items.AddRange(horasSalida);

            // Llenar ComboBox de días
            string[] dias = { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes" };
            comboBoxDia.Items.AddRange(dias);
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
        private void LlenarComboBoxDocentes()
        {
            var nombresDocentes = originalDataTable.AsEnumerable()
                                                   .Where(row => !string.IsNullOrWhiteSpace(row.Field<string>("Nombres")))
                                                   .Select(row => string.Join(" ", row.Field<string>("Apellido Paterno"), row.Field<string>("Apellido Materno"), row.Field<string>("Nombres")))
                                                   .Distinct()
                                                   .ToList();

            comboBoxDocente.DataSource = nombresDocentes;
        }

        private void LlenarComboBoxMaterias()
        {
            // Obtener materias únicas del DataTable
            var materias = originalDataTable.AsEnumerable()
                                            .Select(row => row.Field<string>("Asignatura"))
                                            .Distinct()
                                            .ToList();

            // Llenar ComboBox de Materias
            comboBoxMateria.DataSource = materias;
        }
        private (string semestre, string paralelo) ObtenerSemestreYParalelo(string semestreParalelo)
        {
            var partes = semestreParalelo.Split('-');
            return (partes[0].Trim(), partes[1].Trim());
        }
        private void semestreBox_SelectedIndexChanged(object sender, EventArgs e)
        {
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

            comboBoxDocente.DataSource = docentesFiltrados;

            var materiasFiltradas = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carreraSeleccionada &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                              row.Field<string>("Paralelo") == paralelo)
                .Select(row => row.Field<string>("Asignatura"))
                .Distinct()
                .ToList();

            comboBoxMateria.DataSource = materiasFiltradas;

            // Seleccionar la pestaña del TabControl correspondiente al semestre y paralelo seleccionado
            foreach (TabPage tabPage in semPanel.TabPages)
            {
                if (tabPage.Text == semestreParalelo)
                {
                    semPanel.SelectedTab = tabPage;
                    break;
                }
            }
        }


        private void MateriaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el semestre seleccionado en el ComboBox de Semestre
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();

            // Obtener el nombre del docente seleccionado en el ComboBox de Docentes
            string docenteSeleccionado = comboBoxDocente.SelectedItem?.ToString();

            // Obtener la materia seleccionada en el ComboBox de Materias
            string materiaSeleccionada = comboBoxMateria.SelectedItem?.ToString();

            // Verificar que se hayan seleccionado un semestre, un docente y una materia
            if (string.IsNullOrEmpty(semestreSeleccionado) || string.IsNullOrEmpty(docenteSeleccionado) || string.IsNullOrEmpty(materiaSeleccionada))
            {
                return;
            }

            // Filtrar las filas por el semestre, docente y materia seleccionados
            var filasFiltradas = FiltrarFilasPorSemestreDocente(semestreSeleccionado, docenteSeleccionado, materiaSeleccionada);

            // Verificar si hay resultados después de aplicar el filtro
            if (filasFiltradas.Any())
            {
                // Aquí puedes realizar las acciones necesarias con las filas filtradas.
                // Por ejemplo, puedes actualizar otros ComboBoxes o controles en el formulario si es necesario.
            }
            else
            {
                //MessageBox.Show("No se encontraron registros para la materia seleccionada.");
            }
        }

        private void docenteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarMateriasPorDocenteYSemestre();
        }

        private void FiltrarMateriasPorDocenteYSemestre()
        {
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            string semestreParalelo = semestreBox.SelectedItem?.ToString();
            string docenteSeleccionado = comboBoxDocente.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(carreraSeleccionada) || string.IsNullOrEmpty(semestreParalelo) || string.IsNullOrEmpty(docenteSeleccionado))
            {
                return;
            }

            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);

            var materiasFiltradas = originalDataTable.AsEnumerable()
                .Where(row =>
                    row.Field<string>("Carrera") == carreraSeleccionada &&
                    row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                    row.Field<string>("Paralelo") == paralelo &&
                    string.Join(" ", row.Field<string>("Apellido Paterno"), row.Field<string>("Apellido Materno"), row.Field<string>("Nombres")) == docenteSeleccionado)
                .Select(row => row.Field<string>("Asignatura"))
                .Distinct()
                .ToList();

            comboBoxMateria.DataSource = materiasFiltradas;
        }

        private IEnumerable<DataRow> FiltrarFilasPorSemestreDocente(string semestreSeleccionado, string docenteSeleccionado, string materiaSeleccionada)
        {
            // Filtrar las filas por el semestre y docente seleccionados
            var filasFiltradas = new List<DataRow>();

            try
            {
                for (int i = 0; i < excelData.GetLength(0); i++)
                {
                    string semestre = excelData[i, GetColumnIndex("Semestre Académico")];
                    string nombres = excelData[i, GetColumnIndex("Apellido Paterno")] + " " + excelData[i, GetColumnIndex("Apellido Materno")] + " " + excelData[i, GetColumnIndex("Nombres")];
                    string asingatura = excelData[i, GetColumnIndex("Asignatura")];
                    if (semestre == semestreSeleccionado && nombres == docenteSeleccionado && asingatura == materiaSeleccionada)
                    {
                        // Convertir la fila a DataRow
                        DataRow row = originalDataTable.Rows[i];
                        filasFiltradas.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar datos: {ex.Message}");
            }

            return filasFiltradas;
        }

        private int GetColumnIndex(string columnName)
        {
            System.Data.DataTable dt = originalDataTable;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == columnName)
                {
                    return i;
                }
            }
            return -1;
        }



        private void AgregarAsignacionHorario1(string docente, string materia, string dia, string horaEntrada, string horaSalida)
        {
            TableLayoutPanel horarioTableLayoutPanel = semPanel.SelectedTab.Controls.OfType<TableLayoutPanel>().First();
            int diaColumna = ObtenerColumnaDia(dia);
            if (diaColumna == -1) return;

            TimeSpan entrada = TimeSpan.Parse(horaEntrada);
            TimeSpan salida = TimeSpan.Parse(horaSalida);
            Color colorMateria = ObtenerColorParaMateria(materia);

            // Definir las horas específicas
            string[] horas = { "7:45", "8:30", "9:15", "10:15", "11:00", "12:00", "12:45", "13:30", "14:15", "15:00", "15:45" };

            for (int row = 1; row <= 10; row++)
            {
                TimeSpan inicioClase = TimeSpan.Parse(horas[row - 1]);
                TimeSpan finClase = (row < 10) ? TimeSpan.Parse(horas[row]) : TimeSpan.Parse("16:00");

                if ((entrada >= inicioClase && entrada < finClase) || (salida > inicioClase && salida <= finClase) || (entrada < inicioClase && salida > finClase))
                {
                    var label = horarioTableLayoutPanel.GetControlFromPosition(diaColumna, row) as System.Windows.Forms.Label;
                    if (label != null)
                    {
                        label.Text = materia;
                        label.BackColor = colorMateria;
                        label.ForeColor = ObtenerColorContrastante(colorMateria);
                    }
                }
            }
        }

        private int ObtenerColumnaDia(string dia)
        {
            switch (dia)
            {
                case "Lunes": return 1;
                case "Martes": return 2;
                case "Miercoles": return 3;
                case "Jueves": return 4;
                case "Viernes": return 5;
                default: return -1;
            }
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

            // Agregar encabezados de columnas
            for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66f)); // Cada columna tendrá el 16.66% del ancho
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
                    MinimumSize = new System.Drawing.Size(10, 0), // Establecer un ancho mínimo fijo
                    Padding = new Padding(0),
                    Font = new System.Drawing.Font("Arial", 8), // Tamaño de letra reducido
                    BackColor = System.Drawing.Color.FromArgb(224, 224, 224), // Color gris claro
                    BorderStyle = BorderStyle.FixedSingle // Borde simple
                }, 0, row);

                for (int col = 1; col < 6; col++)
                {
                    System.Windows.Forms.Label materiaLabel = new System.Windows.Forms.Label
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = System.Drawing.ContentAlignment.MiddleCenter, // Centrar el texto
                        AutoSize = true,
                        Padding = new Padding(2), // Ajustar padding
                        Font = new System.Drawing.Font("Arial", 7), // Tamaño de letra reducido
                        Text = "\n", // Texto de ejemplo con salto de línea
                        BorderStyle = BorderStyle.FixedSingle // Borde simple
                    };
                    tableLayoutPanel.Controls.Add(materiaLabel, col, row);
                }
            }


            return tableLayoutPanel;
        }





        private void ActualizarHorarioEnDataTable(string semestre, string asignatura, string dia, string horaEntrada, string horaSalida)
        {
            DataRow[] rows = originalDataTable.Select($"[Semestre Académico] = '{semestre}' AND [Asignatura] = '{asignatura}'");
            if (rows.Length > 0)
            {
                DataRow row = rows[0];
                int cargaHoraria = CalcularCargaHoraria(horaEntrada, horaSalida);
                string horaEntradaSalida = $"{horaEntrada}-{horaSalida}";

                if (string.IsNullOrEmpty(row.Field<string>("Dia")))
                {
                    row["Carga horaria"] = cargaHoraria;
                    row["Dia"] = dia;
                    row["Hora entrada"] = horaEntradaSalida;
                }
                else if (string.IsNullOrEmpty(row.Field<string>("Dia 2")))
                {
                    row["Dia 2"] = dia;
                    row["Hora entrada 2"] = horaEntradaSalida;
                }
                else
                {
                    row["Dia 3"] = dia;
                    row["Hora entrada 3"] = horaEntradaSalida;
                }
            }
        }

        private int CalcularCargaHoraria(string horaEntrada, string horaSalida)
        {
            // Convertir las horas de entrada y salida a objetos DateTime
            DateTime entrada = DateTime.ParseExact(horaEntrada, "H:mm", CultureInfo.InvariantCulture);
            DateTime salida = DateTime.ParseExact(horaSalida, "H:mm", CultureInfo.InvariantCulture);

            // Calcular la diferencia en minutos
            int minutos = (int)(salida - entrada).TotalMinutes;

            // Calcular la carga horaria en bloques de 45 minutos
            int cargaHoraria = (minutos + 44) / 45; // Redondeamos hacia arriba

            return cargaHoraria;
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

        private void agregarButton_Click_1(object sender, EventArgs e)
        {
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();
            string docenteSeleccionado = comboBoxDocente.SelectedItem?.ToString();
            string materiaSeleccionada = comboBoxMateria.SelectedItem?.ToString();
            string diaSeleccionado = comboBoxDia.SelectedItem?.ToString();
            string horaEntrada = comBoxEntrada.SelectedItem?.ToString();
            string horaSalida = comBoxSalida.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(semestreSeleccionado) || string.IsNullOrEmpty(docenteSeleccionado) ||
                string.IsNullOrEmpty(materiaSeleccionada) || string.IsNullOrEmpty(diaSeleccionado) ||
                string.IsNullOrEmpty(horaEntrada) || string.IsNullOrEmpty(horaSalida))
            {
                MessageBox.Show("Por favor, complete todos los campos para agregar una asignación de horario.");
                return;
            }

            // Agregar el horario al TableLayoutPanel
            AgregarAsignacionHorario1(docenteSeleccionado, materiaSeleccionada, diaSeleccionado, horaEntrada, horaSalida);

            // Actualizar el DataTable con los horarios asignados
            ActualizarHorarioEnDataTable(semestreSeleccionado, materiaSeleccionada, diaSeleccionado, horaEntrada, horaSalida);

            // Guardar los cambios en el archivo Excel
            GuardarDatosActualizado();
        }

        private void GuardarDatosActualizado()
        {
            string semestreParalelo = semestreBox.SelectedItem?.ToString();
            string docenteSeleccionado = comboBoxDocente.SelectedItem?.ToString();
            string materiaSeleccionada = comboBoxMateria.SelectedItem?.ToString();
            string carreraSeleccionada = carreraBox.Text;
            string diaSeleccionado = comboBoxDia.SelectedItem?.ToString();
            string horaEntrada = comBoxEntrada.SelectedItem?.ToString();
            string horaSalida = comBoxSalida.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(semestreParalelo) || string.IsNullOrEmpty(docenteSeleccionado) ||
                string.IsNullOrEmpty(materiaSeleccionada) || string.IsNullOrEmpty(carreraSeleccionada) ||
                string.IsNullOrEmpty(diaSeleccionado) || string.IsNullOrEmpty(horaEntrada) || string.IsNullOrEmpty(horaSalida))
            {
                MessageBox.Show("Por favor, complete todos los campos para agregar una asignación de horario.");
                return;
            }

            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);

            DataRow targetRow = null;
            foreach (DataRow row in originalDataTable.Rows)
            {
                if (row["Semestre Académico"].ToString() == semestre &&
                    row["Paralelo"].ToString() == paralelo &&
                    string.Join(" ", row["Apellido Paterno"], row["Apellido Materno"], row["Nombres"]) == docenteSeleccionado &&
                    row["Asignatura"].ToString() == materiaSeleccionada &&
                    row["Carrera"].ToString() == carreraSeleccionada)
                {
                    targetRow = row;
                    break;
                }
            }

            if (targetRow != null)
            {
                ActualizarFilaHorario(targetRow, diaSeleccionado, $"{horaEntrada}-{horaSalida}");
            }

            GuardarCambiosEnExcelActualizado();

            string carreraActual = carreraBox.SelectedItem?.ToString();
            if (horariosPorCarrera.ContainsKey(carreraActual))
            {
                CrearHorariosPorCarrera(carreraActual);
                MostrarHorariosPorCarrera(carreraActual);
            }
        }

        private void ActualizarFilaHorario(DataRow row, string dia, string horaEntrada)
        {
            if (string.IsNullOrEmpty(row["Dia"].ToString()))
            {
                row["Dia"] = dia;
                row["Hora entrada"] = horaEntrada;
            }
            else if (string.IsNullOrEmpty(row["Dia 2"].ToString()))
            {
                row["Dia 2"] = dia;
                row["Hora entrada 2"] = horaEntrada;
            }
            else
            {
                row["Dia 3"] = dia;
                row["Hora entrada 3"] = horaEntrada;
            }
        }
        private void GuardarCambiosEnExcelActualizado()
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(rutaExcel)))
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
                    Dictionary<string, int> columnIndexes = new Dictionary<string, int>();

                    // Cachear los índices de columna
                    for (int i = 1; i <= worksheet.Dimension.End.Column; i++)
                    {
                        string columnName = worksheet.Cells[1, i].Text;
                        columnIndexes[columnName] = i;
                    }

                    for (int i = 0; i < originalDataTable.Rows.Count; i++)
                    {
                        DataRow row = originalDataTable.Rows[i];
                        for (int j = 2; j <= worksheet.Dimension.End.Row; j++)  // Empezar desde la segunda fila
                        {
                            if (worksheet.Cells[j, 1].Text == row["Nº"].ToString())
                            {
                                ActualizarCeldasExcel(worksheet, j, row, columnIndexes);
                                break;
                            }
                        }
                    }

                    excelPackage.Save();
                }

                MessageBox.Show("Cambios guardados exitosamente en el archivo Excel.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios en el archivo Excel: {ex.Message}");
            }
        }

        private void ActualizarCeldasExcel(ExcelWorksheet worksheet, int row, DataRow dataRow, Dictionary<string, int> columnIndexes)
        {
            ActualizarCeldaExcel(worksheet, row, dataRow, columnIndexes, "Dia");
            ActualizarCeldaExcel(worksheet, row, dataRow, columnIndexes, "Hora entrada");
            ActualizarCeldaExcel(worksheet, row, dataRow, columnIndexes, "Dia 2");
            ActualizarCeldaExcel(worksheet, row, dataRow, columnIndexes, "Hora entrada 2");
            ActualizarCeldaExcel(worksheet, row, dataRow, columnIndexes, "Dia 3");
            ActualizarCeldaExcel(worksheet, row, dataRow, columnIndexes, "Hora entrada 3");
            ActualizarCeldaExcel(worksheet, row, dataRow, columnIndexes, "Carga horaria");
        }

        private void ActualizarCeldaExcel(ExcelWorksheet worksheet, int row, DataRow dataRow, Dictionary<string, int> columnIndexes, string columnName)
        {
            if (columnIndexes.TryGetValue(columnName, out int columnIndex))
            {
                worksheet.Cells[row, columnIndex].Value = dataRow[columnName] == DBNull.Value ? null : dataRow[columnName];
            }
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

            MostrarHorariosPorCarrera(carreraSeleccionada);
            ActualizarComboBoxes(carreraSeleccionada);
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
                    tabPage.Name = ObtenerNombreSemestreParalelo(semestreParalelo);
                    tabPage.Controls.Add(horarioTableLayoutPanel);
                    semPanel.TabPages.Add(tabPage);
                }
            }

            // Imprimir para depuración
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
            semestreBox.DataSource = semestres;

            comboBoxDocente.DataSource = null;
            comboBoxMateria.DataSource = null;
        }

        private void btnLimpiarSemestre_Click(object sender, EventArgs e)
        {
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(carreraSeleccionada) || string.IsNullOrEmpty(semestreSeleccionado))
            {
                MessageBox.Show("Por favor, seleccione una carrera y semestre.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de que desea borrar todo el horario del semestre {semestreSeleccionado} de la carrera {carreraSeleccionada}?",
                "Confirmar borrado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                LimpiarSemestreEnHorario(carreraSeleccionada, semestreSeleccionado);
                LimpiarSemestreEnDataTable(carreraSeleccionada, semestreSeleccionado);
                MostrarHorariosPorCarrera(carreraSeleccionada);
                MessageBox.Show("El horario del semestre ha sido borrado exitosamente.", "Borrado completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LimpiarSemestreEnHorario(string carrera, string semestre)
        {
            if (horariosPorCarrera.ContainsKey(carrera) && horariosPorCarrera[carrera].ContainsKey(semestre))
            {
                TableLayoutPanel horarioTableLayoutPanel = horariosPorCarrera[carrera][semestre];

                for (int row = 1; row <= 10; row++)
                {
                    for (int col = 1; col < 6; col++)
                    {
                        var control = horarioTableLayoutPanel.GetControlFromPosition(col, row);
                        if (control is System.Windows.Forms.Label label)
                        {
                            label.Text = "";
                            label.BackColor = Color.White; // Restaurar el color de fondo original
                            label.ForeColor = Color.Black; // Restaurar el color de texto original
                        }
                    }
                }
            }
        }

        private void LimpiarSemestreEnDataTable(string carrera, string semestreParalelo)
        {
            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);

            var rows = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carrera &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                              row.Field<string>("Paralelo") == paralelo)
                .ToList();

            foreach (var row in rows)
            {
                row["Dia"] = DBNull.Value;
                row["Hora entrada"] = DBNull.Value;
                row["Dia 2"] = DBNull.Value;
                row["Hora entrada 2"] = DBNull.Value;
                row["Dia 3"] = DBNull.Value;
                row["Hora entrada 3"] = DBNull.Value;
                row["Carga horaria"] = DBNull.Value;
            }

            GuardarCambiosEnExcelActualizado();
        }
        private void btnLimpiarMateria_Click(object sender, EventArgs e)
        {
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();
            string materiaSeleccionada = comboBoxMateria.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(carreraSeleccionada) || string.IsNullOrEmpty(semestreSeleccionado) || string.IsNullOrEmpty(materiaSeleccionada))
            {
                MessageBox.Show("Por favor, seleccione una carrera, semestre y materia.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"¿Está seguro de que desea borrar el horario de la materia {materiaSeleccionada} del semestre {semestreSeleccionado} de la carrera {carreraSeleccionada}?",
                "Confirmar borrado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                LimpiarMateriaEnHorario(carreraSeleccionada, semestreSeleccionado, materiaSeleccionada);
                LimpiarMateriaEnDataTable(carreraSeleccionada, semestreSeleccionado, materiaSeleccionada);
                MostrarHorariosPorCarrera(carreraSeleccionada);
                MessageBox.Show("El horario de la materia ha sido borrado exitosamente.", "Borrado completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LimpiarMateriaEnHorario(string carrera, string semestre, string materia)
        {
            if (horariosPorCarrera.ContainsKey(carrera) && horariosPorCarrera[carrera].ContainsKey(semestre))
            {
                TableLayoutPanel horarioTableLayoutPanel = horariosPorCarrera[carrera][semestre];

                for (int row = 1; row <= 10; row++)
                {
                    for (int col = 1; col < 6; col++)
                    {
                        var control = horarioTableLayoutPanel.GetControlFromPosition(col, row);
                        if (control is System.Windows.Forms.Label label && label.Text == materia)
                        {
                            label.Text = "";
                            label.BackColor = Color.White; // Restaurar el color de fondo original
                        }
                    }
                }
            }
        }

        private void LimpiarMateriaEnDataTable(string carrera, string semestreParalelo, string materia)
        {
            var (semestre, paralelo) = ObtenerSemestreYParalelo(semestreParalelo);

            var rows = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carrera &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestre &&
                              row.Field<string>("Paralelo") == paralelo &&
                              row.Field<string>("Asignatura") == materia)
                .ToList();

            foreach (var row in rows)
            {
                row["Dia"] = DBNull.Value;
                row["Hora entrada"] = DBNull.Value;
                row["Dia 2"] = DBNull.Value;
                row["Hora entrada 2"] = DBNull.Value;
                row["Dia 3"] = DBNull.Value;
                row["Hora entrada 3"] = DBNull.Value;
                row["Carga horaria"] = DBNull.Value;
            }

            GuardarCambiosEnExcelActualizado();
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
                Title = "Guardar horarios como Excel"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarHorariosAExcel(carreraSeleccionada, saveFileDialog.FileName);
            }
        }

        private void ExportarHorariosAExcel(string carrera, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Horarios " + carrera);

                // Definir encabezados
                string[] dias = { "Hora", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
                for (int i = 0; i < dias.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dias[i];
                }

                // Definir horas
                string[] horas = { "7:45-8:30", "8:30-9:15", "9:15-10:00", "10:15-11:00", "11:00-11:45", "12:00-12:45", "12:45-13:30", "13:30-14:15", "14:15-15:00", "15:00-15:45" };
                for (int i = 0; i < horas.Length; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = horas[i];
                }

                // Llenar horarios
                int rowOffset = 2;
                foreach (var kvp in horariosPorCarrera[carrera])
                {
                    string semestre = kvp.Key;
                    TableLayoutPanel horario = kvp.Value;

                    // Agregar título del semestre
                    worksheet.Cells[rowOffset, 1].Value = "Semestre: " + semestre;
                    worksheet.Cells[rowOffset, 1, rowOffset, 6].Merge = true;
                    rowOffset++;

                    // Llenar horario
                    for (int row = 1; row <= 10; row++)
                    {
                        for (int col = 1; col < 6; col++)
                        {
                            var control = horario.GetControlFromPosition(col, row);
                            if (control is System.Windows.Forms.Label label)
                            {
                                worksheet.Cells[row + rowOffset, col + 1].Value = label.Text;
                            }
                        }
                    }

                    rowOffset += 12; // Espacio para el próximo horario
                }

                // Ajustar ancho de columnas
                worksheet.Cells.AutoFitColumns();

                // Guardar el archivo
                File.WriteAllBytes(filePath, package.GetAsByteArray());
            }

            MessageBox.Show("Horarios exportados exitosamente.", "Exportación Completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

}