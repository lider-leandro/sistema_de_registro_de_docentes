using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace sistema_de_registro_de_docentes
{
    public partial class editarDatosUsuario : Form
    {
        private DataRow usuarioActual;
        private string excelPath;
        private string carnetIdentidadOriginal;

        public editarDatosUsuario()
        {
            InitializeComponent();
            comboBoxUnidadAcademica.Items.Add("UNIDAD ACADEMICA LA PAZ");
            comboBoxRol.Items.AddRange(new string[] { "ADMINISTRADOR", "ENCARGADO" });
            comboBoxNivelAcademico.Items.AddRange(new string[] { "LICENCIATURA", "MILITAR" });
            comboBoxExpedido.Items.AddRange(new string[] { "LP.", "CB.", "SC.", "CH.", "OR.", "BE.", "PA.", "PO.", "TA." });
        }

        public void SetUsuarioData(DataRow usuario, string excelPath)
        {
            this.usuarioActual = usuario;
            this.excelPath = excelPath;
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
                carnetIdentidadOriginal = txtCarnetIdentidad.Text;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (usuarioActual != null)
            {
                usuarioActual["Nombre"] = txtNombres.Text;
                usuarioActual["Apellido Paterno"] = txtApellidoPaterno.Text;
                usuarioActual["Apellido Materno"] = txtApellidoMaterno.Text;
                usuarioActual["Unidad Academica"] = comboBoxUnidadAcademica.Text;
                usuarioActual["rol"] = comboBoxRol.Text;
                usuarioActual["Carnet de identidad"] = txtCarnetIdentidad.Text;
                usuarioActual["Expedido"] = comboBoxExpedido.Text;
                usuarioActual["Direccion"] = txtDireccion.Text;
                usuarioActual["Telefono"] = txtTelefono.Text;
                usuarioActual["Celular"] = txtCelular.Text;
                usuarioActual["Correo Institucional"] = txtCorreoInstitucional.Text;
                usuarioActual["Email Personal"] = txtEmail.Text;
                usuarioActual["Nivel Academico"] = comboBoxNivelAcademico.Text;
                usuarioActual["Usuario"] = txtUsuario.Text;
                usuarioActual["contrasena"] = txtContrasena.Text;
                usuarioActual["Estado"] = checkBoxEstado.Checked ? "ACTIVO" : "INACTIVO";
                GuardarCambiosEnExcel();
            }
        }

        private void GuardarCambiosEnExcel()
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

                    // Buscar la fila donde coincide el Carnet de identidad original
                    var rowToUpdate = worksheet.RowsUsed().Where(r =>
                        r.Cell(headers["Carnet de identidad"]).GetString() == carnetIdentidadOriginal).FirstOrDefault();

                    if (rowToUpdate != null)
                    {
                        // Actualizar los datos en la fila encontrada
                        rowToUpdate.Cell(headers["Nombre"]).Value = txtNombres.Text;
                        rowToUpdate.Cell(headers["Apellido Paterno"]).Value = txtApellidoPaterno.Text;
                        rowToUpdate.Cell(headers["Apellido Materno"]).Value = txtApellidoMaterno.Text;
                        rowToUpdate.Cell(headers["Unidad Academica"]).Value = comboBoxUnidadAcademica.Text;
                        rowToUpdate.Cell(headers["rol"]).Value = comboBoxRol.Text;
                        rowToUpdate.Cell(headers["Carnet de identidad"]).Value = txtCarnetIdentidad.Text;
                        rowToUpdate.Cell(headers["Expedido"]).Value = comboBoxExpedido.Text;
                        rowToUpdate.Cell(headers["Direccion"]).Value = txtDireccion.Text;
                        rowToUpdate.Cell(headers["Telefono"]).Value = txtTelefono.Text;
                        rowToUpdate.Cell(headers["Celular"]).Value = txtCelular.Text;
                        rowToUpdate.Cell(headers["Correo Institucional"]).Value = txtCorreoInstitucional.Text;
                        rowToUpdate.Cell(headers["Email Personal"]).Value = txtEmail.Text;
                        rowToUpdate.Cell(headers["Nivel Academico"]).Value = comboBoxNivelAcademico.Text;
                        rowToUpdate.Cell(headers["usuario"]).Value = txtUsuario.Text;
                        rowToUpdate.Cell(headers["contrasena"]).Value = txtContrasena.Text;
                        rowToUpdate.Cell(headers["Estado"]).Value = checkBoxEstado.Checked ? "ACTIVO" : "INACTIVO";
                    }
                    else
                    {
                        MessageBox.Show("Error: No se encontró el usuario en el archivo Excel.");
                        return;
                    }

                    workbook.Save();
                    MessageBox.Show("Datos del usuario actualizados correctamente.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos en el archivo Excel: {ex.Message}");
            }
        }



        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
