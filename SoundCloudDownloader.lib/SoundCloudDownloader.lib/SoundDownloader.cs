using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace SoundCloudDownloader.lib
{
    public class SoundDownloader
    {
        private WebClient _webClient;
        private AutoResetEvent _waiter;
        private string _url;
        public delegate void OnCompletedEventHandler(object sender);

        private bool _completed = false;
        public event OnCompletedEventHandler OnCompleted;

        public SoundDownloader(string downloadUrl)
        {
            _webClient = new WebClient();
            _url = downloadUrl;
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public void StartDownload(string folderPath)
        {
            string url = SoundCloud.GetTrackDownloadLink(_url);
            string trackName = SoundCloud.GetTrack(_url)["title"].ToString();

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                trackName = trackName.Replace(c, '_');
            }

            _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);
            _webClient.DownloadFile(url, folderPath + trackName + ".mp3");
            Completed();
        }

        public void StartDownload(string filePath, bool customFileName)
        {
            string url = SoundCloud.GetTrackDownloadLink(_url);
            _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);
            _webClient.DownloadFile(url, filePath);
            Completed();
        }

        private void Completed()
        {
            if (OnCompleted != null)
            {
                OnCompleted(this);
                _completed = true;
                Console.WriteLine("Completed ! ");
            }
        }

        private void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Completed");
        }

    }
}
