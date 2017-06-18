using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DependencyResolver;
using Ninject;
using BLL.Interfaces.Services;
using ILogger;
using System.Collections.Generic;

namespace ConsolePL
{
    class Program
    {
        public static readonly IKernel resolver;
        public static readonly ILotService lotService;
        public static readonly ILog log;
        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolverConsole();
            lotService = resolver.Get<ILotService>();
            log = resolver.Get<ILog>();
        }

        static void Main(string[] args)
        {         
            bool isActive = true;
            Console.WriteLine ("Auction");
            Console.WriteLine ("Command list: ");
            Console.WriteLine ("-all: shows all the lots");
            Console.WriteLine ("-bids {n}: shows bids on {n} lot");
            Console.WriteLine ("-exit: exit from application");
            Console.WriteLine("///////////////////////");
            List<string> commands = new List<string> { "-all", "-bids ", "-exit" };
            while (isActive)
            {
                var command = Console.ReadLine();
                switch (commands.FirstOrDefault(command.Contains))
                {
                    case "-all":
                        ShowLots();
                        break;
                    case "-bids ":
                        ShowBids(command);
                        break;
                    case "-exit":
                        isActive = false;
                        break;
                    default:
                        Console.WriteLine("Wrong command");
                        break;
                }
                Console.WriteLine("///////////////////////");
            }
            
            Console.WriteLine("Press \"Enter\"");
            Console.ReadLine();
        }

        private static void ShowBids(string command)
        {
            try 
            {
                log.Trace("ShowBids");
                string[] cmd = command.Split(' ');
                if (cmd.Length > 1)
                {
                    int id = Int32.Parse(cmd[1]);
                    var lot = lotService.GetById(id);
                    if (lot != null)
                    {
                        foreach (var bid in lot.Bids)
                        {
                            Console.WriteLine("N: {0}, Date: {1}, Price: {2}",
                                bid.Id,
                                bid.Date.ToString("dd.MM.yyyy"), bid.Price);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong id");
                        return;
                    }
                }
            }
            catch (FormatException fe)
            {
                log.Error("Error (ShowBids): Format is wrong: "+ fe.ToString());
                Console.WriteLine("Wrong id");
            }
            catch (SystemException se)
            {
                log.Error("Error (ShowBids): Operation with service is wrong: " + se.ToString());
            }
            
        }

        private static void ShowLots()
        {
            try
            {
                log.Trace("ShowLots");
                var list = lotService.GetAllByPredicate(lot => lot.IsChecked == true).ToList();
                if (list != null)
                {
                    foreach (var lot in list)
                    {
                        Console.WriteLine("N: {0}, Name: {1}, Start Date: {2}, End date: {3}, Description: {4}",
                            lot.Id,
                            lot.Name,
                            lot.StartDate.ToString("dd.MM.yyyy"),
                            lot.EndDate.ToString("dd.MM.yyyy"),
                            lot.Description);
                    }
                }    
            }
            catch (SystemException se)
            {
                log.Error("Error (ShowLots): Operation with service is wrong: " + se.ToString());
            }                   
        }
    }
}
