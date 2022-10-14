using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class MyMonitor
{
    public static string getIP() /* get static ip adress of the server */
    {
        var myhost = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ipaddr in myhost.AddressList)
        {
            if (ipaddr.AddressFamily == AddressFamily.InterNetwork)
            {
                return ipaddr.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address was found");
    }
    public static void SendData(string ip, int _port) /* Sending data over tcp listener */
    {
        TcpListener server = null;
        IPAddress localAddr = IPAddress.Parse(ip);
        server = new TcpListener(localAddr, _port);
        server.Start(); /**/
        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("connected");
        NetworkStream stream = client.GetStream();
        Console.WriteLine("--------------------------Start Monitoring-----------------------------------");
        Console.WriteLine("");
        Console.WriteLine("Cases are :");
        Console.WriteLine("Press 01 : Open entry");
        Console.WriteLine("Press 02 : Open exit");
        Console.WriteLine("Press 03 : Always open entry");
        Console.WriteLine("Press 04 : Always open exit");
        Console.WriteLine("Press 05 : Close entry");
        Console.WriteLine("Press 06 : Close exit");
        Console.WriteLine("Press 07 : Lock door");
        Console.WriteLine("Press 08 : Unlock door");
        Console.WriteLine("Press 09 : External alarm");
        Console.WriteLine("Press 10 : Cancel external alarm");
        while (true)
        {
            DateTime dt = DateTime.Now;
            Console.WriteLine("");
            Console.WriteLine(" Send event :");
            string enter = Console.ReadLine();
            switch (enter)
            {
                case ("01"):
                    Console.WriteLine("01 : Open Entry" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] openForEntry = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x29 }; 
                    stream.Write(openForEntry, 0, openForEntry.Length); /* sending openForEntry data */
                    break;
                case ("02"):
                    Console.WriteLine("02 : Open Exit" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] openForExit = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2C };
                    stream.Write(openForExit, 0, openForExit.Length); /* sending OpenForExit data */
                    break;
                case ("03"):
                    Console.WriteLine("03 : Always Open Entry" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] alwaysOpenforEntry = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2A };
                    stream.Write(alwaysOpenforEntry, 0, alwaysOpenforEntry.Length); /* sending alwaysOpenForEntry data */
                    break;
                case ("04"):
                    Console.WriteLine("04 : Always Open Exit" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] alwaysOpenforExit = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2D };
                    stream.Write(alwaysOpenforExit, 0, alwaysOpenforExit.Length); /* sending alwaysOpenForExit data */
                    break;
                case ("05"):
                    Console.WriteLine("05 : Close Entry" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] closeForEntry = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2B };
                    stream.Write(closeForEntry, 0, closeForEntry.Length); /* sending closeForEntry data */
                    break;
                case ("06"):
                    Console.WriteLine("06 : Close Exit" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] closeForExit = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x00, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2E };
                    stream.Write(closeForExit, 0, closeForExit.Length); /* sending closeForExit data */
                    break;
                case ("07"):
                    Console.WriteLine("07 : Lock door" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] lockDoor = { 0xAA, 0x00, 0x01, 0x02, 0x05, 0x1E, 0x02, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x29 };
                    stream.Write(lockDoor, 0, lockDoor.Length); /* sending closeForExit data */
                    break;
                case ("08"):
                    Console.WriteLine("08 : Unlock door" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] unlockDoor = { 0xAA, 0x00, 0x01, 0x02, 0x05, 0x1E, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x28 };
                    stream.Write(unlockDoor, 0, unlockDoor.Length); /* sending unlockDoor data */
                    break;
                case ("09"):
                    Console.WriteLine("09 : External Alarm" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] externalAlarmOn = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2C };
                    stream.Write(externalAlarmOn, 0, externalAlarmOn.Length); /* sending externalAlarmOn data */
                    break;
                case ("10"):
                    Console.WriteLine("10 : Cancel External Alarm" + dt.ToString(" dd/MM/yyyy HH:mm tt"));
                    byte[] externalAlarmOff = { 0xAA, 0x00, 0x01, 0x02, 0x00, 0x1E, 0x08, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2B };
                    stream.Write(externalAlarmOff, 0, externalAlarmOff.Length); /* sending externalAlarmOff data */
                    break;
                default:
                    Console.WriteLine("");
                    Console.WriteLine("Cases are :");
                    Console.WriteLine("Press 01 : Open Entry");
                    Console.WriteLine("Press 02 : Open Exit");
                    Console.WriteLine("Press 03 : Always Open Entry");
                    Console.WriteLine("Press 04 : Always Open Exit");
                    Console.WriteLine("Press 05 : Close Entry");
                    Console.WriteLine("Press 06 : Close Exit");
                    Console.WriteLine("Press 07 : Lock oor");
                    Console.WriteLine("Press 08 : Unlock door");
                    Console.WriteLine("Press 09 : External Alarm");
                    Console.WriteLine("Press 10 : Cancel External Alarm");
                    Console.WriteLine("");
                    break;
            }
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("--------------------------Connect First-----------------------------------");
        Console.WriteLine("Enter port number :");
        string Port = Console.ReadLine();
        while (Port != "13000") /* RS485 to tcp ip converter configured Port= 13000 */
        {
            Console.WriteLine("--------------------------Connect First-----------------------------------");
            Console.WriteLine("Enter port number :");
            Port = Console.ReadLine();
        }
        int _port = int.Parse(Port);
        var _ip = getIP();
        SendData(_ip, _port);
        Console.ReadKey(true);
    }
}