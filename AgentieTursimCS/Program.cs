using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AgentieTurismCS.Repository.DBRepo;
using AgentieTurismCS.Tests;
using log4net.Config;

namespace AgentieTurismCS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            const string xmlFile = @"log4net.xml";
            XmlConfigurator.Configure(new System.IO.FileInfo(xmlFile));
            Console.WriteLine(@"Configuration Settings for agentiedeturism {0}", GetConnectionStringByName("agentiedeturism"));
            IDictionary<string, string> props = new SortedList<string, string>();
            props.Add("ConnectionString", GetConnectionStringByName("agentiedeturism"));

            Console.WriteLine(@"agentie de turism db ...");
            //Test.RunTests(props);
            
            var repoAngajat = new RepoAngajat(props);
            var repoExcursie = new RepoExcursie(props);
            var repoRezervare = new RepoRezervare(props);
            
            var angajatService = new Service.AngajatService(repoAngajat);
            var excursieService = new Service.ExcursieService(repoExcursie);
            var rezervareService = new Service.RezervareService(repoRezervare);
            
            var service = new Service.Service(angajatService, excursieService, rezervareService);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(service));
        }

        private static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            var settings = ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}