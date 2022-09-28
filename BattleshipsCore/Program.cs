using BattleshipsCore.Game;
using BattleshipsCore.Server;
using System.Diagnostics;
using System.Net;

Console.Title = "Server";

var serverProcessName = Process.GetCurrentProcess().ProcessName;

if (Process.GetProcesses().Count(p => p.ProcessName == serverProcessName) > 1)
{
    Console.WriteLine($"Server is already running under process name '{serverProcessName}';");

    return;
}

var selectedIpAddress = await SelectIpAddressAsync();
var commandParser = new GameMessageParser();

Console.WriteLine("Starting server;");
using (var serverListener = new AsyncSocketServer(selectedIpAddress, commandParser))
{
    serverListener.Run();

    Console.WriteLine("Server is running;");
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

    Console.WriteLine("Available IPv4 addresses:");
    for (int i = 0; i < ipv4Addresses.Length; i++)
    {
        Console.WriteLine($"    {i}. {ipv4Addresses[i]}");
    }

    int ipAddressIndex = 0;
    do
    {
        Console.Write("Select index: ");
        ipAddressIndex = (int)(Console.ReadKey().KeyChar) - 48;
        Console.WriteLine();
    } while (ipAddressIndex < 0 || ipAddressIndex >= ipv4Addresses.Length);

    return ipv4Addresses[ipAddressIndex];
}