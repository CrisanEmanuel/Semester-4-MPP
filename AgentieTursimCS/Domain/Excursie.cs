#nullable enable
using System;

namespace AgentieTurismCS.Domain
{
    public class Excursie : Entity<Guid>
    {
        private string _obiectivTuristic;
        private string _numeFirmaTransport;
        private DateTime _oraPlecare;
        private int _nrLocuriDisponibile;
        private double _pret;

        public Excursie(string obiectivTuristic, string numeFirmaTransport, DateTime oraPlecare, int nrLocuriDisponibile, double pret)
        {
            Id = Guid.NewGuid();
            _obiectivTuristic = obiectivTuristic;
            _numeFirmaTransport = numeFirmaTransport;
            _oraPlecare = oraPlecare;
            _nrLocuriDisponibile = nrLocuriDisponibile;
            _pret = pret;
        }

        public string ObiectivTuristic
        {
            get => _obiectivTuristic;
            set => _obiectivTuristic = value;
        }

        public string NumeFirmaTransport
        {
            get => _numeFirmaTransport;
            set => _numeFirmaTransport = value;
        }

        public DateTime OraPlecare
        {
            get => _oraPlecare;
            set => _oraPlecare = value;
        }

        public int NrLocuriDisponibile
        {
            get => _nrLocuriDisponibile;
            set => _nrLocuriDisponibile = value;
        }

        public double Pret
        {
            get => _pret;
            set => _pret = value;
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj is not Excursie excursie) return false;
            return _nrLocuriDisponibile == excursie._nrLocuriDisponibile && _pret.Equals(excursie._pret) && _obiectivTuristic == excursie._obiectivTuristic && _numeFirmaTransport == excursie._numeFirmaTransport && _oraPlecare == excursie._oraPlecare;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_obiectivTuristic, _numeFirmaTransport, _oraPlecare, _nrLocuriDisponibile, _pret);
        }

        public override string ToString()
        {
            return $"Excursie{{obiectivTuristic='{_obiectivTuristic}', numeFirmaTransport='{_numeFirmaTransport}', oraPlecare={_oraPlecare}, nrLocuriDisponibile={_nrLocuriDisponibile}, pret={_pret}}}";
        }
    }
}