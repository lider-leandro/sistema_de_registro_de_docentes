namespace sistema_de_registro_de_docentes
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.paneltitulo = new System.Windows.Forms.Panel();
            this.tituloLogin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.usuario = new System.Windows.Forms.TextBox();
            this.contrasena = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ingresar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // paneltitulo
            // 
            this.paneltitulo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.paneltitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltitulo.Location = new System.Drawing.Point(0, 0);
            this.paneltitulo.Name = "paneltitulo";
            this.paneltitulo.Size = new System.Drawing.Size(621, 30);
            this.paneltitulo.TabIndex = 0;
            // 
            // tituloLogin
            // 
            this.tituloLogin.AutoSize = true;
            this.tituloLogin.Font = new System.Drawing.Font("Ravie", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloLogin.Location = new System.Drawing.Point(50, 185);
            this.tituloLogin.Name = "tituloLogin";
            this.tituloLogin.Size = new System.Drawing.Size(521, 80);
            this.tituloLogin.TabIndex = 3;
            this.tituloLogin.Text = "Registro de Asistencia \r\n        de Docentes";
            this.tituloLogin.Click += new System.EventHandler(this.tituloLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Berlin Sans FB", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Inicio de Sesión";
            // 
            // usuario
            // 
            this.usuario.Font = new System.Drawing.Font("Yu Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usuario.ForeColor = System.Drawing.SystemColors.MenuText;
            this.usuario.Location = new System.Drawing.Point(174, 334);
            this.usuario.Name = "usuario";
            this.usuario.Size = new System.Drawing.Size(250, 35);
            this.usuario.TabIndex = 5;
            this.usuario.Text = "Ingrese su usuario";
            this.usuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.usuario.UseWaitCursor = true;
            this.usuario.Enter += new System.EventHandler(this.usuario_Enter);
            this.usuario.Leave += new System.EventHandler(this.usuario_Leave);
            // 
            // contrasena
            // 
            this.contrasena.Font = new System.Drawing.Font("Yu Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrasena.ForeColor = System.Drawing.SystemColors.MenuText;
            this.contrasena.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.contrasena.Location = new System.Drawing.Point(174, 392);
            this.contrasena.MaxLength = 10;
            this.contrasena.Name = "contrasena";
            this.contrasena.Size = new System.Drawing.Size(250, 35);
            this.contrasena.TabIndex = 6;
            this.contrasena.Text = "Ingrese su contraseña";
            this.contrasena.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.contrasena.UseWaitCursor = true;
            this.contrasena.Enter += new System.EventHandler(this.contrasena_Enter);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Yu Gothic", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(386, 440);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 21);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Mostrar";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ingresar
            // 
            this.ingresar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ingresar.Location = new System.Drawing.Point(237, 462);
            this.ingresar.Name = "ingresar";
            this.ingresar.Size = new System.Drawing.Size(125, 41);
            this.ingresar.TabIndex = 8;
            this.ingresar.Text = "INGRESAR";
            this.ingresar.UseVisualStyleBackColor = false;
            this.ingresar.Click += new System.EventHandler(this.ingresar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::sistema_de_registro_de_docentes.Properties.Resources.logoEmi;
            this.pictureBox1.Location = new System.Drawing.Point(186, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 113);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 629);
            this.Controls.Add(this.ingresar);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.contrasena);
            this.Controls.Add(this.usuario);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tituloLogin);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.paneltitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel paneltitulo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label tituloLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox usuario;
        private System.Windows.Forms.TextBox contrasena;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button ingresar;
    }
}

