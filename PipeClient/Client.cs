using System;
using System.IO;
using System.IO.Pipes;

namespace PipeClient
{
    class Client
    {
        private string message = string.Empty;
        public void Run(string pipeName)
        {
            using (NamedPipeClientStream client = new NamedPipeClientStream(pipeName))
            {
                Console.WriteLine("Forsøger at forbinde..");
                client.Connect();
                Console.WriteLine("\nForbundet");
                using (StreamWriter writer = new StreamWriter(client))
                {
                    writer.AutoFlush = true;
                    Console.WriteLine("*** Tast EXIT når du vil stoppe ***");
                    do
                    {
                        Console.WriteLine("Indtast landekode og vekselkurs (f.eks.: DKK 100) :");
                        message = Console.ReadLine();
                        writer.WriteLine(message);
                        Console.WriteLine($"\n{message} sendt til server");
                    } while (message.ToUpper() != "EXIT");
                }
            }
        }
    }
}