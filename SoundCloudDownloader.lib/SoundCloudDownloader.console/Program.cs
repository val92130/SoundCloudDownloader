using System;
using System.Collections.Generic;
using System.IO;
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
            
            Console.WriteLine("Enter your soundcloud username");
            string user = Console.ReadLine();
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) +
                                      @"\soundcloudDonwloads\");

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) +
                         @"\soundcloudDonwloads\";
            Console.WriteLine("Songs will be saved at  : " + dir);

            List<SoundDownloader> _favorites = SoundCloud.GetAllFavorites(user);
            //Console.WriteLine(SoundCloud.GetFavoritesOffset(0,6128633).Count);

            DownloadQueue _queue = new DownloadQueue(dir, _favorites);
            _queue.StartDownload();
            Console.ReadKey();
        }

        private static void OnCompleted(object sender)
        {
            SoundDownloader s = (SoundDownloader) sender;
            Console.WriteLine("Download complete : " + s.Url);
        }
    }
}
