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
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.semPanel = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.leer = new System.Windows.Forms.Button();
            this.buttonAgregarDocente = new System.Windows.Forms.Button();
            this.carreraBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // semestreBox
            // 
            this.semestreBox.FormattingEnabled = true;
            this.semestreBox.Location = new System.Drawing.Point(156, 107);
            this.semestreBox.Margin = new System.Windows.Forms.Padding(2);
            this.semestreBox.Name = "semestreBox";
            this.semestreBox.Size = new System.Drawing.Size(174, 21);
            this.semestreBox.TabIndex = 0;
            this.semestreBox.SelectedIndexChanged += new System.EventHandler(this.semestreBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(83, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "SEMESTRE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "CARRERA:";
            // 
            // semPanel
            // 
            this.semPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.semPanel.Location = new System.Drawing.Point(73, 221);
            this.semPanel.Margin = new System.Windows.Forms.Padding(2);
            this.semPanel.Name = "semPanel";
            this.semPanel.SelectedIndex = 0;
            this.semPanel.Size = new System.Drawing.Size(572, 197);
            this.semPanel.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.leer);
            this.panel2.Controls.Add(this.buttonAgregarDocente);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(718, 70);
            this.panel2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Asignación de Horarios";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox1.BackColor = System.Drawing.Color.LimeGreen;
            this.pictureBox1.Image = global::sistema_de_registro_de_docentes.Properties.Resources.mas;
            this.pictureBox1.Location = new System.Drawing.Point(567, 27);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 16);
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
            this.leer.Location = new System.Drawing.Point(437, 19);
            this.leer.Margin = new System.Windows.Forms.Padding(2);
            this.leer.Name = "leer";
            this.leer.Size = new System.Drawing.Size(114, 32);
            this.leer.TabIndex = 2;
            this.leer.Text = "EXPORTAR";
            this.leer.UseVisualStyleBackColor = false;
            this.leer.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // buttonAgregarDocente
            // 
            this.buttonAgregarDocente.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonAgregarDocente.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonAgregarDocente.FlatAppearance.BorderSize = 0;
            this.buttonAgregarDocente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAgregarDocente.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregarDocente.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAgregarDocente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAgregarDocente.Location = new System.Drawing.Point(557, 19);
            this.buttonAgregarDocente.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAgregarDocente.Name = "buttonAgregarDocente";
            this.buttonAgregarDocente.Size = new System.Drawing.Size(114, 32);
            this.buttonAgregarDocente.TabIndex = 4;
            this.buttonAgregarDocente.Text = "AGREGAR";
            this.buttonAgregarDocente.UseVisualStyleBackColor = false;
            // 
            // carreraBox
            // 
            this.carreraBox.FormattingEnabled = true;
            this.carreraBox.Location = new System.Drawing.Point(156, 80);
            this.carreraBox.Name = "carreraBox";
            this.carreraBox.Size = new System.Drawing.Size(174, 21);
            this.carreraBox.TabIndex = 21;
            this.carreraBox.SelectedIndexChanged += new System.EventHandler(this.carreraBox_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(86, 151);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(539, 65);
            this.flowLayoutPanel1.TabIndex = 22;
            // 
            // formHorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(718, 436);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.carreraBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.semPanel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.semestreBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl semPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button leer;
        public System.Windows.Forms.Button buttonAgregarDocente;
        private System.Windows.Forms.ComboBox carreraBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}