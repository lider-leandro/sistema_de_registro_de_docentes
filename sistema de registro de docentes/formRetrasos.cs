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
using System.Windows.Forms;
using ClosedXML.Excel;

namespace sistema_de_registro_de_docentes
{
    public partial class formRetrasos : Form
    {
        public formRetrasos()
        {
            InitializeComponent();
        }
        private DataSet LeerArchivoExcel(string direccion)
        {
            // Verificar que el archivo exista en la ruta especificada
            if (File.Exists(direccion))
            {
                // Determinar la extensión del archivo para utilizar el lector adecuado
                string fileExtension = Path.GetExtension(direccion);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                // Establecer la codificación (puedes cambiar esto según la codificación del archivo Excel)
                Encoding encoding = Encoding.GetEncoding("ISO-8859-1");

                // Abrir el archivo Excel como un flujo de datos
                using (var stream = File.Open(direccion, FileMode.Open, FileAccess.Read))
                {
                    // Crear un lector de Excel según la extensión del archivo
                    IExcelDataReader reader;
                    switch (fileExtension)
                    {
                        case ".xls":
                            reader = ExcelReaderFactory.CreateBinaryReader(stream, new ExcelReaderConfiguration()
                            {
                                FallbackEncoding = encoding
                            });
                            break;
                        case ".xlsx":
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream, new ExcelReaderConfiguration()
                            {
                                FallbackEncoding = encoding
                            });
                            break;
                        default:
                            reader = null;
                            break;
                    }

                    if (reader != null)
                    {
                        // Leer los datos del archivo Excel en un DataSet
                        DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true // Indicar si la primera fila es una fila de encabezado
                            }
                        });
                        return dataSet;
                    }
                    else
                    {
                        MessageBox.Show("Extensión de archivo no compatible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
            else
            {
                MessageBox.Show("El archivo especificado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static string[,] ConvertirDataSetEnMatriz(DataSet dataSet)
        {
            // Verificar que el DataSet y la tabla especificada existan
            if (dataSet == null)
            {
                throw new ArgumentException("El DataSet o la tabla especificada no existen.");
            }

            System.Data.DataTable dataTable = dataSet.Tables[0];

            // Crear la matriz con las dimensiones adecuadas
            int rows = dataTable.Rows.Count;
            int columns = dataTable.Columns.Count;
            string[,] matriz = new string[rows, columns];

            // Copiar los datos de la tabla a la matriz
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matriz[i, j] = dataTable.Rows[i][j].ToString();
                }
            }

            return matriz;
        }
        static string ObtenerNombreDiaEnEspanol(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Sunday: return "Domingo";
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miercoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sabado";
                default: throw new ArgumentOutOfRangeException();
            }
        }
        private static int ObtenerMinutosDesdeHora(string hora)
        {
            string[] partes = hora.Split(':');


            int horas = Convert.ToInt32(partes[0]);
            int minutos = Convert.ToInt32(partes[1]);

            // Calcular los minutos totales
            int minutosTotales = horas * 60 + minutos;

            return minutosTotales;
        }
        //Calcular atraso apartir de dos horas formato  {hora}:{minutos}
        private static string CalcularAtraso(string hora1, string hora2)
        {
            int m = ObtenerMinutosDesdeHora(hora1);
            int m2 = ObtenerMinutosDesdeHora(hora2);
            if ((m - m2) > 0)
            {
                return "" + (m - m2);
            }
            else
            {
                return "0";
            }
        }
        public void LlenarDataGridView(DataGridView dgv, string[,] matriz, string[] encabezados)
        {
            dgv.ColumnCount = matriz.GetLength(1);
            dgv.RowCount = matriz.GetLength(0);
            // Configurar los encabezados de las columnas
            for (int j = 0; j < encabezados.Length; j++)
            {
                dgv.Columns[j].HeaderText = encabezados[j];
            }

            // Llenar el DataGridView con los datos de la matriz
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    dgv.Rows[i].Cells[j].Value = matriz[i, j];
                }
            }
        }
        private void ExportToExcel(DataGridView dataGridView, string nombreArchivo)
        {
            try
            {
                // Verificar que el DataGridView y sus propiedades no sean null
                if (dataGridView != null && dataGridView.Rows.Count > 0)
                {
                    // Asegurarse de que el nombre del archivo tenga la extensión correcta
                    if (!nombreArchivo.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        nombreArchivo += ".xlsx";
                    }

                    // Crear un nuevo libro de Excel
                    using (var workbook = new XLWorkbook())
                    {
                        // Crear una nueva hoja de trabajo
                        var worksheet = workbook.Worksheets.Add("Atrasos");

                        // Agregar encabezados de columnas al archivo XLSX
                        for (int col = 0; col < dataGridView.Columns.Count; col++)
                        {
                            var cell = worksheet.Cell(1, col + 1);
                            cell.Value = dataGridView.Columns[col].HeaderText;

                            // Aplicar formato a los encabezados
                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.BackgroundColor = XLColor.BlueGray;
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
                            cell.Style.Border.OutsideBorderColor = XLColor.Black;
                            cell.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            cell.Style.Border.InsideBorderColor = XLColor.Black;

                            // Ajustar el ancho de las columnas del encabezado
                            worksheet.Column(col + 1).Width = 20; // Ajusta este valor según tus necesidades
                        }

                        // Agregar filas de datos al archivo XLSX
                        for (int row = 0; row < dataGridView.Rows.Count; row++)
                        {
                            for (int col = 0; col < dataGridView.Columns.Count; col++)
                            {
                                var cell = worksheet.Cell(row + 2, col + 1);
                                cell.Value = dataGridView.Rows[row].Cells[col].Value?.ToString();


                                // Aplicar bordes a las celdas de datos
                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                cell.Style.Border.OutsideBorderColor = XLColor.Black;
                                cell.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                                cell.Style.Border.InsideBorderColor = XLColor.Black;
                            }
                        }

                        // Ajustar el ancho de las columnas al contenido
                        worksheet.Columns().AdjustToContents();

                        // Guardar el archivo en la ubicación especificada
                        workbook.SaveAs(nombreArchivo);

                        // Abrir un cuadro de diálogo para guardar el archivo (opcional)
                        SaveFileDialog saveFileDialog = new SaveFileDialog
                        {
                            Filter = "Archivos Excel (*.xlsx)|*.xlsx",
                            FileName = Path.GetFileName(nombreArchivo)
                        };

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Copiar el archivo XLSX generado a la ubicación seleccionada por el usuario
                            File.Copy(nombreArchivo, saveFileDialog.FileName, true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void B_calcular_Click(object sender, EventArgs e)
        {
            string[] encabezados = { "Nro", "Grado", "Apellido Paterno", "Apellido Materno", "Nombres", "CI", "Asignatura", "Semestre Academico", "Paralelo", "Atrasos(Minutos)" };
            DataSet dataset = LeerArchivoExcel(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "lista_doc.xlsx"));
            string[,] materiasHorarios = ConvertirDataSetEnMatriz(dataset);

            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Archivos de Excel|*.xlsx|Archivos de Excel 97-2003|*.xls" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    dataset = LeerArchivoExcel(ofd.FileName);
                }
            }

            string[,] registrosEntrada = ConvertirDataSetEnMatriz(dataset);

            // recorre toda la lista de materias con docentes
            for (int i = 0; i < materiasHorarios.GetLength(0); i++)
            {
                string docente = $"{materiasHorarios[i, 2]} {materiasHorarios[i, 3]} {materiasHorarios[i, 4]}";
                string materia = materiasHorarios[i, 8];
                string ci = materiasHorarios[i, 5].Split(' ')[0];//Nro carnet sin extension

                string[] dias = new string[3];
                dias[0] = materiasHorarios[i, 10];// dia 1
                dias[1] = materiasHorarios[i, 12];// dia 2(si no hay es 0)

                string[] horario = new string[5];
                horario[0] = materiasHorarios[i, 11].Split('-')[0];//horario 1 hora de entrada
                horario[1] = materiasHorarios[i, 11].Split('-')[1];//horario 1 hora de finalizacion
                if (materiasHorarios[i, 13] != "0")
                {
                    horario[2] = materiasHorarios[i, 13].Split('-')[0];//horario 2 hora de entrada
                    horario[3] = materiasHorarios[i, 13].Split('-')[1];//horario 2 hora de finalizacion
                }

                int tiempo_atrasado = 0;
                //recorre toda la lista de reportes
                for (int j = 0; j < registrosEntrada.GetLength(0); j++)
                {
                    // verifica si la entrada pertenece a algun docente
                    if (registrosEntrada[j, 0] == ci)
                    {
                        //se separa la fecha y la hora del reporte
                        string fecha = registrosEntrada[j, 2].Split(' ')[0];
                        string hora_llegada = registrosEntrada[j, 2].Split(' ')[1];

                        //se determina el dia que representa la fecha
                        string[] f = fecha.Split('/');
                        DateTime date = new DateTime(int.Parse(f[2]), int.Parse(f[1]), int.Parse(f[0]));
                        string dia = ObtenerNombreDiaEnEspanol(date.DayOfWeek);

                        //verifica el dia del reporte cuadra con el dia 1 del horario del docente
                        if (dias[0] == dia)
                        {
                            //calcula el tiempo de atraso y el tiempo maximo a atrasarse
                            tiempo_atrasado = int.Parse(CalcularAtraso(hora_llegada, horario[0]));
                            int tiempo_atrasado_limite = int.Parse(CalcularAtraso(horario[1], horario[0]));
                            //verifica si el tiempo atrasado pertenece al horario adecuado
                            if (tiempo_atrasado < tiempo_atrasado_limite)
                            {
                                materiasHorarios[i, 14] = (tiempo_atrasado + int.Parse(materiasHorarios[i, 14])).ToString();

                            }
                        }
                        //verifica el dia del reporte cuadra con el dia 2 del horario del docente
                        if (dias[1] == dia && dias[1] != "0")
                        {
                            tiempo_atrasado = int.Parse(CalcularAtraso(hora_llegada, horario[2]));
                            int tiempo_atrasado_limite = int.Parse(CalcularAtraso(horario[3], horario[2]));
                            if (tiempo_atrasado < tiempo_atrasado_limite)
                            {
                                materiasHorarios[i, 14] = (tiempo_atrasado + int.Parse(materiasHorarios[i, 14])).ToString();

                            }
                        }
                    }


                }


            }
            string[,] matrizNueva = new string[materiasHorarios.GetLength(0) + 2, 10];

            // Imprimir la matriz nueva para verificar
            for (int i = 0; i < materiasHorarios.GetLength(0); i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    matrizNueva[i, j] = materiasHorarios[i, j];
                }
                matrizNueva[i, 9] = materiasHorarios[i, 14];

            }
            LlenarDataGridView(dataGridView1, matrizNueva, encabezados);
        }

        private void B_exportar_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1, "Informe Atrasos");
        }


    }
}
