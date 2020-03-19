using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

class MyTcpListener
{
    public static void Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any, 13000);
        server.Start();
        while (true)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("Жду подключения...");
                using TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Подключение состоялось!");

                using Stream stream = client.GetStream();
                using var br = new BinaryReader(stream);
                string data = br.ReadString();
                Console.WriteLine("Получено: {0}", data);
                data = ProcessCommand(data);
                using var bw = new BinaryWriter(stream);
                bw.Write(data);
                Console.WriteLine("Отправлено: {0}", data);
            }
            catch
            { }
        }
    }
    public static string ProcessCommand(string data)
    {
        return data.ToUpper();
    }
}

