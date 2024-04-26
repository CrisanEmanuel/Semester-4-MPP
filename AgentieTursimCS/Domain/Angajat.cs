using System;

namespace AgentieTurismCS.Domain
{
    public class Angajat : Entity<Guid>
    {
        private string _nume;
        private string _prenume;
        private string _username;
        private string _password;
        private string _agentieTurism;

        public Angajat(string nume, string prenume, string username, string password, string agentieTurism)
        {
            Id = Guid.NewGuid();
            _nume = nume;
            _prenume = prenume;
            _username = username;
            _password = password;
            _agentieTurism = agentieTurism;
        }

        public string Nume
        {
            get => _nume;
            set => _nume = value;
        }

        public string Prenume
        {
            get => _prenume;
            set => _prenume = value;
        }

        public string Username
        {
            get => _username;
            set => _username = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string AgentieTurism
        {
            get => _agentieTurism;
            set => _agentieTurism = value;
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (!(obj is Angajat angajat)) return false;
            return _nume == angajat._nume &&
                   _prenume == angajat._prenume &&
                   _username == angajat._username &&
                   _password == angajat._password &&
                   _agentieTurism == angajat._agentieTurism;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(_nume, _prenume, _username, _password, _agentieTurism);
        }



        public override string ToString()
        {
            return
                $"Angajat{{nume='{_nume}', prenume='{_prenume}', username='{_username}', password='{_password}', agentieTurism='{_agentieTurism}'}}";
        }
    }
}