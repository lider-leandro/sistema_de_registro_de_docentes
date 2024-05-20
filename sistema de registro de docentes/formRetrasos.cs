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
        public void LlenarDataGridView(DataGridView dgv, string[,] matriz)
        {
            dgv.ColumnCount = matriz.GetLength(1);
            dgv.RowCount = matriz.GetLength(0);

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
                    // Crear un StringBuilder para construir el contenido CSV
                    StringBuilder sb = new StringBuilder();

                    // Agregar encabezados de columnas al archivo CSV
                    foreach (DataGridViewColumn column in dataGridView.Columns)
                    {
                        sb.Append(column.HeaderText + ",");
                    }
                    sb.AppendLine(); // Nueva línea después de los encabezados

                    // Agregar filas de datos al archivo CSV
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            sb.Append(cell.Value?.ToString() + ","); // Usar null conditional operator para evitar NullReferenceException
                        }
                        sb.AppendLine(); // Nueva línea después de cada fila
                    }

                    // Escribir el contenido del StringBuilder en un archivo CSV
                    File.WriteAllText(nombreArchivo, sb.ToString());

                    // Abrir un cuadro de diálogo para guardar el archivo (opcional)
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Archivos CSV (*.xlsx)|*.xlsx";
                    saveFileDialog.FileName = nombreArchivo;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Copiar el archivo CSV generado a la ubicación seleccionada por el usuario
                        File.Copy(nombreArchivo, saveFileDialog.FileName, true);
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a CSV: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void B_calcular_Click(object sender, EventArgs e)
        {
            DataSet dataset = LeerArchivoExcel(@"E:\PROYECTO\proyecto registro de asistencia profesores\sistema de registro de docentes\sistema de registro de docentes\bin\Debug\net8.0-windows\data\lista_doc.xlsx");
            string[,] materiasHorarios = ConvertirDataSetEnMatriz(dataset);

            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Archivos de Excel|*.xlsx|Archivos de Excel 97-2003|*.xls" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    dataset = LeerArchivoExcel(ofd.FileName);
                }
            }

            string[,] registrosEntrada = ConvertirDataSetEnMatriz(dataset);

            Dictionary<string, int> atrasosPorMateria = new Dictionary<string, int>();
            for (int i = 0; i < materiasHorarios.GetLength(0); i++)
            {
                string docente = $"{materiasHorarios[i, 2]} {materiasHorarios[i, 3]} {materiasHorarios[i, 4]}";
                string materia = materiasHorarios[i, 8];
                string ci = materiasHorarios[i, 5];
                string[] horario = new string[4];
                horario[0] = materiasHorarios[i, 12];
                horario[1] = materiasHorarios[i, 13];
                horario[2] = materiasHorarios[i, 14];
                horario[3] = materiasHorarios[i, 15];

                string[] horario2 = horario[1]?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
                if (horario2.Length > 1)
                {
                    horario[1] = horario2[1].Substring(0, horario2[1].Length - 3);
                }

                horario2 = horario[3]?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
                if (horario2.Length > 1)
                {
                    horario[3] = horario2[1].Substring(0, horario2[1].Length - 3);
                }
                int x = 0;
                for (int j = 0; j < registrosEntrada.GetLength(0); j++)
                {
                    string[] fecha = registrosEntrada[j, 2]?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
                    if (fecha.Length > 0)
                    {
                        string[] f = fecha[0].Split('/');
                        if (f.Length == 3)
                        {
                            DateTime date = new DateTime(int.Parse(f[2]), int.Parse(f[1]), int.Parse(f[0]));
                            string dia = ObtenerNombreDiaEnEspanol(date.DayOfWeek);

                            if (registrosEntrada[j, 0] == ci)
                            {
                                if (horario[0] == dia)
                                {
                                    x = int.Parse(CalcularAtraso( fecha[1], horario[1]));
                                    if (x < 70)
                                    {
                                        materiasHorarios[i, 16] = (x + int.Parse(materiasHorarios[i, 16])).ToString();
                                        
                                    }
                                }
                                if (horario[2] == dia)
                                {
                                    x = int.Parse(CalcularAtraso(fecha[1], horario[1]));
                                    if (x < 70)
                                    {
                                        materiasHorarios[i, 16] = (x + int.Parse(materiasHorarios[i, 16])).ToString();
                                        
                                    }
                                }
                            }
                        }
                    }
                }

                LlenarDataGridView(dataGridView1, materiasHorarios);
            }
        }

        private void B_exportar_Click(object sender, EventArgs e)
        {
            ExportToExcel(dataGridView1, "Informe Atrasos");
        }
    }
}
