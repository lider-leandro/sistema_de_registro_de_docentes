using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;

namespace sistema_de_registro_de_docentes
{
    public partial class formUsuarios : Form
    {
        private string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\sportsc.xlsx");
        private string rutaImagenes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\imagenes");

        public formUsuarios()
        {
            InitializeComponent();
            CargarDatosDesdeExcel();
        }

        private void CargarDatosDesdeExcel()
        {
            string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\sportsc.xlsx");
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

                        var dataTable = dataSet.Tables["Credenciales"];

                        // Ordenar los datos: Activos primero, luego inactivos
                        DataView dataView = new DataView(dataTable);
                        dataView.Sort = "Estado ASC";

                        DataTable sortedDataTable = dataView.ToTable();

                        DataTable filteredDataTable = sortedDataTable.DefaultView.ToTable(false,
                            "Nro", "Apellido Paterno", "Apellido Materno", "Nombre", "Unidad Academica", "rol", "Carnet de identidad", "Expedido", "Estado");

                        // Reenumerar la columna "Nro"
                        int nro = 1;
                        foreach (DataRow row in filteredDataTable.Rows)
                        {
                            row["Nro"] = nro++;
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
                        dataGridView1.Columns["Detalle"].Width = 280;

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
                                dataGridView1.Columns["Detalle"].Width = 180;
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


                        // Establecer el ancho de la columna "Detalle"
                       

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
            var expedidoColumn = dataGridView1.Columns["Expedido"];
            if (expedidoColumn != null)
            {
                expedidoColumn.Width = 60; // Ajusta el ancho según sea necesario
                usedWidth += expedidoColumn.Width;
            }

            // Ajustar el ancho de las demás columnas excepto "Detalle" y "Nro"
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name != "Detalle" && column.Name != "Nro" && column.Name != "Expedido")
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    usedWidth += column.Width;
                }
            }

            // Ajustar el ancho de la columna "Detalle"
          
        }



        private void buttonAgregarDocente_Click(object sender, EventArgs e)
        {
            transparente transparentForm = new transparente();
            añadirDatosDotencesEmergente emergenteUsuarios = new añadirDatosDotencesEmergente();
            transparentForm.Show();
            emergenteUsuarios.ShowDialog();
            transparentForm.Close();
            CargarDatosDesdeExcel();
        }

        private void leer_Click(object sender, EventArgs e)
        {
            CargarDatosDesdeExcel();
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
                    string carnetIdentidad = dataRow["Carnet de identidad"].ToString();
                    string expedido = dataRow["Expedido"].ToString();
                    string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\sportsc.xlsx");

                    transparente transparentForm = new transparente();
                    FormDetalleUsuario detalleForm = new FormDetalleUsuario();
                    transparentForm.Show();
                    detalleForm.SetUsuarioData(carnetIdentidad, expedido, rutaexcel);
                    detalleForm.ShowDialog();
                    transparentForm.Close();
                }
            }
        }
        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Detalle")
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
                else
                {
                    dataGridView1.Cursor = Cursors.Default;
                }
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Cursor = Cursors.Default;
        }

        private void formUsuarios_Load(object sender, EventArgs e)
        {
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
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
    }
}
