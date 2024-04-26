using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AgentieTurismCS.Domain;
using AgentieTurismCS.Repository.DBRepo;
using AgentieTurismCS.Utils;

namespace AgentieTurismCS.Tests
{
    public static class Test
    {
        public static void RunTests(IDictionary<string, string> props)
        {

            var repoAngajat = new RepoAngajat(props);
            var repoExcursie = new RepoExcursie(props);
            var repoRezervare = new RepoRezervare(props);

            // var angajat = new Angajat("test", "test", "test", "test", "test");
            // Debug.Assert(repoAngajat.Save(angajat) is null);
            // Debug.Assert(repoAngajat.FindOne(angajat.Id) is not null);
            // Debug.Assert(repoAngajat.FindAll().Count() == 1);
            // angajat.Nume = "test2";
            // Debug.Assert(repoAngajat.Update(angajat) is null);
            // Debug.Assert(repoAngajat.Delete(angajat.Id) is not null);
            //
            // var excursie = new Excursie("test", "test", DateTime.Now, 10, 10);
            // Debug.Assert(repoExcursie.Save(excursie) is null);
            // Debug.Assert(repoExcursie.FindOne(excursie.Id) is not null);
            // Debug.Assert(repoExcursie.FindAll().Count() == 1);
            // excursie.NrLocuriDisponibile = 5;
            // Debug.Assert(repoExcursie.Update(excursie) is null);
            //
            // var rezervare = new Rezervare("test", "test", 1, excursie);
            // Debug.Assert(repoRezervare.Save(rezervare) is null);
            // Debug.Assert(repoRezervare.FindOne(rezervare.Id) is not null);
            // Debug.Assert(repoRezervare.FindAll().Count() == 1);
            // rezervare.NumeClient = "test2";
            // Debug.Assert(repoRezervare.Update(rezervare) is null);
            // Debug.Assert(repoExcursie.Delete(excursie.Id) is not null); // cascade delete should delete the rezervare 
            // Console.WriteLine(@"Tests passed successfully!");
            //
            // add angajat back for login
            // var hashedPass1 = Password.HashPassword("test1");
            // var angajat1 = new Angajat("test1", "test1", "test1", hashedPass1, "test1");
            // var hashedPass2 = Password.HashPassword("test2");
            // var angajat2 = new Angajat("test2", "test2", "test2", hashedPass2, "test2");
            // repoAngajat.Save(angajat1);
            // repoAngajat.Save(angajat2);
            
            // var exc1 = new Excursie("Eiffel Tower", "TransportCompany1", new DateTime(2024, 4, 10, 8, 0, 0), 20, 150.00);
            // var exc2 = new Excursie("Machu Picchu", "TransportCompany2", new DateTime(2024, 4, 15, 9, 30, 0), 15, 200.00);
            // var exc3 = new Excursie("Colosseum", "TransportCompany3", new DateTime(2024, 5, 1, 7, 45, 0), 25, 300.00);
            // var exc4 = new Excursie("Turnul CN", "TransportCompany4", new DateTime(2024, 5, 10, 10, 15, 0), 10, 250.00);
            // var exc5 = new Excursie("Opera din Sydney", "TransportCompany5", new DateTime(2024, 6, 5, 8, 0, 0), 30, 180.00);
            // var exc6 = new Excursie("Opera din Paris", "TransportCompany6", new DateTime(2024, 6, 20, 7, 30, 0), 12, 350.00);
            // var exc7 = new Excursie("Marea Piramidă a lui Khufu", "TransportCompany7", new DateTime(2024, 7, 1, 9, 0, 0), 18, 400.00);
            // repoExcursie.Save(exc1);
            // repoExcursie.Save(exc2);
            // repoExcursie.Save(exc3);
            // repoExcursie.Save(exc4);
            // repoExcursie.Save(exc5);
            // repoExcursie.Save(exc6);
            // repoExcursie.Save(exc7);
            
        }
    }
}