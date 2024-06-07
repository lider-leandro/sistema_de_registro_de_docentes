using DocumentFormat.OpenXml.Wordprocessing;
using ExcelDataReader;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;
using System.Linq;

namespace sistema_de_registro_de_docentes
{
    public partial class añadirDatosDotencesEmergente : Form
    {
        private DataRow usuarioActual;
        private string excelPath;
        public añadirDatosDotencesEmergente()
        {
            InitializeComponent();
            // Configurar el formulario emergente para que se comporte como una ventana modal
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            // Configurar los ComboBox
            comboBoxUnidadAcademica.Items.Add("UNIDAD ACADEMICA LA PAZ");
            comboBoxRol.Items.AddRange(new string[] { "ADMINISTRADOR", "ENCARGADO" });
            comboBoxNivelAcademico.Items.AddRange(new string[] { "LICENCIATURA", "MILITAR" });
            comboBoxExpedido.Items.AddRange(new string[] { "LP.", "CB.", "SC.", "CH.", "OR.", "BE.", "PA.", "PO.", "TA." });
        }
        public void SetUsuarioData(DataRow usuario, string excelPath)
        {
            usuarioActual = usuario;
            //MessageBox.Show(usuarioActual).ToString();
            this.excelPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\sportsc.xlsx"));
            CargarDatosUsuario();
        }
        private void CargarDatosUsuario()
        {
            if (usuarioActual != null)
            {
                txtNombres.Text = usuarioActual["Nombre"].ToString();
                txtApellidoPaterno.Text = usuarioActual["Apellido Paterno"].ToString();
                txtApellidoMaterno.Text = usuarioActual["Apellido Materno"].ToString();
                comboBoxUnidadAcademica.Text = usuarioActual["Unidad Academica"].ToString();
                comboBoxRol.Text = usuarioActual["rol"].ToString();
                txtCarnetIdentidad.Text = usuarioActual["Carnet de identidad"].ToString();
                comboBoxExpedido.Text = usuarioActual["Expedido"].ToString();
                txtDireccion.Text = usuarioActual["Direccion"].ToString();
                txtTelefono.Text = usuarioActual["Telefono"].ToString();
                txtCelular.Text = usuarioActual["Celular"].ToString();
                txtCorreoInstitucional.Text = usuarioActual["Correo Institucional"].ToString();
                txtEmail.Text = usuarioActual["Email Personal"].ToString();
                comboBoxNivelAcademico.Text = usuarioActual["Nivel Academico"].ToString();
                txtUsuario.Text = usuarioActual["Usuario"].ToString();
                txtContrasena.Text = usuarioActual["contrasena"].ToString();
                checkBoxEstado.Checked = usuarioActual["Estado"].ToString() == "ACTIVO";
            }
        }
        // Propiedad para almacenar el resultado del diálogo
        public bool Resultado { get; private set; }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubirImagen_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los campos de texto
            string nombres = txtNombres.Text.Trim();
            string apellidoPaterno = txtApellidoPaterno.Text.Trim();

