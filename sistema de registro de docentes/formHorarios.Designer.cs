namespace sistema_de_registro_de_docentes
{
    partial class formHorarios
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
            this.semestreBox = new System.Windows.Forms.ComboBox();
            this.carreraBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.semPanel = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.leer = new System.Windows.Forms.Button();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.flowMaterias = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // semestreBox
            // 
            this.semestreBox.FormattingEnabled = true;
            this.semestreBox.Location = new System.Drawing.Point(512, 107);
            this.semestreBox.Name = "semestreBox";
            this.semestreBox.Size = new System.Drawing.Size(231, 24);
            this.semestreBox.TabIndex = 0;
            // 
            // carreraBox
            // 
            this.carreraBox.FormattingEnabled = true;
            this.carreraBox.Location = new System.Drawing.Point(125, 104);
            this.carreraBox.Name = "carreraBox";
            this.carreraBox.Size = new System.Drawing.Size(231, 24);
            this.carreraBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(421, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "SEMESTRE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "CARRERA:";
            // 
            // semPanel
            // 
            this.semPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.semPanel.Location = new System.Drawing.Point(12, 267);
            this.semPanel.Name = "semPanel";
            this.semPanel.SelectedIndex = 0;
            this.semPanel.Size = new System.Drawing.Size(949, 318);
            this.semPanel.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.leer);
            this.panel2.Controls.Add(this.buttonGuardar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(973, 86);
            this.panel2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Asignación de Horarios";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox1.BackColor = System.Drawing.Color.LimeGreen;
            this.pictureBox1.Image = global::sistema_de_registro_de_docentes.Properties.Resources.mas;
            this.pictureBox1.Location = new System.Drawing.Point(802, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // leer
            // 
            this.leer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.leer.BackColor = System.Drawing.Color.DarkTurquoise;
            this.leer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.leer.FlatAppearance.BorderSize = 0;
            this.leer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.leer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leer.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.leer.Location = new System.Drawing.Point(603, 33);
            this.leer.Name = "leer";
            this.leer.Size = new System.Drawing.Size(152, 39);
            this.leer.TabIndex = 2;
            this.leer.Text = "EXPORTAR";
            this.leer.UseVisualStyleBackColor = false;
            this.leer.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonGuardar.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonGuardar.FlatAppearance.BorderSize = 0;
            this.buttonGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGuardar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGuardar.Location = new System.Drawing.Point(789, 33);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(152, 39);
            this.buttonGuardar.TabIndex = 4;
            this.buttonGuardar.Text = "GUARDAR";
            this.buttonGuardar.UseVisualStyleBackColor = false;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // flowMaterias
            // 
            this.flowMaterias.Location = new System.Drawing.Point(13, 137);
            this.flowMaterias.Name = "flowMaterias";
            this.flowMaterias.Size = new System.Drawing.Size(928, 105);
            this.flowMaterias.TabIndex = 19;
            // 
            // formHorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(973, 611);
            this.Controls.Add(this.flowMaterias);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.semPanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.carreraBox);
            this.Controls.Add(this.semestreBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formHorarios";
            this.Text = "formHorarios";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox semestreBox;
        private System.Windows.Forms.ComboBox carreraBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl semPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button leer;
        public System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.FlowLayoutPanel flowMaterias;
    }
}