using System;
using System.Windows.Forms;
using AgentieTurismCS.Domain;

namespace AgentieTurismCS
{
    public partial class RezervareForm : Form
    {
        private readonly Service.Service _service;
        private readonly Excursie _excursie;
        private readonly Form _form; 
        public RezervareForm(Service.Service service, Excursie excursie, Form form)
        {
            _service = service;
            _excursie = excursie;
            _form = form;
            InitializeComponent();
            Text = @"Rezervare";
            numeObiectivLabel.Text = excursie.ObiectivTuristic;
            nrLocuriLabel.Text = excursie.NrLocuriDisponibile.ToString();
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void rezervaButton_Click(object sender, EventArgs e)
        {
            if (numeClientTextBox.Text == "" || numarTelefonTextBox.Text == "" || numarLocuriTextBox.Text == "")
            {
                MessageBox.Show(@"Completati toate campurile!");
                return;
            }
            
            var numeClient = numeClientTextBox.Text;
            var numarTelefon = numarTelefonTextBox.Text;
            var nrLocuri = int.Parse(numarLocuriTextBox.Text);
            
            if (nrLocuri > _excursie.NrLocuriDisponibile)
            {
                MessageBox.Show(@"Nu sunt suficiente locuri disponibile!");
                return;
            }

            var rezervare = new Rezervare(numeClient, numarTelefon, nrLocuri, _excursie);
            _service.AddRezervare(rezervare);
            _excursie.NrLocuriDisponibile -= nrLocuri;
            _service.UpdateExcursie(_excursie);
            MessageBox.Show(@"Rezervare efectuata cu succes!");
            Close();
            _form.Show();
        }
        
        private void RezervareForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _form.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
            _form.Show();
        }
    }
}