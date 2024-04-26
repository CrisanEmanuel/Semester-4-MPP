using System;
using System.Windows.Forms;
using Model;
using Services;

namespace Client.Forms
{
    public partial class RezervareForm : Form
    {
        private readonly Excursie _excursie;
        private readonly Form _angajatForm;
        private readonly IService _server;
        public RezervareForm(IService server, Excursie excursie, Form angajatForm)
        {
            _server = server;
            _excursie = excursie;
            _angajatForm = angajatForm;
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
            _server.AddRezervare(rezervare);
            _excursie.NrLocuriDisponibile -= nrLocuri;
            _server.UpdateExcursie(_excursie);
            MessageBox.Show(@"Rezervare efectuata cu succes!");
            Close();
            _angajatForm.Show();
        }
        
        private void RezervareForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _angajatForm.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
            _angajatForm.Show();
        }
    }
}