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
            this.comboBoxMateria = new System.Windows.Forms.ComboBox();
            this.comboBoxDocente = new System.Windows.Forms.ComboBox();
            this.comBoxSalida = new System.Windows.Forms.ComboBox();
            this.comBoxEntrada = new System.Windows.Forms.ComboBox();
            this.carreraBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.semPanel = new System.Windows.Forms.TabControl();
            this.agregarButton = new System.Windows.Forms.Button();
            this.actualizarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // semestreBox
            // 
            this.semestreBox.FormattingEnabled = true;
            this.semestreBox.Location = new System.Drawing.Point(197, 72);
            this.semestreBox.Name = "semestreBox";
            this.semestreBox.Size = new System.Drawing.Size(231, 24);
            this.semestreBox.TabIndex = 0;
            // 
            // comboBoxDia
            // 
            this.comboBoxDia.FormattingEnabled = true;
            this.comboBoxDia.Location = new System.Drawing.Point(197, 162);
            this.comboBoxDia.Name = "comboBoxDia";
            this.comboBoxDia.Size = new System.Drawing.Size(121, 24);
            this.comboBoxDia.TabIndex = 1;
            // 
            // comboBoxMateria
            // 
            this.comboBoxMateria.FormattingEnabled = true;
            this.comboBoxMateria.Location = new System.Drawing.Point(197, 132);
            this.comboBoxMateria.Name = "comboBoxMateria";
            this.comboBoxMateria.Size = new System.Drawing.Size(231, 24);
            this.comboBoxMateria.TabIndex = 2;
            // 
            // comboBoxDocente
            // 
            this.comboBoxDocente.FormattingEnabled = true;
            this.comboBoxDocente.Location = new System.Drawing.Point(197, 102);
            this.comboBoxDocente.Name = "comboBoxDocente";
            this.comboBoxDocente.Size = new System.Drawing.Size(231, 24);
            this.comboBoxDocente.TabIndex = 3;
            // 
            // comBoxSalida
            // 
            this.comBoxSalida.FormattingEnabled = true;
            this.comBoxSalida.Location = new System.Drawing.Point(589, 178);
            this.comBoxSalida.Name = "comBoxSalida";
            this.comBoxSalida.Size = new System.Drawing.Size(95, 24);
            this.comBoxSalida.TabIndex = 6;
            // 
            // comBoxEntrada
            // 
            this.comBoxEntrada.FormattingEnabled = true;
            this.comBoxEntrada.Location = new System.Drawing.Point(465, 177);
            this.comBoxEntrada.Name = "comBoxEntrada";
            this.comBoxEntrada.Size = new System.Drawing.Size(103, 24);
            this.comBoxEntrada.TabIndex = 5;
            // 
            // carreraBox
            // 
            this.carreraBox.FormattingEnabled = true;
            this.carreraBox.Location = new System.Drawing.Point(589, 72);
            this.carreraBox.Name = "carreraBox";
            this.carreraBox.Size = new System.Drawing.Size(182, 24);
            this.carreraBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(353, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "ASIGNACION DE HORARIOS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "SEMESTRE:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "DOCENTE:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "MATERIA:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(67, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "DIA:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(492, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "CARRERA:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(462, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Hora de Entrada";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(586, 140);
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
            this.semPanel.Size = new System.Drawing.Size(685, 189);
            this.semPanel.TabIndex = 15;
            // 
            // agregarButton
            // 
            this.agregarButton.BackColor = System.Drawing.Color.LimeGreen;
            this.agregarButton.FlatAppearance.BorderSize = 0;
            this.agregarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agregarButton.ForeColor = System.Drawing.Color.White;
            this.agregarButton.Location = new System.Drawing.Point(705, 132);
            this.agregarButton.Name = "agregarButton";
            this.agregarButton.Size = new System.Drawing.Size(152, 39);
            this.agregarButton.TabIndex = 16;
            this.agregarButton.Text = "Agregar";
            this.agregarButton.UseVisualStyleBackColor = false;
            this.agregarButton.Click += new System.EventHandler(this.agregarButton_Click_1);
            // 
            // actualizarButton
            // 
            this.actualizarButton.BackColor = System.Drawing.Color.DarkTurquoise;
            this.actualizarButton.FlatAppearance.BorderSize = 0;
            this.actualizarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actualizarButton.ForeColor = System.Drawing.Color.White;
            this.actualizarButton.Location = new System.Drawing.Point(705, 178);
            this.actualizarButton.Name = "actualizarButton";
            this.actualizarButton.Size = new System.Drawing.Size(152, 39);
            this.actualizarButton.TabIndex = 17;
            this.actualizarButton.Text = "Actualizar";
            this.actualizarButton.UseVisualStyleBackColor = false;
            this.actualizarButton.ClientSizeChanged += new System.EventHandler(this.cancelarButton_Click);
            // 
            // formHorarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(880, 537);
            this.Controls.Add(this.actualizarButton);
            this.Controls.Add(this.agregarButton);
            this.Controls.Add(this.semPanel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comBoxSalida);
            this.Controls.Add(this.comBoxEntrada);
            this.Controls.Add(this.carreraBox);
            this.Controls.Add(this.comboBoxDocente);
            this.Controls.Add(this.comboBoxMateria);
            this.Controls.Add(this.comboBoxDia);
            this.Controls.Add(this.semestreBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formHorarios";
            this.Text = "formHorarios";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox semestreBox;
        private System.Windows.Forms.ComboBox comboBoxDia;
        private System.Windows.Forms.ComboBox comboBoxMateria;
        private System.Windows.Forms.ComboBox comboBoxDocente;
        private System.Windows.Forms.ComboBox comBoxSalida;
        private System.Windows.Forms.ComboBox comBoxEntrada;
        private System.Windows.Forms.ComboBox carreraBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl semPanel;
        private System.Windows.Forms.Button agregarButton;
        private System.Windows.Forms.Button actualizarButton;
    }
}