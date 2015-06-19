using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundCloudDownloader.lib;
using Newtonsoft.Json.Linq;

namespace SoundCloudDownloader.console
{
    class Program
    {
        static void Main(string[] args)
        {
            Downloader d = new Downloader();
            User user = Downloader.GetUser("valentinchatelain");
            JArray data = Downloader.GetFavorites(user.Id);

            foreach (JObject j in data)
            {
                Console.WriteLine(j["title"].ToString());
            }

            Console.ReadKey();
        }
    }
}
