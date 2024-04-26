using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Client.Controllers;
using Client.Events;
using Model;
using Services;

namespace Client.Forms
{
    public partial class AngajatForm : Form
    {
        private readonly Form _logInForm;
        private readonly IService _server;
        private readonly AngajatController _angajatController;

        public AngajatForm(AngajatController angajatController, IService server, string angajatUsername, Form logInForm)
        {
            
            _logInForm = logInForm;
            _angajatController = angajatController;
            _server = server;
            _angajatController.UpdateEvent += UserUpdate;
            InitializeComponent();
            Text = @"Welcome, " + angajatUsername;
            LoadExcursiiData();
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }
        
        private void LoadExcursiiData()
        {
            var excursii = _server.FindAllExcursii();
            
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Guid));
            dataTable.Columns.Add("Obiectiv Turistic", typeof(string));
            dataTable.Columns.Add("Firma de transport", typeof(string));
            dataTable.Columns.Add("Ora de plecare", typeof(DateTime));
            dataTable.Columns.Add("Nr locuri disponibile", typeof(int));
            dataTable.Columns.Add("Pret", typeof(double));

            foreach (var excursie in excursii)
            {
                dataTable.Rows.Add(excursie.Id, excursie.ObiectivTuristic, excursie.NumeFirmaTransport, excursie.OraPlecare,
                    excursie.NrLocuriDisponibile, excursie.Pret);
            }
            dataGridViewToateExcursiile.DataSource = dataTable;
            
            // Hide the "ID" column
            dataGridViewToateExcursiile.Columns["Id"]!.Visible = false;
            
            foreach (DataGridViewColumn column in dataGridViewToateExcursiile.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void cautaButton_click(object sender, EventArgs e)
        {
            var numeObiectivTuristic = obiectivTuristicTextBox.Text;
            var deLaOra = int.Parse(deLaOraTextBox.Text);
            var panaLaOra = int.Parse(panaLaOraTextBox.Text);
            IEnumerable<Excursie> excursii = new List<Excursie>();
            try
            {
                excursii = _server.CautaExcursii(numeObiectivTuristic, deLaOra, panaLaOra);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(this, @"Cautare excursii error " + ex.Message/* + ex.StackTrace*/, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Guid));
            dataTable.Columns.Add("Obiectiv Turistic", typeof(string));
            dataTable.Columns.Add("Firma de transport", typeof(string));
            dataTable.Columns.Add("Ora de plecare", typeof(DateTime));
            dataTable.Columns.Add("Nr locuri disponibile", typeof(int));
            dataTable.Columns.Add("Pret", typeof(double));

            foreach (var excursie in excursii)
            {
                dataTable.Rows.Add(excursie.Id, excursie.ObiectivTuristic, excursie.NumeFirmaTransport, excursie.OraPlecare,
                    excursie.NrLocuriDisponibile, excursie.Pret);
            }
            dataGridViewExcursiiCautate.DataSource = dataTable;
            
            // Hide the "ID" column
            dataGridViewExcursiiCautate.Columns["Id"]!.Visible = false;
            
            foreach (DataGridViewColumn column in dataGridViewExcursiiCautate.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        
        private void dataGridViewToateExcursiile_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridViewToateExcursiile.Columns[e.ColumnIndex].Name != "Nr locuri disponibile") return;
            // Check if the value of "Nr locuri disponibile" is 0
            if (e.Value == null || e.Value.ToString() != "0") return;
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X + 2, e.CellBounds.Y + 2);
            e.Graphics.FillRectangle(Brushes.Tomato, e.CellBounds);
            e.PaintContent(e.CellBounds);
            e.Handled = true;  
        }
        
