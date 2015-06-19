using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SoundCloudDownloader.lib;
using Newtonsoft.Json.Linq;

namespace SoundCloudDownloader.console
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Thread t = new Thread(() =>
            {
                SoundDownloader d = new SoundDownloader("https://soundcloud.com/pisextra/justice-stress");
                d.OnCompleted += new SoundDownloader.OnCompletedEventHandler(OnCompleted);
                d.StartDownload(@"C:\Users\val\Music\");
            });

            t.Start();

            Console.ReadKey();
        }

        private static void OnCompleted(object sender)
        {
            SoundDownloader s = (SoundDownloader) sender;
            Console.WriteLine("Download complete : " + s.Url);
        }
    }
}
