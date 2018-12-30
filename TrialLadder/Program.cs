using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace TrialLadder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* string filepath = Directory.GetCurrentDirectory();
             DirectoryInfo d = new DirectoryInfo(filepath);

             foreach (var file in d.GetFiles("*.Challenge.Gbx"))
             {
                 Directory.Move(file.FullName, filepath + "\\TextFiles\\" + file.Name);
             }
             Map m = new Map();*/

            List<Map> maps = new List<Map>();
            string path = Path.Combine(Directory.GetCurrentDirectory() + "/maps/maps.txt");
            var lines = File.ReadLines(path);
            using (WebClient wc = new WebClient())
            {
                foreach (string uid in lines)
                {
                    string tmxinfo = wc.DownloadString("http://tmnforever.tm-exchange.com/apiget.aspx?action=apitrackinfo&uid=" + uid );
                    Map m = new Map();
                    m.uid = uid; 
                    m.name = tmxinfo.Split("\t")[1];
                    m.author = tmxinfo.Split("\t")[3];
                    maps.Add(m);
                }
            }
            ;

        CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
