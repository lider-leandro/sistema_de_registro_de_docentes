namespace sistema_de_registro_de_docentes
{
    partial class formDocentes
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
            this.leer = new System.Windows.Forms.Button();
            this.buttonAgregarDocente = new System.Windows.Forms.Button();
            this.buttonElminarDocente = new System.Windows.Forms.Button();
            this.buttonImportarDatosDocente = new System.Windows.Forms.Button();
            this.panelDocentes = new System.Windows.Forms.Panel();
            this.panel_Docente2 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // leer
            // 
            this.leer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.leer.Location = new System.Drawing.Point(705, 168);
            this.leer.Name = "leer";
            this.leer.Size = new System.Drawing.Size(137, 52);
            this.leer.TabIndex = 2;
            this.leer.Text = "Actualizar";
            this.leer.UseVisualStyleBackColor = true;
            // 
            // buttonAgregarDocente
            // 
            this.buttonAgregarDocente.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonAgregarDocente.Location = new System.Drawing.Point(705, 247);
            this.buttonAgregarDocente.Name = "buttonAgregarDocente";
            this.buttonAgregarDocente.Size = new System.Drawing.Size(137, 52);
            this.buttonAgregarDocente.TabIndex = 4;
            this.buttonAgregarDocente.Text = "Agregar";
            this.buttonAgregarDocente.UseVisualStyleBackColor = true;
            this.buttonAgregarDocente.Click += new System.EventHandler(this.buttonAgregarDocente_Click);
            // 
            // buttonElminarDocente
            // 
            this.buttonElminarDocente.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonElminarDocente.Location = new System.Drawing.Point(705, 323);
            this.buttonElminarDocente.Name = "buttonElminarDocente";
            this.buttonElminarDocente.Size = new System.Drawing.Size(137, 52);
            this.buttonElminarDocente.TabIndex = 5;
            this.buttonElminarDocente.Text = "Eliminar";
            this.buttonElminarDocente.UseVisualStyleBackColor = true;
            this.buttonElminarDocente.Click += new System.EventHandler(this.buttonElminarDocente_Click);
            // 
            // buttonImportarDatosDocente
            // 
            this.buttonImportarDatosDocente.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonImportarDatosDocente.Location = new System.Drawing.Point(705, 401);
            this.buttonImportarDatosDocente.Name = "buttonImportarDatosDocente";
            this.buttonImportarDatosDocente.Size = new System.Drawing.Size(137, 52);
            this.buttonImportarDatosDocente.TabIndex = 6;
            this.buttonImportarDatosDocente.Text = "Importar";
            this.buttonImportarDatosDocente.UseVisualStyleBackColor = true;
            this.buttonImportarDatosDocente.Click += new System.EventHandler(this.buttonImportarDatosDocente_Click);
            // 
            // panelDocentes
            // 
            this.panelDocentes.BackColor = System.Drawing.Color.Transparent;
            this.panelDocentes.Location = new System.Drawing.Point(0, -1);
            this.panelDocentes.Name = "panelDocentes";
            this.panelDocentes.Size = new System.Drawing.Size(0, 0);
            this.panelDocentes.TabIndex = 7;
            // 
            // panel_Docente2
            // 
            this.panel_Docente2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_Docente2.Location = new System.Drawing.Point(0, 0);
            this.panel_Docente2.Name = "panel_Docente2";
            this.panel_Docente2.Size = new System.Drawing.Size(644, 629);
            this.panel_Docente2.TabIndex = 8;
            // 
            // formDocentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 629);
            this.Controls.Add(this.panel_Docente2);
            this.Controls.Add(this.panelDocentes);
            this.Controls.Add(this.buttonImportarDatosDocente);
            this.Controls.Add(this.buttonElminarDocente);
            this.Controls.Add(this.buttonAgregarDocente);
            this.Controls.Add(this.leer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formDocentes";
            this.Text = "formDocentes";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button leer;
        private System.Windows.Forms.Button buttonAgregarDocente;
        private System.Windows.Forms.Button buttonElminarDocente;
        private System.Windows.Forms.Button buttonImportarDatosDocente;
        private System.Windows.Forms.Panel panelDocentes;
        private System.Windows.Forms.Panel panel_Docente2;
    }
}