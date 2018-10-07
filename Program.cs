using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using restafile.Models;
namespace restafile
{
    public class Program
    {
        
      
        public static void Main(string[] args)
        {
            //To create a project like this;
            //https://www.youtube.com/watch?v=QpjkiQ5qYtw
            //This project created by:  dotnet new api -n restafile 
            //To build: restafile>dotnet build
            //To run: restafile>dotnet run path=/home/goode_k/Dev/restafile/testfiles
            bool error = true;
            if (args.Length == 1 ) {
                string[] parsed =args[0].Split("=");
                if (parsed.Length==2 && parsed[0].ToLower()=="path")
                { 
                    ServerConfig.getConfig().filePath=parsed[1];
                    error = false;
                }
                Console.Write("INFO: Reading files from path " + ServerConfig.getConfig().filePath + "\n");
            }
            if(error){
                Console.Write("FATAL ERROR: Expected single parameter specifying file root directory path IE path=XXXX\n");
                return;
            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
