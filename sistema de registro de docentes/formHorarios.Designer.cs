namespace Horarios
{
    partial class ASIGNACION_HORARIOS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Titulo = new System.Windows.Forms.Label();
            this.registro = new System.Windows.Forms.DataGridView();
            this.Export = new System.Windows.Forms.Button();
            this.ImportButton = new System.Windows.Forms.Button();
            this.CancelarButton = new System.Windows.Forms.Button();
            this.QuitarButton = new System.Windows.Forms.Button();
            this.materiaBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.docenteBox = new System.Windows.Forms.ComboBox();
            this.semestreBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GuardarButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.registro)).BeginInit();
            this.SuspendLayout();
            // 
            // Titulo
            // 
            this.Titulo.BackColor = System.Drawing.Color.White;
            this.Titulo.Enabled = false;
            this.Titulo.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulo.ForeColor = System.Drawing.Color.Black;
            this.Titulo.Location = new System.Drawing.Point(13, 193);
            this.Titulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(732, 26);
            this.Titulo.TabIndex = 35;
            this.Titulo.Text = "HORARIO";
            this.Titulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // registro
            // 
            this.registro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.registro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.registro.Location = new System.Drawing.Point(38, 223);
            this.registro.Margin = new System.Windows.Forms.Padding(4);
            this.registro.Name = "registro";
            this.registro.RowHeadersWidth = 51;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registro.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.registro.Size = new System.Drawing.Size(678, 321);
            this.registro.TabIndex = 34;
            // 
            // Export
            // 
            this.Export.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Export.Location = new System.Drawing.Point(700, 99);
            this.Export.Margin = new System.Windows.Forms.Padding(4);
            this.Export.Name = "Export";
            this.Export.Size = new System.Drawing.Size(115, 37);
            this.Export.TabIndex = 33;
            this.Export.Text = "Exportar";
            this.Export.UseVisualStyleBackColor = true;
            this.Export.Click += new System.EventHandler(this.Export_Click);
            // 
            // ImportButton
            // 
            this.ImportButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportButton.Location = new System.Drawing.Point(547, 99);
            this.ImportButton.Margin = new System.Windows.Forms.Padding(4);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(116, 37);
            this.ImportButton.TabIndex = 32;
            this.ImportButton.Text = "Importar";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click_1);
            // 
            // CancelarButton
            // 
            this.CancelarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelarButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelarButton.Location = new System.Drawing.Point(753, 415);
            this.CancelarButton.Margin = new System.Windows.Forms.Padding(4);
            this.CancelarButton.Name = "CancelarButton";
            this.CancelarButton.Size = new System.Drawing.Size(96, 37);
            this.CancelarButton.TabIndex = 31;
            this.CancelarButton.Text = "Cancelar";
            this.CancelarButton.UseVisualStyleBackColor = true;
            this.CancelarButton.Click += new System.EventHandler(this.CancelarButton_Click);
            // 
            // QuitarButton
            // 
            this.QuitarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.QuitarButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuitarButton.Location = new System.Drawing.Point(753, 357);
            this.QuitarButton.Margin = new System.Windows.Forms.Padding(4);
            this.QuitarButton.Name = "QuitarButton";
            this.QuitarButton.Size = new System.Drawing.Size(96, 37);
            this.QuitarButton.TabIndex = 30;
            this.QuitarButton.Text = "Quitar";
            this.QuitarButton.UseVisualStyleBackColor = true;
            this.QuitarButton.Click += new System.EventHandler(this.QuitarButton_Click);
            // 
            // materiaBox
            // 
            this.materiaBox.FormattingEnabled = true;
            this.materiaBox.Location = new System.Drawing.Point(298, 146);
            this.materiaBox.Margin = new System.Windows.Forms.Padding(4);
            this.materiaBox.Name = "materiaBox";
            this.materiaBox.Size = new System.Drawing.Size(160, 24);
            this.materiaBox.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 26);
            this.label2.TabIndex = 28;
            this.label2.Text = "Elija la Materia:";
            // 
            // docenteBox
            // 
            this.docenteBox.FormattingEnabled = true;
            this.docenteBox.Location = new System.Drawing.Point(298, 99);
            this.docenteBox.Margin = new System.Windows.Forms.Padding(4);
            this.docenteBox.Name = "docenteBox";
            this.docenteBox.Size = new System.Drawing.Size(160, 24);
            this.docenteBox.TabIndex = 27;
            // 
            // semestreBox
            // 
            this.semestreBox.FormattingEnabled = true;
            this.semestreBox.Location = new System.Drawing.Point(298, 55);
            this.semestreBox.Margin = new System.Windows.Forms.Padding(4);
            this.semestreBox.Name = "semestreBox";
            this.semestreBox.Size = new System.Drawing.Size(160, 24);
            this.semestreBox.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(15, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 26);
            this.label4.TabIndex = 25;
            this.label4.Text = "Seleccione el Docente:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(15, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(257, 26);
            this.label3.TabIndex = 24;
            this.label3.Text = "Seleccione el Semestre:";
            // 
            // GuardarButton
            // 
            this.GuardarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GuardarButton.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GuardarButton.Location = new System.Drawing.Point(753, 301);
            this.GuardarButton.Margin = new System.Windows.Forms.Padding(4);
            this.GuardarButton.Name = "GuardarButton";
            this.GuardarButton.Size = new System.Drawing.Size(96, 37);
            this.GuardarButton.TabIndex = 23;
            this.GuardarButton.Text = "Guardar";
            this.GuardarButton.UseVisualStyleBackColor = true;
            this.GuardarButton.Click += new System.EventHandler(this.GuardarButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(228, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(404, 28);
            this.label1.TabIndex = 22;
            this.label1.Text = "ASIGNACION DE HORARIOS";
            // 
            // ASIGNACION_HORARIOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 582);
            this.Controls.Add(this.Titulo);
            this.Controls.Add(this.registro);
            this.Controls.Add(this.Export);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.CancelarButton);
            this.Controls.Add(this.QuitarButton);
            this.Controls.Add(this.materiaBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.docenteBox);
            this.Controls.Add(this.semestreBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GuardarButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ASIGNACION_HORARIOS";
            this.Text = "formHorarios";
            ((System.ComponentModel.ISupportInitialize)(this.registro)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.DataGridView registro;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button CancelarButton;
        private System.Windows.Forms.Button QuitarButton;
        private System.Windows.Forms.ComboBox materiaBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox docenteBox;
        private System.Windows.Forms.ComboBox semestreBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GuardarButton;
        private System.Windows.Forms.Label label1;
    }
}