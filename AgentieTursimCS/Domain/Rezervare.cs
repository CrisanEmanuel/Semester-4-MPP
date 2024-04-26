#nullable enable
using System;

namespace AgentieTurismCS.Domain
{
    public class Rezervare : Entity<Guid>
    {
        private string _numeClient;
        private string _numarTelefonClient;
        private int _nrBilete;
        private Excursie _excursie;

        public Rezervare(string numeClient, string numarTelefonClient, int nrBilete, Excursie excursie)
        {
            Id = Guid.NewGuid();
            _numeClient = numeClient;
            _numarTelefonClient = numarTelefonClient;
            _nrBilete = nrBilete;
            _excursie = excursie;
        }

        public string NumeClient
        {
            get => _numeClient;
            set => _numeClient = value;
        }

        public string NumarTelefonClient
        {
            get => _numarTelefonClient;
            set => _numarTelefonClient = value;
        }

        public int NrBilete
        {
            get => _nrBilete;
            set => _nrBilete = value;
        }

        public Excursie Excursie
        {
            get => _excursie;
            set => _excursie = value;
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (!(obj is Rezervare rezervare)) return false;
            return _nrBilete == rezervare._nrBilete && _numeClient == rezervare._numeClient && _numarTelefonClient == rezervare._numarTelefonClient && _excursie.Equals(rezervare._excursie);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_numeClient, _numarTelefonClient, _nrBilete, _excursie);
        }

        public override string ToString()
        {
            return $"Rezervare{{numeClient='{_numeClient}', numarTelefonClient='{_numarTelefonClient}', nrBilete={_nrBilete}, excursie={_excursie}}}";
        }
    }
}