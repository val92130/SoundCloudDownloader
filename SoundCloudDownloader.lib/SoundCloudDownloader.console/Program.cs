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
            Downloader d = new Downloader("https://soundcloud.com/aspenlawler/lethargic");
            Downloader d1 = new Downloader("https://soundcloud.com/aspenlawler/lethargic");
            Downloader d2 = new Downloader("https://soundcloud.com/djcarnageofficial/carnage-feat-timmy-trumpet-kshmr-toca-1");
            Downloader d3 = new Downloader("https://soundcloud.com/aspenlawler/lethargic");
            Downloader d4 = new Downloader("https://soundcloud.com/aspenlawler/lethargic");

            d2.StartDownload(@"C:\Users\val\Music\lelele.mp3");

            Console.ReadKey();

        }
    }
}
