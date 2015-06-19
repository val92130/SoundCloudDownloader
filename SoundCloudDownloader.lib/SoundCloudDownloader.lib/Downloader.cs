using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace SoundCloudDownloader.lib
{
    public class Downloader
    {
        
        private WebClient _webClient;
        private AutoResetEvent _waiter;
        private string _url;

        public Downloader(string downloadUrl)
        {
            _webClient = new WebClient();
            _url = downloadUrl;
        }

        public void StartDownload(string filePath)
        {         
            string url = SoundCloud.GetTrack(_url)["stream_url"].ToString();

        }

        private void OnDownloadCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            
            Console.WriteLine(e.Result.ToString());
        }



    }
}
