﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudDownloader.lib
{
    [Serializable]
    public class DownloadQueue
    {
        private Queue<SoundDownloader> _downloadQueue;
        private string _folderPath;
        private bool _finished = false;
        private bool _paused = false;

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

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            if (_paused)
            {
                _paused = false;
                StartDownload();
            }
        }

        public bool Finished
        {
            get { return _finished; }
        }

        public void Add(SoundDownloader Sound)
        {
            lock (_downloadQueue)
            {
                _downloadQueue.Enqueue(Sound);
            }
            
        }

        public void Get()
        {
            lock (_downloadQueue)
            {
                _downloadQueue.Dequeue();
            }         
        }

        public bool IsPaused
        {
            get
            {
                return _paused;
            }
        }

        public void StartDownload()
        {
            if (_downloadQueue.Count != 0)
            {
                _finished = false;

                if (_paused)
                {
                    return;
                }

                SoundDownloader currentDl;
                lock (_downloadQueue)
                {
                    currentDl = _downloadQueue.Dequeue();
                }

                if (currentDl == null)
                    return;

                if (currentDl.IsCompleted)
                {
                    StartDownload();
                    return;
                }
                     
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
                _finished = true;
                Console.WriteLine("Queue finished");
            }
        }

        private void DownloadComplete(object sender)
        {
            SoundDownloader s = (SoundDownloader) sender;
            Console.WriteLine("Download completed : " + s.TrackTitle);
            if (_downloadQueue.Count != 0)
            {
                StartDownload();
            }
            
        }
    }
}
