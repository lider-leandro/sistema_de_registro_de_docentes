namespace sistema_de_registro_de_docentes
{
    partial class formRetrasos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.B_exportar = new System.Windows.Forms.Button();
            this.B_calcular = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxHorario = new System.Windows.Forms.ComboBox();
            this.comboBoxCarrera = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 49);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 35;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(849, 481);
            this.dataGridView1.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(0, 68);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(912, 564);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // B_exportar
            // 
            this.B_exportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_exportar.BackColor = System.Drawing.Color.DarkTurquoise;
            this.B_exportar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.B_exportar.FlatAppearance.BorderColor = System.Drawing.Color.DarkTurquoise;
            this.B_exportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B_exportar.ForeColor = System.Drawing.Color.White;
            this.B_exportar.Location = new System.Drawing.Point(551, 23);
            this.B_exportar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.B_exportar.Name = "B_exportar";
            this.B_exportar.Size = new System.Drawing.Size(152, 39);
            this.B_exportar.TabIndex = 9;
            this.B_exportar.Text = "EXPORTAR";
            this.B_exportar.UseVisualStyleBackColor = false;
            this.B_exportar.ClientSizeChanged += new System.EventHandler(this.B_exportar_Click);
            this.B_exportar.Click += new System.EventHandler(this.B_exportar_Click);
            // 
            // B_calcular
            // 
            this.B_calcular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_calcular.BackColor = System.Drawing.Color.LimeGreen;
            this.B_calcular.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.B_calcular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B_calcular.ForeColor = System.Drawing.Color.White;
            this.B_calcular.Location = new System.Drawing.Point(725, 23);
            this.B_calcular.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.B_calcular.Name = "B_calcular";
            this.B_calcular.Size = new System.Drawing.Size(152, 39);
            this.B_calcular.TabIndex = 8;
            this.B_calcular.Text = "SUBIR REPORTES";
            this.B_calcular.UseVisualStyleBackColor = false;
            this.B_calcular.Click += new System.EventHandler(this.B_calcular_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.comboBoxHorario);
            this.panel2.Controls.Add(this.comboBoxCarrera);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.B_exportar);
            this.panel2.Controls.Add(this.B_calcular);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(909, 86);
            this.panel2.TabIndex = 10;
            // 
            // comboBoxHorario
            // 
            this.comboBoxHorario.FormattingEnabled = true;
            this.comboBoxHorario.Items.AddRange(new object[] {
            "HORARIO NORMAL",
            "HORARIO INVERNAL"});
            this.comboBoxHorario.Location = new System.Drawing.Point(283, 50);
            this.comboBoxHorario.Name = "comboBoxHorario";
            this.comboBoxHorario.Size = new System.Drawing.Size(201, 24);
            this.comboBoxHorario.TabIndex = 11;
            this.comboBoxHorario.Text = "SELECCIONE EL HORARIO";
            // 
            // comboBoxCarrera
            // 
            this.comboBoxCarrera.FormattingEnabled = true;
            this.comboBoxCarrera.Items.AddRange(new object[] {
            "ING. DE TELECOMUNICACIONES",
            "ING. DE SISTEMAS",
            "ING. MECATRONICA",
            "ING. DE SISTEMAS ELECTRONICOS"});
            this.comboBoxCarrera.Location = new System.Drawing.Point(30, 50);
            this.comboBoxCarrera.Name = "comboBoxCarrera";
            this.comboBoxCarrera.Size = new System.Drawing.Size(227, 24);
            this.comboBoxCarrera.TabIndex = 10;
            this.comboBoxCarrera.Text = "SELECCIONE LA CARRERA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Detalle de Registros de Atrasos";
            // 
            // formRetrasos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(909, 629);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "formRetrasos";
            this.Text = "formRetrasos";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button B_exportar;
        private System.Windows.Forms.Button B_calcular;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxHorario;
        private System.Windows.Forms.ComboBox comboBoxCarrera;
    }
}