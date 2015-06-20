using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SoundCloudDownloader.lib
{
    public class Util
    {
        public static bool ValidTrackLink(string url)
        {
            Regex r = new Regex(@"https{0,1}:\/\/w{0,3}\.*soundcloud\.com\/([A-Za-z0-9_-]+)\/([A-Za-z0-9_-]+)[^< ]*");
            return r.IsMatch(url);
        }

        public static bool ValidUser(string username)
        {
            JObject data =
                SoundCloud.GetJson("http://api.soundcloud.com/resolve.json?url=http://soundcloud.com/" + username + "&client_id=" +
                       SoundCloud.ClientId);
            return data != null;
        }

        public static string ValidateString(string s)
        {
            string str = s;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                str = str.Replace(c, '_');
            }

            return str;
        }

    }
}
