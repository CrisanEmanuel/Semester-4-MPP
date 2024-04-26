using System;
using System.Collections.Generic;
using Model;

namespace Networking.ObjectProtocol
{
    public interface IResponse
    {
    }
    
    [Serializable]
    public class OkResponse : IResponse
    {
		
    }
    
    [Serializable]
    public class UpdateResponse : IResponse
    {
        public UpdateResponse(object updatedObject)
        {
            UpdatedObject = updatedObject;
        }
        public virtual object UpdatedObject { get; }
    }
    
    [Serializable]
    public class ErrorResponse : IResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }
        public virtual string Message { get; }
    }
    
    [Serializable]
    public class FindAllExcursiiResponse : IResponse
    {
        public FindAllExcursiiResponse(IEnumerable<Excursie> excursii)
        {
            Excursii = excursii;
        }
        public virtual IEnumerable<Excursie> Excursii { get; }
    }
    
    [Serializable]
    public class FindExcursiiCautateResponse : IResponse
    {
        public FindExcursiiCautateResponse(IEnumerable<Excursie> excursii)
        {
            Excursii = excursii;
        }
        public virtual IEnumerable<Excursie> Excursii { get; }
    }
    
    [Serializable]
    public class AdaugaRezervareResponse : IResponse
    {
        public AdaugaRezervareResponse(Rezervare rezervare)
        {
            Rezervare = rezervare;
        }
        public virtual Rezervare Rezervare { get; }
    }
    
    [Serializable]
    public class UpdateNumarLocuriExcursieResponse : IResponse
    {
        public UpdateNumarLocuriExcursieResponse(Excursie excursie)
        {
            Excursie = excursie;
        }
        public virtual Excursie Excursie { get; }
    }
}