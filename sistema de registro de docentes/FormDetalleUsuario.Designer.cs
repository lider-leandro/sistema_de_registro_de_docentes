namespace sistema_de_registro_de_docentes
{
    partial class FormDetalleUsuario
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button buttonCambiarContraseña;
            System.Windows.Forms.Button btnCancelar;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDetalleUsuario));
            System.Windows.Forms.Button buttonEditar;
            System.Windows.Forms.Button button2;
            this.lblCI = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblEmailPersonal = new System.Windows.Forms.Label();
            this.lblUnidadAcademica = new System.Windows.Forms.Label();
            this.lblNivelAcademico = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblCelular = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.lblExpedido = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBoxFoto = new System.Windows.Forms.PictureBox();
            buttonCambiarContraseña = new System.Windows.Forms.Button();
            btnCancelar = new System.Windows.Forms.Button();
            buttonEditar = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCambiarContraseña
            // 
            buttonCambiarContraseña.BackColor = System.Drawing.Color.LimeGreen;
            buttonCambiarContraseña.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonCambiarContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonCambiarContraseña.ForeColor = System.Drawing.Color.White;
            buttonCambiarContraseña.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCambiarContraseña.Location = new System.Drawing.Point(243, 405);
            buttonCambiarContraseña.Name = "buttonCambiarContraseña";
            buttonCambiarContraseña.Size = new System.Drawing.Size(170, 44);
            buttonCambiarContraseña.TabIndex = 24;
            buttonCambiarContraseña.Text = "Cambiar contraseña";
            buttonCambiarContraseña.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonCambiarContraseña.UseVisualStyleBackColor = false;
            buttonCambiarContraseña.Click += new System.EventHandler(this.buttonCambiarContraseña_Click);
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = System.Drawing.Color.Red;
            btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            btnCancelar.ForeColor = System.Drawing.Color.White;
            btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            btnCancelar.Location = new System.Drawing.Point(622, 12);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            btnCancelar.Size = new System.Drawing.Size(46, 42);
            btnCancelar.TabIndex = 23;
            btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // buttonEditar
            // 
            buttonEditar.BackColor = System.Drawing.Color.DarkOrange;
            buttonEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonEditar.ForeColor = System.Drawing.Color.White;
            buttonEditar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonEditar.Location = new System.Drawing.Point(419, 405);
            buttonEditar.Name = "buttonEditar";
            buttonEditar.Size = new System.Drawing.Size(112, 44);
            buttonEditar.TabIndex = 32;
            buttonEditar.Text = "Editar";
            buttonEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonEditar.UseVisualStyleBackColor = false;
            buttonEditar.Click += new System.EventHandler(this.buttonEditar_Click);
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.Red;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button2.ForeColor = System.Drawing.Color.White;
            button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button2.Location = new System.Drawing.Point(537, 405);
            button2.Name = "button2";
            button2.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            button2.Size = new System.Drawing.Size(131, 44);
            button2.TabIndex = 33;
            button2.Text = "Eliminar";
            button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            button2.UseVisualStyleBackColor = false;
            button2.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblCI
            // 
            this.lblCI.AutoSize = true;
            this.lblCI.Location = new System.Drawing.Point(426, 127);
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(63, 16);
            this.lblCI.TabIndex = 0;
            this.lblCI.Text = "CARNET";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(426, 100);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(143, 16);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "NOMBRE COMPLETO";
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaNacimiento.Location = new System.Drawing.Point(268, 285);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(129, 16);
            this.lblFechaNacimiento.TabIndex = 2;
            this.lblFechaNacimiento.Text = "Nivel Academico:";
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(426, 158);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(80, 16);
            this.lblDireccion.TabIndex = 3;
            this.lblDireccion.Text = "DIRECCION";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(426, 190);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(169, 16);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "CORREO INSTITUCIONAL";
            // 
            // lblEmailPersonal
            // 
            this.lblEmailPersonal.AutoSize = true;
            this.lblEmailPersonal.Location = new System.Drawing.Point(426, 221);
            this.lblEmailPersonal.Name = "lblEmailPersonal";
            this.lblEmailPersonal.Size = new System.Drawing.Size(122, 16);
            this.lblEmailPersonal.TabIndex = 5;
            this.lblEmailPersonal.Text = "EMAIL PERSONAL";
            // 
            // lblUnidadAcademica
            // 
            this.lblUnidadAcademica.AutoSize = true;
            this.lblUnidadAcademica.Location = new System.Drawing.Point(426, 253);
            this.lblUnidadAcademica.Name = "lblUnidadAcademica";
            this.lblUnidadAcademica.Size = new System.Drawing.Size(140, 16);
            this.lblUnidadAcademica.TabIndex = 6;
            this.lblUnidadAcademica.Text = "UNIDAD ACADEMICA";
            // 
            // lblNivelAcademico
            // 
            this.lblNivelAcademico.AutoSize = true;
            this.lblNivelAcademico.Location = new System.Drawing.Point(426, 285);
            this.lblNivelAcademico.Name = "lblNivelAcademico";
            this.lblNivelAcademico.Size = new System.Drawing.Size(127, 16);
            this.lblNivelAcademico.TabIndex = 7;
            this.lblNivelAcademico.Text = "NIVEL ACADEMICO";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(426, 316);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(79, 16);
            this.lblTelefono.TabIndex = 8;
            this.lblTelefono.Text = "TELEFONO";
            // 
            // lblCelular
            // 
            this.lblCelular.AutoSize = true;
            this.lblCelular.Location = new System.Drawing.Point(426, 345);
            this.lblCelular.Name = "lblCelular";
            this.lblCelular.Size = new System.Drawing.Size(68, 16);
            this.lblCelular.TabIndex = 9;
            this.lblCelular.Text = "CELULAR";
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.Location = new System.Drawing.Point(136, 37);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(52, 25);
            this.lblRol.TabIndex = 10;
            this.lblRol.Text = "ROL";
            // 
            // lblExpedido
            // 
            this.lblExpedido.AutoSize = true;
            this.lblExpedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpedido.Location = new System.Drawing.Point(268, 316);
            this.lblExpedido.Name = "lblExpedido";
            this.lblExpedido.Size = new System.Drawing.Size(73, 16);
            this.lblExpedido.TabIndex = 11;
            this.lblExpedido.Text = "Telefono:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(268, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Nombre Completo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(268, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "Carnet de identidad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(268, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "Direccion:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(268, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Correo Institucional:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(268, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Email Personal:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(268, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "Unidad Academica:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(268, 345);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 16);
            this.label7.TabIndex = 31;
            this.label7.Text = "Celular:";
            // 
            // pictureBoxFoto
            // 
            this.pictureBoxFoto.Location = new System.Drawing.Point(35, 149);
            this.pictureBoxFoto.Name = "pictureBoxFoto";
            this.pictureBoxFoto.Size = new System.Drawing.Size(200, 162);
            this.pictureBoxFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFoto.TabIndex = 12;
            this.pictureBoxFoto.TabStop = false;
            // 
            // FormDetalleUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 471);
            this.Controls.Add(button2);
            this.Controls.Add(buttonEditar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(buttonCambiarContraseña);
            this.Controls.Add(btnCancelar);
            this.Controls.Add(this.pictureBoxFoto);
            this.Controls.Add(this.lblExpedido);
            this.Controls.Add(this.lblRol);
            this.Controls.Add(this.lblCelular);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.lblNivelAcademico);
            this.Controls.Add(this.lblUnidadAcademica);
            this.Controls.Add(this.lblEmailPersonal);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblDireccion);
            this.Controls.Add(this.lblFechaNacimiento);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.lblCI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDetalleUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormDetalleUsuario";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCI;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblFechaNacimiento;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblEmailPersonal;
        private System.Windows.Forms.Label lblUnidadAcademica;
        private System.Windows.Forms.Label lblNivelAcademico;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblCelular;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Label lblExpedido;
        private System.Windows.Forms.PictureBox pictureBoxFoto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}