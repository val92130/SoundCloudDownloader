using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SoundCloudDownloader.lib
{
    public class Util
    {
        public static bool ValidTrackLink(string url)
        {
            Regex r = new Regex(@"https{0,1}:\/\/w{0,3}\.*soundcloud\.com\/([A-Za-z0-9_-]+)\/([A-Za-z0-9_-]+)[^< ]*");
            return r.IsMatch(url);
        }
    }
}
