using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SoundCloudDownloader.lib
{
    public class SoundCloud
    {
        private static string ClientId = "4dd97a35cf647de595b918944aa6915d";
        static WebClient WebClient = new WebClient();

        public static JObject GetTrack(string url)
        {
            JObject data =
               GetJson("http://api.soundcloud.com/resolve.json?url=" + url + "&client_id=" +
                       ClientId);

            return data;
        }

        public static string GetTrackDownloadLink(string url)
        {
            JObject data = GetTrack(url);
            return data["stream_url"].ToString() + "?client_id=" + ClientId;
        }

        public static void DownloadTrack(string url, string path)
        {
            JObject data = GetTrack(url);
            WebClient.DownloadFile(data["stream_url"].ToString() + "?client_id=" + ClientId, path);
        }

        public static JArray GetFavorites(int userId)
        {
            JArray data =
                GetJsonArray("http://api.soundcloud.com/users/" + userId + "/favorites/?client_id=" + ClientId + "&limit=200");

            return data;
        }

        public static JArray GetFavoritesOffset(int offset, int userId)
        {
            JArray data =
                GetJsonArray("http://api.soundcloud.com/users/" + userId + "/favorites/?client_id=" + ClientId + "&offset="+offset+"&limit=200");

            return data;
        }

        public static List<SoundDownloader> GetAllFavorites(string userName)
        {
            User user = GetUser(userName);
            int favoritesCount = user.FavoritesCount;
            int span = (int)Math.Ceiling((double) favoritesCount/(double) 200);
            List<SoundDownloader> _soundList = new List<SoundDownloader>();

            for (int i = 0; i < span; i++)
            {
                JArray data = GetFavoritesOffset(i*200, user.Id);
                foreach (JObject j in data)
                {
                    if (j["stream_url"] == null )
                    {
                        if (j["id"] == null)
                            continue;
                        _soundList.Add(new SoundDownloader("https://api.soundcloud.com/tracks/" + j["id"].ToString() + "/stream" + "?client_id=" + ClientId, true,
                            j["title"].ToString()));
                    }
                    else
                    {
                        _soundList.Add(new SoundDownloader(j["stream_url"].ToString() + "?client_id=" + ClientId, true, j["title"].ToString()));
                    }
                    
                }
            }
            return _soundList;
        }

        public static User GetUser(string userName)
        {
            JObject data =
                GetJson("http://api.soundcloud.com/resolve.json?url=http://soundcloud.com/" + userName + "&client_id=" +
                        ClientId);

            User user = new User(int.Parse(data["id"].ToString()), data["kind"].ToString(), data["permalink"].ToString(), data["username"].ToString(),
                data["uri"].ToString(), data["permalink_url"].ToString(), data["avatar_url"].ToString(), data["country"].ToString(), data["first_name"].ToString(),
                data["last_name"].ToString(), data["full_name"].ToString(), data["description"].ToString(), data["city"].ToString(), data["website"].ToString(),
                data["website_title"].ToString(), Boolean.Parse(data["online"].ToString()), int.Parse(data["track_count"].ToString()), int.Parse(data["playlist_count"].ToString()),
                data["plan"].ToString(), int.Parse(data["public_favorites_count"].ToString()), int.Parse(data["followings_count"].ToString()));
            return user;
        }

        public static JObject GetJson(string url)
        {
            var data = WebClient.DownloadString(url);
            return JObject.Parse(data);
        }

        public static JArray GetJsonArray(string url)
        {
            var data = WebClient.DownloadString(url);
            return JArray.Parse(data);
        }
    }
}
