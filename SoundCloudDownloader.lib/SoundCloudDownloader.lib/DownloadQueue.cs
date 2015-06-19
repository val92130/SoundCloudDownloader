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

        public DownloadQueue()
        {
            
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
                
            }
        }
    }
}
