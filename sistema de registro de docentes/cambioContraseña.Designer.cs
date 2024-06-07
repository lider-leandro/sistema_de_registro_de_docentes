namespace sistema_de_registro_de_docentes
{
    partial class cambioContraseña
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCambioContraseña = new System.Windows.Forms.TextBox();
            buttonCambiarContraseña = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonCambiarContraseña
            // 
            buttonCambiarContraseña.BackColor = System.Drawing.Color.LimeGreen;
            buttonCambiarContraseña.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonCambiarContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonCambiarContraseña.ForeColor = System.Drawing.Color.White;
            buttonCambiarContraseña.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCambiarContraseña.Location = new System.Drawing.Point(131, 175);
            buttonCambiarContraseña.Name = "buttonCambiarContraseña";
            buttonCambiarContraseña.Size = new System.Drawing.Size(170, 44);
            buttonCambiarContraseña.TabIndex = 25;
            buttonCambiarContraseña.Text = "Cambiar contraseña";
            buttonCambiarContraseña.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            buttonCambiarContraseña.UseVisualStyleBackColor = false;
            buttonCambiarContraseña.Click += new System.EventHandler(this.buttonCambiarContraseña_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 25);
            this.label2.TabIndex = 26;
            this.label2.Text = "Introdusca la nueva contraseña";
            // 
            // textBoxCambioContraseña
            // 
            this.textBoxCambioContraseña.BackColor = System.Drawing.Color.White;
            this.textBoxCambioContraseña.Font = new System.Drawing.Font("Yu Gothic Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCambioContraseña.Location = new System.Drawing.Point(104, 115);
            this.textBoxCambioContraseña.Name = "textBoxCambioContraseña";
            this.textBoxCambioContraseña.Size = new System.Drawing.Size(221, 36);
            this.textBoxCambioContraseña.TabIndex = 27;
            // 
            // cambioContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 300);
            this.Controls.Add(this.textBoxCambioContraseña);
            this.Controls.Add(this.label2);
            this.Controls.Add(buttonCambiarContraseña);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "cambioContraseña";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cambioContraseña";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCambioContraseña;
    }
}