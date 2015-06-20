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

            JObject j2 = SoundCloud.GetTrack("https://soundcloud.com/");
            Assert.That(j2 == null );
        }

        [Test]
        public void ValidTrackWorksCorrectly()
        {
            string trackValid = "https://soundcloud.com/whatsonot/what-so-not-touched";
            string trackInvalid = "google.fr/test";

            Assert.That(Util.ValidTrackLink(trackValid));
            Assert.That(!Util.ValidTrackLink(trackInvalid));
        }
    }
}
