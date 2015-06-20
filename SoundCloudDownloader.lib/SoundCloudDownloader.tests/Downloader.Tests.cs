using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SoundCloudDownloader.lib;

namespace SoundCloudDownloader.tests
{
    [TestFixture]
    public class Downloader
    {
        [Test]
        public void GetTracksWorksCorrectly()
        {
            JObject j = SoundCloud.GetTrack("https://soundcloud.com/whatsonot/what-so-not-touched");
            Assert.That(j != null && j["title"] != null);
        }

        [Test]
        public void ValidTrackWorksCorrectly()
        {
            
        }
    }
}