            if (string.IsNullOrEmpty(nombres) || string.IsNullOrEmpty(apellidoPaterno))
            {
                MessageBox.Show("Por favor, complete los campos de Nombres y Apellido Paterno.");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.Title = "Seleccionar una imagen";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sourceFilePath = openFileDialog.FileName;
                // Crear el nombre del archivo basado en los campos de texto
                string fileName = $"{nombres}_{apellidoPaterno}.jpg";
                string destinationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\imagenes_usuarios");
                string destinationFilePath = Path.Combine(destinationDirectory, fileName);

                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                File.Copy(sourceFilePath, destinationFilePath, true);
                MessageBox.Show("Imagen subida y guardada exitosamente.");

                // Mostrar la imagen en el PictureBox
                //pictureBoxImagen.Image = Image.FromFile(destinationFilePath);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatosEnExcel();
        }
        private void GuardarDatosEnExcel()
        {
            string rutaexcel = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Resources\sportsc.xlsx");

            if (!File.Exists(rutaexcel))
            {
                MessageBox.Show($"Error: No se encontró el archivo Excel en la ruta especificada: {rutaexcel}");
                return;
            }

            try
            {
                using (var workbook = new XLWorkbook(rutaexcel))
                {
                    var worksheet = workbook.Worksheet("Credenciales");

                    // Obtener los índices de las columnas basándose en los nombres
                    var headers = worksheet.FirstRow().Cells().ToDictionary(c => c.Value.ToString(), c => c.Address.ColumnNumber);

                    // Obtener la primera fila vacía
                    var lastRowUsed = worksheet.LastRowUsed().RowNumber() + 1;

                    // Guardar los datos en la fila vacía
                    worksheet.Cell(lastRowUsed, headers["Nombre"]).Value = txtNombres.Text;
                    worksheet.Cell(lastRowUsed, headers["Apellido Paterno"]).Value = txtApellidoPaterno.Text;
                    worksheet.Cell(lastRowUsed, headers["Apellido Materno"]).Value = txtApellidoMaterno.Text;
                    worksheet.Cell(lastRowUsed, headers["Unidad Academica"]).Value = comboBoxUnidadAcademica.Text;
                    worksheet.Cell(lastRowUsed, headers["rol"]).Value = comboBoxRol.Text;
                    worksheet.Cell(lastRowUsed, headers["Carnet de identidad"]).Value = txtCarnetIdentidad.Text;
                    worksheet.Cell(lastRowUsed, headers["Expedido"]).Value = comboBoxExpedido.Text;
                    worksheet.Cell(lastRowUsed, headers["Direccion"]).Value = txtDireccion.Text;
                    worksheet.Cell(lastRowUsed, headers["Telefono"]).Value = txtTelefono.Text;
                    worksheet.Cell(lastRowUsed, headers["Celular"]).Value = txtCelular.Text;
                    worksheet.Cell(lastRowUsed, headers["Correo Institucional"]).Value = txtCorreoInstitucional.Text;
                    worksheet.Cell(lastRowUsed, headers["Email Personal"]).Value = txtEmail.Text;
                    worksheet.Cell(lastRowUsed, headers["Nivel Academico"]).Value = comboBoxNivelAcademico.Text;
                    worksheet.Cell(lastRowUsed, headers["usuario"]).Value = txtUsuario.Text;
                    worksheet.Cell(lastRowUsed, headers["contrasena"]).Value = txtContrasena.Text;
                    worksheet.Cell(lastRowUsed, headers["Estado"]).Value = checkBoxEstado.Checked ? "ACTIVO" : "INACTIVO";

                    workbook.Save();
                }

                MessageBox.Show("Datos guardados correctamente.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos en el archivo Excel: {ex.Message}");
            }
        }

        private void GuardarCambiosEnExcel()
        {
            try
            {
                if (!File.Exists(excelPath))
                {
                    MessageBox.Show($"Error: No se encontró el archivo Excel en la ruta especificada: {excelPath}");
                    return;
                }

                using (var workbook = new XLWorkbook(excelPath))
                {
                    var worksheet = workbook.Worksheet("Credenciales");
                    var rows = worksheet.RangeUsed().RowsUsed();

                    int newRowIndex = rows.Last().RowNumber() + 1;

                    var carnetDeIdentidadColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Carnet de identidad")?.Address.ColumnNumber ?? -1;
                    var expedidoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Expedido")?.Address.ColumnNumber ?? -1;
                    var nombreColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Nombre")?.Address.ColumnNumber ?? -1;
                    var apellidoPaternoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Apellido Paterno")?.Address.ColumnNumber ?? -1;
                    var apellidoMaternoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Apellido Materno")?.Address.ColumnNumber ?? -1;
                    var unidadAcademicaColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Unidad Academica")?.Address.ColumnNumber ?? -1;
                    var rolColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "rol")?.Address.ColumnNumber ?? -1;
                    var direccionColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Direccion")?.Address.ColumnNumber ?? -1;
                    var telefonoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Telefono")?.Address.ColumnNumber ?? -1;
                    var celularColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Celular")?.Address.ColumnNumber ?? -1;
                    var correoInstitucionalColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Correo Institucional")?.Address.ColumnNumber ?? -1;
                    var emailPersonalColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Email Personal")?.Address.ColumnNumber ?? -1;
                    var nivelAcademicoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Nivel Academico")?.Address.ColumnNumber ?? -1;
                    var usuarioColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "usuario")?.Address.ColumnNumber ?? -1;
                    var contrasenaColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "contrasena")?.Address.ColumnNumber ?? -1;
                    var estadoColIndex = worksheet.FirstRow().Cells().FirstOrDefault(c => c.Value.ToString() == "Estado")?.Address.ColumnNumber ?? -1;

