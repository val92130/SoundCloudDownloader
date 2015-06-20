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

            string user = "";
            do
            {
                Console.WriteLine("Enter a soundcloud username");
                user = Console.ReadLine();
            } while (!Util.ValidUser(user));

            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) +
                         @"\soundcloudDownloads\"+Util.ValidateString(user)+@"\";

            Directory.CreateDirectory(dir);
            Console.WriteLine("Songs will be saved at  : " + dir);

            Console.WriteLine("Fetching favorites...");
            List<SoundDownloader> favorites = SoundCloud.GetAllFavorites(user);
            Console.WriteLine("Favorites fetching done : "+ favorites.Count + " tracks found");

            List<SoundDownloader>[] _soundDownloaders = new List<SoundDownloader>[]
            {
                new List<SoundDownloader>(),
                new List<SoundDownloader>(),
                new List<SoundDownloader>(),
                new List<SoundDownloader>()
            };

            int listCount = favorites.Count/4;
            for (int i = 0; i < 4; i++)
            {
                for (int j = listCount*i; j < listCount*i + listCount; j++)
                {
                    _soundDownloaders[i].Add(favorites[j]);
                }
            }

            DownloadQueue q1, q2, q3, q4;

            Thread t1 = new Thread(() =>
            {
                q1 = new DownloadQueue(dir,_soundDownloaders[0]);
                q1.StartDownload();
            });
            Thread t2 = new Thread(() =>
            {
                q2 = new DownloadQueue(dir, _soundDownloaders[1]);
                q2.StartDownload();
            });
            Thread t3 = new Thread(() =>
            {
                q3 = new DownloadQueue(dir, _soundDownloaders[2]);
                q3.StartDownload();
            });
            Thread t4 = new Thread(() =>
            {
                q4 = new DownloadQueue(dir, _soundDownloaders[3]);
                q4.StartDownload();
            });

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();


            //DownloadQueue queue = new DownloadQueue(dir, favorites);

            //queue.StartDownload();

            Console.ReadKey();
        }

        private static void OnCompleted(object sender)
        {
            SoundDownloader s = (SoundDownloader)sender;
            Console.WriteLine("Download complete : " + s.Url);
        }

    }
}
