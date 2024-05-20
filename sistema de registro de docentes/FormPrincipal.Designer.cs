namespace sistema_de_registro_de_docentes
{
    partial class FormPrincipal
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
            this.panelMenuLateral = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Docentes = new System.Windows.Forms.Button();
            this.buttonHorarios = new System.Windows.Forms.Button();
            this.buttonRetrasos = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.panelHijo = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelMenuLateral.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenuLateral
            // 
            this.panelMenuLateral.BackColor = System.Drawing.Color.DarkBlue;
            this.panelMenuLateral.Controls.Add(this.button5);
            this.panelMenuLateral.Controls.Add(this.buttonRetrasos);
            this.panelMenuLateral.Controls.Add(this.buttonHorarios);
            this.panelMenuLateral.Controls.Add(this.btn_Docentes);
            this.panelMenuLateral.Controls.Add(this.panel1);
            this.panelMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenuLateral.Location = new System.Drawing.Point(0, 0);
            this.panelMenuLateral.Name = "panelMenuLateral";
            this.panelMenuLateral.Size = new System.Drawing.Size(250, 629);
            this.panelMenuLateral.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 174);
            this.panel1.TabIndex = 6;
            // 
            // btn_Docentes
            // 
            this.btn_Docentes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Docentes.Location = new System.Drawing.Point(0, 174);
            this.btn_Docentes.Name = "btn_Docentes";
            this.btn_Docentes.Size = new System.Drawing.Size(250, 74);
            this.btn_Docentes.TabIndex = 6;
            this.btn_Docentes.Text = "Docentes";
            this.btn_Docentes.UseVisualStyleBackColor = true;
            this.btn_Docentes.Click += new System.EventHandler(this.btn_Docentes_Click);
            // 
            // buttonHorarios
            // 
            this.buttonHorarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonHorarios.Location = new System.Drawing.Point(0, 248);
            this.buttonHorarios.Name = "buttonHorarios";
            this.buttonHorarios.Size = new System.Drawing.Size(250, 74);
            this.buttonHorarios.TabIndex = 7;
            this.buttonHorarios.Text = "Horarios";
            this.buttonHorarios.UseVisualStyleBackColor = true;
            this.buttonHorarios.Click += new System.EventHandler(this.buttonHorarios_Click);
            // 
            // buttonRetrasos
            // 
            this.buttonRetrasos.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonRetrasos.Location = new System.Drawing.Point(0, 322);
            this.buttonRetrasos.Name = "buttonRetrasos";
            this.buttonRetrasos.Size = new System.Drawing.Size(250, 74);
            this.buttonRetrasos.TabIndex = 8;
            this.buttonRetrasos.Text = "Retrasos";
            this.buttonRetrasos.UseVisualStyleBackColor = true;
            this.buttonRetrasos.Click += new System.EventHandler(this.buttonRetrasos_Click);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Top;
            this.button5.Location = new System.Drawing.Point(0, 396);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(250, 74);
            this.button5.TabIndex = 9;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // panelHijo
            // 
            this.panelHijo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHijo.BackColor = System.Drawing.Color.Transparent;
            this.panelHijo.BackgroundImage = global::sistema_de_registro_de_docentes.Properties.Resources.fondologin;
            this.panelHijo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelHijo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHijo.Location = new System.Drawing.Point(250, 0);
            this.panelHijo.Name = "panelHijo";
            this.panelHijo.Size = new System.Drawing.Size(909, 629);
            this.panelHijo.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::sistema_de_registro_de_docentes.Properties.Resources.logoEmi;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(62, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 65);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 629);
            this.Controls.Add(this.panelHijo);
            this.Controls.Add(this.panelMenuLateral);
            this.MinimumSize = new System.Drawing.Size(1177, 676);
            this.Name = "FormPrincipal";
            this.Text = "FormPrincipal";
            this.panelMenuLateral.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenuLateral;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonRetrasos;
        private System.Windows.Forms.Button buttonHorarios;
        private System.Windows.Forms.Button btn_Docentes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelHijo;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}