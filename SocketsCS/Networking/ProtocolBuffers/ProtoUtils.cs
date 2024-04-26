using System;
using System.Collections.Generic;
using AgentieProto;
using Angajat = Model.Angajat;
using Excursie = Model.Excursie;

namespace Networking.ProtocolBuffers
{
    public abstract class ProtoUtils
    {
        public static AgentieResponse CreateFindAllExcursiiResponse(IEnumerable<Excursie> toateExcursiile)
        {
            var response = new AgentieResponse { Type = AgentieResponse.Types.Type.ToateExcursiile };
            foreach (var excursie in toateExcursiile)
            {
                response.ToateExcursiile.Add(BuildExcursieProtoFromExcursie(excursie));
            }
            return response;
        }
        
        public static AgentieResponse CreateCautaExcursiiResponse(IEnumerable<Excursie> excursiiCautate)
        {
            var response = new AgentieResponse { Type = AgentieResponse.Types.Type.ExcursiiCautate };
            foreach (var excursie in excursiiCautate)
            {
                response.ExcursiiCautate.Add(BuildExcursieProtoFromExcursie(excursie));
            }
            return response;
        }

        public static AgentieResponse CreateUpdateResponse(Excursie excursie)
        {
            return new AgentieResponse
            {
                Type = AgentieResponse.Types.Type.UpdatedNumarLocuriExcursie,
                UpdatedExcursie = BuildExcursieProtoFromExcursie(excursie)
            };
        }

        public static AgentieResponse CreateOkResponse()
        {
            var response = new AgentieResponse { Type = AgentieResponse.Types.Type.Ok };
            return response;
        }
        
        public static AgentieResponse CreateErrorResponse(string text)
        {
            var response = new AgentieResponse { Type = AgentieResponse.Types.Type.Error, Error = text };
            return response;
        }
        
        public static Angajat GetAngajat(AgentieRequest request)
        {
            var angajat = new Angajat(request.Angajat.Username, request.Angajat.Password);
            return angajat;
        }
        
        private static AgentieProto.Excursie BuildExcursieProtoFromExcursie(Excursie excursieModel)
        {
            var excursieProto = new AgentieProto.Excursie
            {
                Uuid = excursieModel.Id.ToString(),
                ObiectivTuristic = excursieModel.ObiectivTuristic,
                NumeFirmaTransport = excursieModel.NumeFirmaTransport,
                OraPlecare = excursieModel.OraPlecare.ToString("yyyy-MM-ddTHH:mm:ss"),
                NrLocuriDisponibile = excursieModel.NrLocuriDisponibile,
                Pret = excursieModel.Pret
            };
            return excursieProto;
        }
        
        public static Excursie BuildExcursieFromExcursieProto(AgentieProto.Excursie excursieProto)
        {
            var excursieModel = new Excursie
            {
                Id = Guid.Parse(excursieProto.Uuid),
                ObiectivTuristic = excursieProto.ObiectivTuristic,
                NumeFirmaTransport = excursieProto.NumeFirmaTransport,
                OraPlecare = DateTime.Parse(excursieProto.OraPlecare),
                NrLocuriDisponibile = excursieProto.NrLocuriDisponibile,
                Pret = excursieProto.Pret
            };
            return excursieModel;
        }

        public static Model.Rezervare BuildRezervareFromRezervareProto(Rezervare rezervareProto)
        {
            var rezervareModel = new Model.Rezervare
            {
                Id = Guid.Parse(rezervareProto.Uuid),
                NumeClient = rezervareProto.NumeClient,
                NumarTelefonClient = rezervareProto.NumarTelefonClient,
                NrBilete = rezervareProto.NrBilete,
                Excursie = BuildExcursieFromExcursieProto(rezervareProto.Excursie)
            };
            return rezervareModel;

        }
    }
}