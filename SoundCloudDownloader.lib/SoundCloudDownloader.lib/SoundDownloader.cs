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

        private string _trackTitle;
        private string _downloadLink;

        public SoundDownloader(string downloadUrl)
        {
            _webClient = new WebClient();
            _url = downloadUrl;

            _downloadLink = SoundCloud.GetTrackDownloadLink(_url);
            string trackName = SoundCloud.GetTrack(_url)["title"].ToString();

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                trackName = trackName.Replace(c, '_');
            }
            _trackTitle = trackName;

        }

        public SoundDownloader(string downloadUrl, bool IsDownLoadLink, string soundTitle)
        {
            _webClient = new WebClient();
            _downloadLink = downloadUrl;
            string trackName = soundTitle;

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                trackName = trackName.Replace(c, '_');
            }
            _trackTitle = trackName;

        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string TrackTitle
        {
            get { return _trackTitle; }
        }

        public string DownloadLink
        {
            get { return _downloadLink; }
        }

        public void StartDownload(string folderPath)
        {
            
            _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);
            try
            {
                _webClient.DownloadFile(_downloadLink, folderPath + _trackTitle + ".mp3");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Completed();
            }
        }

        public void StartDownload(string filePath, bool customFileName)
        {
            string url = SoundCloud.GetTrackDownloadLink(_url);
            _webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadCompleted);
            try
            {
                _webClient.DownloadFile(url, filePath);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Completed();
            }
        }

        private void Completed()
        {
            if (OnCompleted != null)
            {
                OnCompleted(this);
                _completed = true;
            }
        }

        private void OnDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Completed");
        }

    }
}