                    if (carnetDeIdentidadColIndex == -1 || expedidoColIndex == -1 || nombreColIndex == -1 || apellidoPaternoColIndex == -1 || apellidoMaternoColIndex == -1 || unidadAcademicaColIndex == -1 || rolColIndex == -1 || direccionColIndex == -1 || telefonoColIndex == -1 || celularColIndex == -1 || correoInstitucionalColIndex == -1 || emailPersonalColIndex == -1 || nivelAcademicoColIndex == -1 || usuarioColIndex == -1 || contrasenaColIndex == -1 || estadoColIndex == -1)
                    {
                        MessageBox.Show("Error: No se encontraron todas las columnas necesarias en el archivo Excel.");
                        return;
                    }

                    worksheet.Cell(newRowIndex, carnetDeIdentidadColIndex).Value = usuarioActual["Carnet de identidad"].ToString();
                    worksheet.Cell(newRowIndex, expedidoColIndex).Value = usuarioActual["Expedido"].ToString();
                    worksheet.Cell(newRowIndex, nombreColIndex).Value = usuarioActual["Nombre"].ToString();
                    worksheet.Cell(newRowIndex, apellidoPaternoColIndex).Value = usuarioActual["Apellido Paterno"].ToString();
                    worksheet.Cell(newRowIndex, apellidoMaternoColIndex).Value = usuarioActual["Apellido Materno"].ToString();
                    worksheet.Cell(newRowIndex, unidadAcademicaColIndex).Value = usuarioActual["Unidad Academica"].ToString();
                    worksheet.Cell(newRowIndex, rolColIndex).Value = usuarioActual["rol"].ToString();
                    worksheet.Cell(newRowIndex, direccionColIndex).Value = usuarioActual["Direccion"].ToString();
                    worksheet.Cell(newRowIndex, telefonoColIndex).Value = usuarioActual["Telefono"].ToString();
                    worksheet.Cell(newRowIndex, celularColIndex).Value = usuarioActual["Celular"].ToString();
                    worksheet.Cell(newRowIndex, correoInstitucionalColIndex).Value = usuarioActual["Correo Institucional"].ToString();
                    worksheet.Cell(newRowIndex, emailPersonalColIndex).Value = usuarioActual["Email Personal"].ToString();
                    worksheet.Cell(newRowIndex, nivelAcademicoColIndex).Value = usuarioActual["Nivel Academico"].ToString();
                    worksheet.Cell(newRowIndex, usuarioColIndex).Value = usuarioActual["Usuario"].ToString();
                    worksheet.Cell(newRowIndex, contrasenaColIndex).Value = usuarioActual["contrasena"].ToString();
                    worksheet.Cell(newRowIndex, estadoColIndex).Value = usuarioActual["Estado"].ToString();

                    workbook.Save();
                    MessageBox.Show("Datos del usuario guardados correctamente.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los cambios en el archivo Excel: {ex.Message}");
            }
        }







    }
}
