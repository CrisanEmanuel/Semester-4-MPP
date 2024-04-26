using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Controllers;
using Client.Forms;
using Networking.ObjectProtocol;
using Services;

namespace Client
{
    static class StartClient
    {
        private static int _defaultPort = 55556;
        private static string _defaultIp = "127.0.0.1";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var port = _defaultPort;
            var ip = _defaultIp;
            var portS= ConfigurationManager.AppSettings["port"];
            if (portS == null)
            {
                Console.WriteLine(@"Port property not set. Using default value " + _defaultPort);
            }
            else
            {
                var result = int.TryParse(portS, out port);
                if (!result)
                {
                    Console.WriteLine(@"Port property not a number. Using default value " + _defaultPort);
                    port = _defaultPort;
                    Console.WriteLine(@"Portul " + port);
                }
            }
            var ipS= ConfigurationManager.AppSettings["ip"];
            if (ipS == null)
            {
                Console.WriteLine(@"Port property not set. Using default value " + _defaultIp);
            }
            Console.WriteLine(@"Using  server on IP {0} and port {1}", ip, port);
            
            IService server = new AgentieServerObjectProxy(ip, port);
            var window = new LoginForm(server);
            Application.Run(window);
        }
    }
}