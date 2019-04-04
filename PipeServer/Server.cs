using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;

namespace PipeServer
{
    class Server
    {
        private StreamReader pipeReader;
        private bool running = true;
        private string input = string.Empty;
        private Dictionary<string, Currency> currencies = new Dictionary<string, Currency>();

        public void Run(string pipeName)
        {
            Console.WriteLine("Venter på klient...");
            using (NamedPipeServerStream server = new NamedPipeServerStream(pipeName))
            {
                //Wait for client to connect:
                server.WaitForConnection();
                Console.WriteLine(" - klient tilsluttet!");
                using (pipeReader = new StreamReader(server))
                {
                    while (running)
                    {
                        Console.WriteLine("Venter på input");
                        input = GetLineFromClient(pipeReader);
                        running = ManageInput(input);
                    }
                }
            }
        }

        private string GetLineFromClient(StreamReader reader)
        {
            string result = string.Empty;
            while (reader.Peek() >= 0)
            {
                result += reader.ReadLine();
            }
            reader.DiscardBufferedData();
            return result.ToUpper();
        }
        private bool ManageInput(string input)
        {
            if (input.Equals("EXIT"))
            {
                return false;
            }
            else
            {
                string[] split = input.Split(' ');
                Currency curr = new Currency(split[0], int.Parse(split[1]));
                currencies.Add(curr.CountryCode, curr);
                ShowCurrencies();
                return true;
            }
            //Console.WriteLine("\nInput var: " + input);
            //return !(input.ToUpper().Equals("EXIT"));
        }
        private void ShowCurrencies()
        {
            Console.WriteLine("\nValutakurser: ");
            foreach (Currency c in currencies.Values)
            {
                Console.WriteLine($"{c.CountryCode}, {c.ExchangeRate}");
            }
        }
    }
}