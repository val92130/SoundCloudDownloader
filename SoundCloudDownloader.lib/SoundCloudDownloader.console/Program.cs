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


            Console.WriteLine("Choose the number of threads (max 8)");
  
            bool valid = false;
            int a;
            do
            {            
                if (int.TryParse(Console.ReadLine(), out a))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Enter a valid number");
                }

            } while (!valid);

            StartThreads(a <= 8 && a > 0 ? a : 4, favorites, dir );

            Console.ReadKey();
        }

        private static void StartThreads(int threadsNbr, List<SoundDownloader> soundList, string folderPath )
        {
            List<SoundDownloader>[] soundDownloaders = new List<SoundDownloader>[threadsNbr];

            for (int i = 0; i < soundDownloaders.Length; i++)
            {
                soundDownloaders[i] = new List<SoundDownloader>();
            }

            int nbr = threadsNbr;
            int listCount = soundList.Count / soundDownloaders.Length;
            for (int i = 0; i < soundDownloaders.Length; i++)
            {
                for (int j = listCount * i; j < listCount * i + listCount; j++)
                {
                    soundDownloaders[i].Add(soundList[j]);
                }
            }

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < soundDownloaders.Length; i++)
            {
                var i1 = i;
                Thread t = new Thread(() =>
                {
                    DownloadQueue queue = new DownloadQueue(folderPath, soundDownloaders[i1]);
                    queue.StartDownload();
                });
                threads.Add(t);
            }

            foreach (Thread t in threads)
            {
                t.Start();
            }
        }

        private static void OnCompleted(object sender)
        {
            SoundDownloader s = (SoundDownloader)sender;
            Console.WriteLine("Download complete : " + s.Url);
        }

    }
}
