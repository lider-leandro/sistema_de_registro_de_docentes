using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sistema_de_registro_de_docentes
{
    public partial class detallesAtrasos : Form
    {

        public detallesAtrasos(DateTime inicio, DateTime final)
        {
            InitializeComponent();
            this.monthCalendar.Dock = DockStyle.Left;
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.MinDate = inicio;
            this.monthCalendar.MaxDate = final;
            // Optionally, you can set the Calendar to display the range between inicio and final
            DateTime[] range = Enumerable.Range(0, 1 + final.Subtract(inicio).Days)
                                         .Select(offset => inicio.AddDays(offset))
                                         .ToArray();
            this.monthCalendar.BoldedDates = range;

            this.monthCalendar.DateSelected += new DateRangeEventHandler(MonthCalendar_DateSelected);
            this.detailsTextBox.Size = new Size(300, 300);
            this.detailsTextBox.Multiline = true;
            this.detailsTextBox.ScrollBars = ScrollBars.Vertical;
        }
        private void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            // Update the detailsTextBox with details for the selected date
            // Example:
            this.detailsTextBox.Text = $"Detalles para la fecha: {e.Start.ToShortDateString()}";
        }

    }
}