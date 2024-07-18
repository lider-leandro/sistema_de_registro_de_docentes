using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using ClosedXML.Excel;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace sistema_de_registro_de_docentes
{
    public partial class formDocentes : Form
    {
        private DataTable tabla;
        private string rutaexceldoc;
        private string rutaexceldocprueba;
        private string carnet;
        private DataTable originalDataTable;
        public formDocentes()
        {
            InitializeComponent();
            rutaexceldoc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\lista_doc.xlsx");
            CargarDatosDesdeExcelDocentes();

            // Ajustar las columnas del DataGridView para llenar el espacio disponible
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        /*
        private void GuardarDatosEnExcelDocentes(DataView dataView)
        {
            rutaexceldocprueba = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\lista_doc.xlsx");

            try
            {
                // Verificar si el archivo está siendo utilizado
                if (IsFileLocked(new FileInfo(rutaexceldocprueba)))
                {
                    MessageBox.Show($"El archivo {rutaexceldocprueba} está siendo utilizado por otro proceso.");
                    return;
                }

                // Convertir DataView a DataTable
                DataTable dataTable = dataView.ToTable();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Hoja2");
                    worksheet.Cell(1, 1).InsertTable(dataTable);

                    // Guardar el archivo en la ruta especificada
                    workbook.SaveAs(rutaexceldocprueba);
                }

                MessageBox.Show($"Datos guardados exitosamente en {rutaexceldocprueba}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos en el archivo Excel: {ex.Message}");
            }
        }
        private void GuardarDatosEnExcelDocentesprueba(DataView dataView)
        {
            rutaexceldocprueba = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\prueba.xlsx");

            try
            {
                // Verificar si el archivo está siendo utilizado
                if (IsFileLocked(new FileInfo(rutaexceldocprueba)))
                {
                    MessageBox.Show($"El archivo {rutaexceldocprueba} está siendo utilizado por otro proceso.");
                    return;
                }

                // Convertir DataView a DataTable
                DataTable dataTable = dataView.ToTable();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Hoja2");
                    worksheet.Cell(1, 1).InsertTable(dataTable);

                    // Guardar el archivo en la ruta especificada
                    workbook.SaveAs(rutaexceldocprueba);
                }

                MessageBox.Show($"Datos guardados exitosamente en {rutaexceldocprueba}");
                GuardarDatosEnExcelDocentes(dataView);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos en el archivo Excel: {ex.Message}");
            }
        }
        
        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }

            return false;
        }
        
        */
        private void CargarDatosDesdeExcelDocentes()
        {
            string rutaexcel = Path.Combine(rutaexceldoc);
            string rutaImagenes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\imagenes");

            rutaexcel = Path.GetFullPath(rutaexcel);
            rutaImagenes = Path.GetFullPath(rutaImagenes);

            try
            {
                using (var stream = File.Open(rutaexcel, FileMode.Open, FileAccess.Read))
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

                        originalDataTable = dataSet.Tables["Hoja2"]; // Guardar los datos originales

                        // Ordenar los datos: Activos primero, luego inactivos
                        DataView dataView = new DataView(originalDataTable);
                        dataView.Sort = "Estado ASC";

                        DataTable sortedDataTable = dataView.ToTable();

                        DataTable filteredDataTable = sortedDataTable.DefaultView.ToTable(false,
                            "Nº", "Grdo", "Apellido Paterno", "Apellido Materno", "Nombres", "CI", "Carrera", "Asignatura", "Semestre Académico", "Estado");

                        // Reenumerar la columna "Nro"
                        int nro = 1;
                        foreach (DataRow row in filteredDataTable.Rows)
                        {
                            row["Nº"] = nro++;
                        }

                        dataGridView1.DataSource = filteredDataTable;

                        if (!dataGridView1.Columns.Contains("Detalle"))
                        {
                            DataGridViewButtonColumn btnDetalle = new DataGridViewButtonColumn();
                            btnDetalle.Name = "Detalle";
                            btnDetalle.HeaderText = "Detalle";
                            btnDetalle.Text = "Detalles";
                            btnDetalle.UseColumnTextForButtonValue = false; // Importante para personalizar el texto
                            dataGridView1.Columns.Add(btnDetalle);
                        }

                        // Ajustar el ancho de la columna "Detalle"
                        dataGridView1.Columns["Detalle"].Width = 280;

                        dataGridView1.ReadOnly = true;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dataGridView1.MultiSelect = false;
                        dataGridView1.AllowUserToAddRows = false;
                        dataGridView1.AllowUserToDeleteRows = false;
                        dataGridView1.AllowUserToResizeColumns = false;
                        dataGridView1.AllowUserToResizeRows = false;
                        dataGridView1.AllowUserToOrderColumns = false;

                        dataGridView1.EnableHeadersVisualStyles = false;
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                        dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                        dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

                        AdjustColumnWidths();

                        dataGridView1.CellMouseEnter += new DataGridViewCellEventHandler(dataGridView1_CellMouseEnter);
                        dataGridView1.CellMouseLeave += new DataGridViewCellEventHandler(dataGridView1_CellMouseLeave);

                        dataGridView1.CellPainting += (s, e) =>
                        {
                            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Estado")
                            {
                                e.Paint(e.CellBounds, DataGridViewPaintParts.Background);
                                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                                string estado = dataGridView1.Rows[e.RowIndex].Cells["Estado"].Value.ToString();
                                string iconoRuta = estado == "ACTIVO" ? Path.Combine(rutaImagenes, "check.png") : Path.Combine(rutaImagenes, "cancelarRojo.png");
                                Color textColor = estado == "ACTIVO" ? Color.Green : Color.Red;

                                if (File.Exists(iconoRuta))
                                {
                                    Image img = ResizeImage(Image.FromFile(iconoRuta), 16, 16);
                                    int imgX = e.CellBounds.Left + 35;
                                    int imgY = e.CellBounds.Top + (e.CellBounds.Height - img.Height) / 2;

                                    e.Graphics.DrawImage(img, new Rectangle(imgX, imgY, img.Width, img.Height));

                                    using (Brush textBrush = new SolidBrush(textColor))
                                    {
                                        e.Graphics.DrawString(estado, e.CellStyle.Font, textBrush, imgX + img.Width + 5, e.CellBounds.Top + ((e.CellBounds.Height - e.Graphics.MeasureString(estado, e.CellStyle.Font).Height) / 2));
                                    }
                                }

                                e.Handled = true;
                            }
                        };

                        // Personalizar el botón "Detalle"
                        dataGridView1.CellPainting += (s, e) =>
                        {
                            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Detalle" && e.RowIndex >= 0)
                            {
                                //dataGridView1.Columns["Detalle"].Width = 60;
                                e.Paint(e.CellBounds, DataGridViewPaintParts.Background);

                                // Rellenar la celda con el color de fondo
                                e.Graphics.FillRectangle(Brushes.DarkBlue, e.CellBounds);

                                // Dibujar el borde de la celda
                                using (Pen pen = new Pen(Color.LightBlue, 2)) // Ajustar el color y el grosor del borde según sea necesario
                                {
                                    e.Graphics.DrawRectangle(pen, e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width - 1, e.CellBounds.Height - 1);
                                }

                                // Pintar el resto de la celda
                                e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

                                string iconoRuta = Path.Combine(rutaImagenes, "menu.png");
                                if (File.Exists(iconoRuta))
                                {
                                    Image img = ResizeImage(Image.FromFile(iconoRuta), 10, 10);
                                    int imgX = e.CellBounds.Left + 3;
                                    int imgY = e.CellBounds.Top + (e.CellBounds.Height - img.Height) / 2;

                                    e.Graphics.DrawImage(img, new Rectangle(imgX, imgY, img.Width, img.Height));
                                }

                                using (Brush textBrush = new SolidBrush(Color.White))
                                {
                                    e.Graphics.DrawString("Detalles", e.CellStyle.Font, textBrush, e.CellBounds.Left + 13, e.CellBounds.Top + ((e.CellBounds.Height - e.Graphics.MeasureString("Detalles", e.CellStyle.Font).Height) / 2));
                                }

                                e.Handled = true;
                            }
                        };
                        //GuardarDatosEnExcelDocentesprueba(dataView);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos desde el archivo Excel: {ex.Message}");
            }
        }


        private void AdjustColumnWidths()
        {
            int totalWidth = dataGridView1.ClientSize.Width;
            int usedWidth = 0;

            // Ajustar el ancho de la columna "Expedido"
            var expedidoColumn = dataGridView1.Columns["Grdo"];
            if (expedidoColumn != null)
            {
                expedidoColumn.Width = 20; // Ajusta el ancho según sea necesario
                usedWidth += expedidoColumn.Width;
            }

            // Ajustar el ancho de las demás columnas excepto "Detalle" y "Nro"
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name != "Nombres")
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    usedWidth += column.Width;
                }
            }

            // Ajustar el ancho de la columna "Detalle"

        }
        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;

            if (!string.IsNullOrEmpty(searchValue))
            {
                // Suponiendo que el DataGridView ya está poblado con datos del archivo Excel
                DataTable dataTable = dataGridView1.DataSource as DataTable;
                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    dataView.RowFilter = string.Format("CI LIKE '%{0}%'", searchValue);
                    dataGridView1.DataSource = dataView;
                }
            }
            else
            {
                // Si searchValue está vacío, restablecer el DataGridView para mostrar todos los datos
                CargarDatosDesdeExcelDocentes();
            }
        }
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Detalle"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Cursor = Cursors.Hand;
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Detalle"].Index && e.RowIndex >= 0)
            {
                dataGridView1.Cursor = Cursors.Default;
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

            panel_Docente2.Controls.Add(childForm);
            panel_Docente2.Tag = childForm;
            childForm.BringToFront();

            childForm.Show();
        }

        private void buttonAgregarDocente_Click(object sender, EventArgs e)
        {
            transparente transparentForm = new transparente();
            añadir_docente emergenteDocentes = new añadir_docente();
            transparentForm.Show();
            emergenteDocentes.ShowDialog();
            transparentForm.Close();
        }

        private void buttonElminarDocente_Click(object sender, EventArgs e)
        {
            // Implementar lógica para eliminar un docente
        }

        private void buttonImportarDatosDocente_Click(object sender, EventArgs e)
        {
            // Implementar lógica para importar datos de docentes
        }

        private void leer_Click(object sender, EventArgs e)
        {
            CargarDatosDesdeExcelDocentes();
            abriFormHijo(new formDocentes());
        }
        private Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        private void formDocentes_Load(object sender, EventArgs e)
        {
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Detalle" && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataRowView dataRowView = row.DataBoundItem as DataRowView;

                if (dataRowView != null)
                {
                    DataRow dataRow = dataRowView.Row;
                    string carnetIdentidad = dataRow["CI"].ToString();
                    string asignatura = dataRow["Asignatura"].ToString();
                    string carrera = dataRow["Carrera"].ToString();
                    string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\lista_doc.xlsx");

                    transparente transparentForm = new transparente();
                    FormDetalleDocente detalleFormDocentes = new FormDetalleDocente();
                    transparentForm.Show();
                    detalleFormDocentes.SetDocenteData(carnetIdentidad, rutaexcel,asignatura,carrera);
                    detalleFormDocentes.ShowDialog();
                    transparentForm.Close();
                }
            }
        }

        
        private void textBoxBusqueda_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = textBoxSearch.Text.Trim();
            // Obtener la DataTable actual del DataGridView
            DataTable dataTable = (DataTable)dataGridView1.DataSource;

            // Aplicar filtro si el texto de búsqueda no está vacío
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                // Filtrar los datos por la columna "CI"
                DataTable filteredDataTable = originalDataTable.Clone(); // Clonar la estructura de la DataTable original

                foreach (DataRow row in originalDataTable.Rows)
                {
                    if (row["CI"].ToString().Contains(textoBusqueda))
                    {
                        filteredDataTable.ImportRow(row);
                    }
                }

                // Mostrar los datos filtrados en el DataGridView


                filteredDataTable.DefaultView.Sort = "CI ASC";
                AjustarAnchoColumnas();
                dataGridView1.DataSource = filteredDataTable.DefaultView.ToTable(false,"Nº", "Grdo", "Apellido Paterno", "Apellido Materno", "Nombres", "CI", "Carrera", "Asignatura", "Semestre Académico", "Estado");
                
            }    
            else
            {
                CargarDatosDesdeExcelDocentes();
                AjustarAnchoColumnas();
                
            }
        }
        private void AjustarAnchoColumnas()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "Detalle")
                {
                    column.Width = 120; // Ancho específico para la columna Detalle
                }
                else if (column.Name == "Grdo")
                {
                    column.Width = 60; // Ancho específico para la columna Grdo
                }
                
            }
        }
    }
}
