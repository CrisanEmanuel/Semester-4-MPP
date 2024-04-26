using System;
using Model;

namespace Networking.ObjectProtocol
{
    public interface IRequest
    {
    }
    
    [Serializable]
    public class LoginRequest : IRequest
    {
        public LoginRequest(Angajat angajat)
        {
            Angajat = angajat;
        }
        public virtual Angajat Angajat { get; }
    }
    
    [Serializable]
    public class LogoutRequest : IRequest
    {
        public LogoutRequest(Angajat angajat)
        {
            Angajat = angajat;
        }
        public virtual Angajat Angajat { get; }
    }
    
    [Serializable]
    public class FindAllExcursiiRequest : IRequest
    {
    }
    
    [Serializable]
    public class FindAllExcursiiCautateRequest : IRequest
    {
        public FindAllExcursiiCautateRequest(string obiectivTuristic, int deLaOra, int panaLaOra)
        {
            ObiectivTuristic = obiectivTuristic;
            DeLaOra = deLaOra;
            PanaLaOra = panaLaOra;
        }
        public virtual string ObiectivTuristic { get; }
        public virtual int DeLaOra { get; }
        public virtual int PanaLaOra { get; }
    }
    
    [Serializable]
    public class AdaugaRezervareRequest : IRequest
    {
        public AdaugaRezervareRequest(Rezervare rezervare)
        {
            Rezervare = rezervare;
        }
        public virtual Rezervare Rezervare { get; }
    }
    
    [Serializable]
    public class UpdateNumarLocuriExcursieRequest : IRequest
    {
        public UpdateNumarLocuriExcursieRequest(Excursie excursie)
        {
            Excursie = excursie;
        }
        public virtual Excursie Excursie { get; }
    }
}