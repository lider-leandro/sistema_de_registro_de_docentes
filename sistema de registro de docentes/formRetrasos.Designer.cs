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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.B_calcular = new System.Windows.Forms.Button();
            this.B_exportar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Formulario";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(82, 119);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(663, 22);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(153, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(282, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 41);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // B_calcular
            // 
            this.B_calcular.Location = new System.Drawing.Point(442, 202);
            this.B_calcular.Name = "B_calcular";
            this.B_calcular.Size = new System.Drawing.Size(88, 41);
            this.B_calcular.TabIndex = 4;
            this.B_calcular.Text = "llenar";
            this.B_calcular.UseVisualStyleBackColor = true;
            this.B_calcular.Click += new System.EventHandler(this.B_calcular_Click);
            // 
            // B_exportar
            // 
            this.B_exportar.Location = new System.Drawing.Point(568, 202);
            this.B_exportar.Name = "B_exportar";
            this.B_exportar.Size = new System.Drawing.Size(88, 41);
            this.B_exportar.TabIndex = 5;
            this.B_exportar.Text = "Exportar";
            this.B_exportar.UseVisualStyleBackColor = true;
            this.B_exportar.Click += new System.EventHandler(this.B_exportar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(96, 282);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(664, 299);
            this.dataGridView1.TabIndex = 6;
            // 
            // formRetrasos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 629);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.B_exportar);
            this.Controls.Add(this.B_calcular);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formRetrasos";
            this.Text = "formRetrasos";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button B_calcular;
        private System.Windows.Forms.Button B_exportar;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}