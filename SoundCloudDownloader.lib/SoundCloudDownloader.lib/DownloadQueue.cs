using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudDownloader.lib
{
    public class DownloadQueue
    {
        private Queue<SoundDownloader> _downloadQueue;
        private string _folderPath;

        public DownloadQueue(string folderPath)
        {
            _downloadQueue = new Queue<SoundDownloader>();
            _folderPath = folderPath;
        }

        public DownloadQueue(string folderPath, List<SoundDownloader> downloadList)
        {
            _downloadQueue = new Queue<SoundDownloader>();
            _folderPath = folderPath;
            foreach (SoundDownloader s in downloadList)
            {
                _downloadQueue.Enqueue(s);
            }
        }

        public void Add(SoundDownloader Sound)
        {
            _downloadQueue.Enqueue(Sound);
        }

        public void Get()
        {
            _downloadQueue.Dequeue();
        }

        public void StartDownload()
        {
            if (_downloadQueue.Count != 0)
            {
                SoundDownloader currentDl = _downloadQueue.Dequeue();
                Console.WriteLine("Downloading : " + currentDl.TrackTitle);
                currentDl.OnCompleted += new SoundDownloader.OnCompletedEventHandler(DownloadComplete);
                try
                {
                    currentDl.StartDownload(_folderPath);
                }
                catch (Exception e)
                {
                    
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Queue finished");
            }
        }

        private void DownloadComplete(object sender)
        {
            SoundDownloader s = (SoundDownloader) sender;
            Console.WriteLine("Download completed : " + s.TrackTitle);
            StartDownload();
        }
    }
}
