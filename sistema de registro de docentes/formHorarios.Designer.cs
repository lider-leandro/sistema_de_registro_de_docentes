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
            this.comboBoxDia = new System.Windows.Forms.ComboBox();
            this.comBoxSalida = new System.Windows.Forms.ComboBox();
            this.comBoxEntrada = new System.Windows.Forms.ComboBox();
            this.carreraBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.semPanel = new System.Windows.Forms.TabControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.leer = new System.Windows.Forms.Button();
            this.buttonAgregarDocente = new System.Windows.Forms.Button();
            this.comboBoxDocente = new System.Windows.Forms.ComboBox();
            this.comboBoxMateria = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // semestreBox
            // 
            this.semestreBox.FormattingEnabled = true;
            this.semestreBox.Location = new System.Drawing.Point(208, 132);
            this.semestreBox.Name = "semestreBox";
            this.semestreBox.Size = new System.Drawing.Size(231, 24);
            this.semestreBox.TabIndex = 0;
            // 
            // comboBoxDia
            // 
            this.comboBoxDia.FormattingEnabled = true;
            this.comboBoxDia.Location = new System.Drawing.Point(208, 222);
            this.comboBoxDia.Name = "comboBoxDia";
            this.comboBoxDia.Size = new System.Drawing.Size(231, 24);
            this.comboBoxDia.TabIndex = 1;
            // 
            // comBoxSalida
            // 
            this.comBoxSalida.FormattingEnabled = true;
            this.comBoxSalida.Location = new System.Drawing.Point(634, 220);
            this.comBoxSalida.Name = "comBoxSalida";
            this.comBoxSalida.Size = new System.Drawing.Size(95, 24);
            this.comBoxSalida.TabIndex = 6;
            // 
            // comBoxEntrada
            // 
            this.comBoxEntrada.FormattingEnabled = true;
            this.comBoxEntrada.Location = new System.Drawing.Point(510, 219);
            this.comBoxEntrada.Name = "comBoxEntrada";
            this.comBoxEntrada.Size = new System.Drawing.Size(103, 24);
            this.comBoxEntrada.TabIndex = 5;
            // 
            // carreraBox
            // 
            this.carreraBox.FormattingEnabled = true;
            this.carreraBox.Location = new System.Drawing.Point(208, 102);
            this.carreraBox.Name = "carreraBox";
            this.carreraBox.Size = new System.Drawing.Size(231, 24);
            this.carreraBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "SEMESTRE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "DOCENTE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "MATERIA:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "DIA:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "CARRERA:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(507, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Hora de Entrada";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(631, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "Hora de Salida";
            // 
            // semPanel
            // 
            this.semPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.semPanel.Location = new System.Drawing.Point(97, 272);
            this.semPanel.Name = "semPanel";
            this.semPanel.SelectedIndex = 0;
            this.semPanel.Size = new System.Drawing.Size(685, 243);
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
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 86);
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
            this.pictureBox1.Location = new System.Drawing.Point(709, 43);
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
            this.leer.Location = new System.Drawing.Point(510, 33);
            this.leer.Name = "leer";
            this.leer.Size = new System.Drawing.Size(152, 39);
            this.leer.TabIndex = 2;
            this.leer.Text = "ACTUALIZAR";
            this.leer.UseVisualStyleBackColor = false;
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
            this.buttonAgregarDocente.Location = new System.Drawing.Point(696, 33);
            this.buttonAgregarDocente.Name = "buttonAgregarDocente";
            this.buttonAgregarDocente.Size = new System.Drawing.Size(152, 39);
            this.buttonAgregarDocente.TabIndex = 4;
            this.buttonAgregarDocente.Text = "AGREGAR";
            this.buttonAgregarDocente.UseVisualStyleBackColor = false;
            this.buttonAgregarDocente.Click += new System.EventHandler(this.agregarButton_Click_1);
            // 
            // comboBoxDocente
            // 
            this.comboBoxDocente.FormattingEnabled = true;
            this.comboBoxDocente.Location = new System.Drawing.Point(208, 162);
            this.comboBoxDocente.Name = "comboBoxDocente";
            this.comboBoxDocente.Size = new System.Drawing.Size(231, 24);
            this.comboBoxDocente.TabIndex = 19;
            // 
            // comboBoxMateria
            // 
            this.comboBoxMateria.FormattingEnabled = true;
            this.comboBoxMateria.Location = new System.Drawing.Point(208, 192);
            this.comboBoxMateria.Name = "comboBoxMateria";
            this.comboBoxMateria.Size = new System.Drawing.Size(273, 24);
            this.comboBoxMateria.TabIndex = 20;
            // 
            // formHorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(880, 537);
            this.Controls.Add(this.comboBoxMateria);
            this.Controls.Add(this.comboBoxDocente);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.semPanel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comBoxSalida);
            this.Controls.Add(this.comBoxEntrada);
            this.Controls.Add(this.carreraBox);
            this.Controls.Add(this.comboBoxDia);
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
        private System.Windows.Forms.ComboBox comboBoxDia;
        private System.Windows.Forms.ComboBox comBoxSalida;
        private System.Windows.Forms.ComboBox comBoxEntrada;
        private System.Windows.Forms.ComboBox carreraBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl semPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button leer;
        public System.Windows.Forms.Button buttonAgregarDocente;
        private System.Windows.Forms.ComboBox comboBoxDocente;
        private System.Windows.Forms.ComboBox comboBoxMateria;
    }
}