        private void dataGridViewExcursiiCautate_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dataGridViewExcursiiCautate.Columns[e.ColumnIndex].Name != "Nr locuri disponibile") return;
            // Check if the value of "Nr locuri disponibile" is 0
            if (e.Value == null || e.Value.ToString() != "0") return;
            e.PaintBackground(e.CellBounds, true);
            e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.White, e.CellBounds.X + 2, e.CellBounds.Y + 2);
            e.Graphics.FillRectangle(Brushes.Tomato, e.CellBounds);
            e.PaintContent(e.CellBounds);
            e.Handled = true;
        }


        private void rezervaButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewExcursiiCautate.SelectedCells.Count > 0)
            {
                // Get the index of the first selected cell
                var selectedCellIndex = dataGridViewExcursiiCautate.SelectedCells[0].RowIndex;

                // Retrieve information of the selected row based on the selected cell index
                var id = (Guid)dataGridViewExcursiiCautate.Rows[selectedCellIndex].Cells["Id"].Value;
                var obiectivTuristic = dataGridViewExcursiiCautate.Rows[selectedCellIndex].Cells["Obiectiv Turistic"].Value.ToString();
                var firmaTransport = dataGridViewExcursiiCautate.Rows[selectedCellIndex].Cells["Firma de transport"].Value.ToString();
                var oraPlecare = (DateTime)dataGridViewExcursiiCautate.Rows[selectedCellIndex].Cells["Ora de plecare"].Value;
                var nrLocuriDisponibile = (int)dataGridViewExcursiiCautate.Rows[selectedCellIndex].Cells["Nr locuri disponibile"].Value;
                var pret = (double)dataGridViewExcursiiCautate.Rows[selectedCellIndex].Cells["Pret"].Value;

                var excursie = new Excursie(obiectivTuristic, firmaTransport, oraPlecare, nrLocuriDisponibile, pret)
                    {
                        Id = id
                    };
                var rezervareForm = new RezervareForm(_server, excursie, this);
                dataGridViewExcursiiCautate.DataSource = null;
                rezervareForm.Show();
                Hide();
            }
            else
            {
                MessageBox.Show(@"Selectati o excursie!");
            }
        }
        
        private void AngajatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _angajatController.Logout();
                _angajatController.UpdateEvent -= UserUpdate;
                MessageBox.Show(@"Logged out!");
                _logInForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, @"Logout Error " + ex.Message/* + ex.StackTrace*/, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void logOutButton_click(object sender, EventArgs e)
        {
            try
            {
                Close(); // Calls AngajatForm_FormClosing
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, @"Logout Error " + ex.Message/* + ex.StackTrace*/, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void UserUpdate(object sender, AgentieAngajatEventArgs e)
        {
            if (e.AngajatEventType != AgentieAngajatEvent.UpdatedNrLocuriExcursie) return;
            
            if (e.Data is not Excursie excursie) return;

            dataGridViewToateExcursiile.BeginInvoke((MethodInvoker) delegate
            {
                for (var indexRow = 0; indexRow < dataGridViewToateExcursiile.Rows.Count; indexRow++)
                {
                    var row = dataGridViewToateExcursiile.Rows[indexRow];
                    if (row.Cells["Id"].Value.ToString() != excursie.Id.ToString()) continue;
                    row.Cells["Nr locuri disponibile"].Value = excursie.NrLocuriDisponibile;
                    dataGridViewToateExcursiile.InvalidateRow(indexRow);
                    break;
                }
            });
        }

        private void obiectivTuristic_Enter(object sender, EventArgs e)
        {
            if (obiectivTuristicTextBox.Text != @" nume obiectiv") return;
            obiectivTuristicTextBox.Text = @"";
            obiectivTuristicTextBox.ForeColor = Color.Black;
        }
        
        private void obiectivTuristic_Leave(object sender, EventArgs e)
        {
            if (obiectivTuristicTextBox.Text != @"") return;
            obiectivTuristicTextBox.Text = @" nume obiectiv";
            obiectivTuristicTextBox.ForeColor = Color.Silver;
        }
        
        private void deLaOra_Enter(object sender, EventArgs e)
        {
            if (deLaOraTextBox.Text != @" ora") return;
            deLaOraTextBox.Text = @"";
            deLaOraTextBox.ForeColor = Color.Black;
        }
        
        private void deLaOra_Leave(object sender, EventArgs e)
        {
            if (deLaOraTextBox.Text != @"") return;
            deLaOraTextBox.Text = @" ora";
            deLaOraTextBox.ForeColor = Color.Silver;
        }
        
        private void panaLaOra_Enter(object sender, EventArgs e)
        {
            if (panaLaOraTextBox.Text != @" ora") return;
            panaLaOraTextBox.Text = @"";
            panaLaOraTextBox.ForeColor = Color.Black;
        }
        
        private void panaLaOra_Leave(object sender, EventArgs e)
        {
            if (panaLaOraTextBox.Text != @"") return;
            panaLaOraTextBox.Text = @" ora";
            panaLaOraTextBox.ForeColor = Color.Silver;
        }
    }
}