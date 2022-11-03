using BattleshipsCore;
using BattleshipsCore.Game;
using BattleshipsCore.Server;
using System.Diagnostics;
using System.Net;




Client client = new Client(new Facade());

client.startGame();



        

ServerLogger.Instance.ShowTimestamp = true;
var selectedIpAddress = await SelectIpAddressAsync();
var commandParser = new GameMessageParser();

ServerLogger.Instance.LogInfo("Starting server;");
using (var serverListener = new AsyncSocketServer(selectedIpAddress, commandParser))
{
    serverListener.Run();

    ServerLogger.Instance.LogInfo("Server is running;");
    Console.Read();
}

async static Task<IPAddress> SelectIpAddressAsync()
            {
                var hostEntryInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());

                if (hostEntryInfo == null)
                {
                    throw new ArgumentNullException("Host Entry could not be found.");
                }

                var ipv4Addresses = hostEntryInfo.AddressList.Where(adr => adr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();

                ServerLogger.Instance.LogInfo("Available IPv4 addresses:");
                for (int i = 0; i < ipv4Addresses.Length; i++)
                {
                    ServerLogger.Instance.LogInfo($"    {i}. {ipv4Addresses[i]}");
                }

                int ipAddressIndex = 0;
                do
                {
                    ServerLogger.Instance.LogInfo($"Select index (0-{ipv4Addresses.Length - 1});");
                    ipAddressIndex = Console.ReadKey(true).KeyChar - 48;
                } while (ipAddressIndex < 0 || ipAddressIndex >= ipv4Addresses.Length);

                var ipAddress = ipv4Addresses[ipAddressIndex];

                ServerLogger.Instance.LogInfo($"Server is running on {ipAddress};");

                return ipAddress;
            
        


        
    }


