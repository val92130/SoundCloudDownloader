using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudDownloader.lib
{
    [Serializable]
    public class TrackInformation
    {
        private bool _downloaded;
        public TrackInformation(string artist, string title, double duration)
        {
            this.Artist = artist;
            this.Title = title;
            this.Duration = duration;
        }

        public string Artist { get; set; }
        public string Title { get; set; }
        public double Duration { get; set; }

        public bool Downloaded
        {
            get { return _downloaded; }
            set { _downloaded = value; }
        }
    }
}
