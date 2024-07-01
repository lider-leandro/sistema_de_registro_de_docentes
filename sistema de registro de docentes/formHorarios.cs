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
                    }
                }
            }
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
            // Limpiar todas las celdas del TableLayoutPanel
            for (int row = 1; row <= 10; row++) // Empezamos desde 1 para evitar borrar los encabezados
            {
                for (int col = 1; col < 6; col++)
                {
                    var control = tableLayoutPanel.GetControlFromPosition(col, row);
                    if (control is System.Windows.Forms.Label label)
                    {
                        label.Text = "";
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
            string[] horasEntrada = { "7:45", "9:15", "10:15", "11:00", "12:00", "12:45", "14:15" };
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

            var semestres = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carrera)
                .Select(row => row.Field<object>("Semestre Académico")?.ToString())
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            foreach (var semestre in semestres)
            {
                if (!horariosPorCarrera[carrera].ContainsKey(semestre))
                {
                    TableLayoutPanel horarioTableLayoutPanel = CrearHorarioTableLayoutPanel();
                    horarioTableLayoutPanel.Name = $"Horario_{ObtenerNombreSemestre(semestre)}";
                    LlenarHorarioSemestre(carrera, semestre, horarioTableLayoutPanel);
                    horariosPorCarrera[carrera][semestre] = horarioTableLayoutPanel;
                }
            }
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
            // Obtener semestres únicos del DataTable
            var semestres = originalDataTable.AsEnumerable()
                                             .Select(row =>
                                             {
                                                 var semestreObject = row.Field<object>("Semestre Académico");
                                                 return semestreObject != null ? semestreObject.ToString() : null;
                                             })
                                             .Where(semestre => !string.IsNullOrEmpty(semestre))
                                             .Distinct()
                                             .ToList();

            // Llenar ComboBox de Semestre
            semestreBox.DataSource = semestres;
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

        private void semestreBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(carreraSeleccionada) || string.IsNullOrEmpty(semestreSeleccionado)) return;

            var docentesFiltrados = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carreraSeleccionada &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestreSeleccionado)
                .Select(row => string.Join(" ", row.Field<string>("Apellido Paterno"), row.Field<string>("Apellido Materno"), row.Field<string>("Nombres")))
                .Distinct()
                .ToList();

            comboBoxDocente.DataSource = docentesFiltrados;

            var materiasFiltradas = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carreraSeleccionada &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestreSeleccionado)
                .Select(row => row.Field<string>("Asignatura"))
                .Distinct()
                .ToList();

            comboBoxMateria.DataSource = materiasFiltradas;

            // Seleccionar la pestaña del TabControl correspondiente al semestre seleccionado
            foreach (TabPage tabPage in semPanel.TabPages)
            {
                if (tabPage.Text == semestreSeleccionado)
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
            FiltrarMateriasPorDocente();
        }

        private void FiltrarMateriasPorDocente()
        {
            string carreraSeleccionada = carreraBox.SelectedItem?.ToString();
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();
            string docenteSeleccionado = comboBoxDocente.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(carreraSeleccionada) || string.IsNullOrEmpty(semestreSeleccionado) || string.IsNullOrEmpty(docenteSeleccionado)) return;

            var materiasFiltradas = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carreraSeleccionada &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestreSeleccionado &&
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
            // Obtener la tabla de horarios del semestre seleccionado
            TableLayoutPanel horarioTableLayoutPanel = semPanel.SelectedTab.Controls.OfType<TableLayoutPanel>().First();

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
                    }
                }
            }
        }

        private string ObtenerNombreSemestre(string semestreSeleccionado)
        {
            // Eliminar los espacios y los caracteres especiales de la cadena
            return semestreSeleccionado.Replace(" ", "").Replace("º", "").Replace("-", "");
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
            // Buscar la fila correspondiente en el DataTable
            DataRow[] rows = originalDataTable.Select($"[Semestre Académico] = '{semestre}' AND [Asignatura] = '{asignatura}'");

            if (rows.Length > 0)
            {
                DataRow row = rows[0];

                // Calcular la carga horaria
                int cargaHoraria = CalcularCargaHoraria(horaEntrada, horaSalida);

                if (string.IsNullOrEmpty(row.Field<string>("Dia")))
                {
                    // Si el campo "Dia" está vacío, asignar los valores en "Dia" y "Hora entrada"
                    row.SetField("Carga horaria", cargaHoraria);
                    row.SetField("Dia", dia);
                    row.SetField("Hora entrada", $"{horaEntrada}-{horaSalida}"); // Formato solicitado
                }
                else
                {
                    // Si el campo "Dia" está lleno, asignar los valores en "Dia 2" y "Hora entrada 2"
                    row.SetField("Dia 2", dia);
                    row.SetField("Hora entrada 2", $"{horaEntrada}-{horaSalida}"); // Formato solicitado
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
            string semestreSeleccionado = semestreBox.SelectedItem?.ToString();
            string docenteSeleccionado = comboBoxDocente.SelectedItem?.ToString();
            string materiaSeleccionada = comboBoxMateria.SelectedItem?.ToString();
            string carreraSeleccionada = carreraBox.Text.ToString();
            string diaSeleccionado = comboBoxDia.SelectedItem?.ToString();
            string horaEntrada = comBoxEntrada.SelectedItem?.ToString();
            string horaSalida = comBoxSalida.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(semestreSeleccionado) || string.IsNullOrEmpty(docenteSeleccionado) ||
                string.IsNullOrEmpty(materiaSeleccionada) || string.IsNullOrEmpty(carreraSeleccionada) ||
                string.IsNullOrEmpty(diaSeleccionado) || string.IsNullOrEmpty(horaEntrada) || string.IsNullOrEmpty(horaSalida))
            {
                MessageBox.Show("Por favor, complete todos los campos para agregar una asignación de horario.");
                return;
            }

            foreach (DataRow row in originalDataTable.Rows)
            {
                string semestre = row["Semestre Académico"]?.ToString();
                string docente = string.Join(" ", row["Apellido Paterno"], row["Apellido Materno"], row["Nombres"]);
                string materia = row["Asignatura"]?.ToString();
                string carrera = row["Carrera"]?.ToString();

                if (semestre == semestreSeleccionado && docente == docenteSeleccionado &&
                    materia == materiaSeleccionada && carrera == carreraSeleccionada)
                {
                    // Buscar si ya existe una asignación para este día
                    if (row["Dia"]?.ToString() == diaSeleccionado)
                    {
                        row["Hora entrada"] = $"{horaEntrada}-{horaSalida}";
                    }
                    else if (row["Dia 2"]?.ToString() == diaSeleccionado)
                    {
                        row["Hora entrada 2"] = $"{horaEntrada}-{horaSalida}";
                    }
                    else if (row["Dia 3"]?.ToString() == diaSeleccionado)
                    {
                        row["Hora entrada 3"] = $"{horaEntrada}-{horaSalida}";
                    }
                    else
                    {
                        // Si no existe, buscar el primer campo vacío
                        if (string.IsNullOrEmpty(row["Dia"]?.ToString()))
                        {
                            row["Dia"] = diaSeleccionado;
                            row["Hora entrada"] = $"{horaEntrada}-{horaSalida}";
                        }
                        else if (string.IsNullOrEmpty(row["Dia 2"]?.ToString()))
                        {
                            row["Dia 2"] = diaSeleccionado;
                            row["Hora entrada 2"] = $"{horaEntrada}-{horaSalida}";
                        }
                        else if (string.IsNullOrEmpty(row["Dia 3"]?.ToString()))
                        {
                            row["Dia 3"] = diaSeleccionado;
                            row["Hora entrada 3"] = $"{horaEntrada}-{horaSalida}";
                        }
                        else
                        {
                            // Si todos los campos están llenos, agregar a Dia 3
                            row["Dia 3"] = diaSeleccionado;
                            row["Hora entrada 3"] = $"{horaEntrada}-{horaSalida}";
                        }
                    }
                    break;
                }
            }

            GuardarCambiosEnExcelActualizado();

            // Actualizar el horario visual
            string carreraActual = carreraBox.SelectedItem?.ToString();
            if (horariosPorCarrera.ContainsKey(carreraActual))
            {
                CrearHorariosPorCarrera(carreraActual);
                MostrarHorariosPorCarrera(carreraActual);
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

                    for (int i = 0; i < originalDataTable.Rows.Count; i++)
                    {
                        DataRow row = originalDataTable.Rows[i];
                        for (int j = 1; j <= worksheet.Dimension.End.Row; j++)
                        {
                            if (worksheet.Cells[j, 1].Text == row["Nº"].ToString()) // Asumiendo que la primera columna es un ID único
                            {
                                worksheet.Cells[j, GetColumnIndex(worksheet, "Dia")].Value = row["Dia"];
                                worksheet.Cells[j, GetColumnIndex(worksheet, "Hora entrada")].Value = row["Hora entrada"];
                                worksheet.Cells[j, GetColumnIndex(worksheet, "Dia 2")].Value = row["Dia 2"];
                                worksheet.Cells[j, GetColumnIndex(worksheet, "Hora entrada 2")].Value = row["Hora entrada 2"];
                                worksheet.Cells[j, GetColumnIndex(worksheet, "Dia 3")].Value = row["Dia 3"];
                                worksheet.Cells[j, GetColumnIndex(worksheet, "Hora entrada 3")].Value = row["Hora entrada 3"];
                                worksheet.Cells[j, GetColumnIndex(worksheet, "Carga horaria")].Value = row["Carga horaria"];
                                break;
                            }
                        }
                    }

                    excelPackage.Save();
                    MessageBox.Show("Cambios guardados exitosamente en el archivo Excel.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios en el archivo Excel: {ex.Message}");
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
                    string semestre = kvp.Key;
                    TableLayoutPanel horarioTableLayoutPanel = kvp.Value;

                    TabPage tabPage = new TabPage(semestre);
                    tabPage.Name = ObtenerNombreSemestre(semestre);
                    tabPage.Controls.Add(horarioTableLayoutPanel);
                    semPanel.TabPages.Add(tabPage);
                }
            }
        }

        private void LlenarHorarioSemestre(string carrera, string semestre, TableLayoutPanel horarioTableLayoutPanel)
        {
            var horarios = originalDataTable.AsEnumerable()
                .Where(row => row.Field<string>("Carrera") == carrera &&
                              row.Field<object>("Semestre Académico")?.ToString() == semestre)
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
            if (!string.IsNullOrEmpty(horasEntrada))
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
            if (horariosPorCarrera.ContainsKey(carrera))
            {
                var semestres = horariosPorCarrera[carrera].Keys.OrderBy(s => s).ToList();
                semestreBox.DataSource = semestres;
            }
            else
            {
                // Manejar el caso en el que la carrera no exista en el diccionario
                MessageBox.Show($"La carrera '{carrera}' no se encontró en el diccionario.");
                semestreBox.DataSource = null;
            }

            comboBoxDocente.DataSource = null;
            comboBoxMateria.DataSource = null;
        }

    }
}