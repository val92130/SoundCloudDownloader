﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SoundCloudDownloader.lib
{
    public class SoundCloud
    {
        public static string ClientId = "4dd97a35cf647de595b918944aa6915d";
        static WebClient WebClient = new WebClient();

        public static JObject GetTrack(string url)
        {
            if (!Util.ValidTrackLink(url))
                return null;

            JObject data =
               GetJson("http://api.soundcloud.com/resolve.json?url=" + url + "&client_id=" +
                       ClientId);

            return data;
        }

        public static string GetTrackDownloadLink(string url)
        {
            JObject data = GetTrack(url);
            if (data == null || data["stream_url"] == null)
            {
                Console.WriteLine("Error in GetTrackDownloadLink, link was invalid");
                return String.Empty;
            }
            return data["stream_url"].ToString() + "?client_id=" + ClientId;
        }

        public static void DownloadTrack(string url, string folderPath)
        {
            JObject data = GetTrack(url);
            if (data == null)
            {
                return;
            }

            try
            {
                if (data["stream_url"] == null)
                {
                    if (data["id"] == null)
                        return;

                    WebClient.DownloadFile("https://api.soundcloud.com/tracks/" + data["id"].ToString() + "/stream" + "?client_id=" + ClientId, folderPath + @"\" + Util.ValidateString(data["title"].ToString()) + ".mp3");
                    return;
                }
                WebClient.DownloadFile(data["stream_url"].ToString() + "?client_id=" + ClientId, folderPath + @"\" + Util.ValidateString(data["title"].ToString()) + ".mp3");
            }
            catch (Exception e)
            {
                
            }
        }

        public static JArray GetFavorites(int userId)
        {
            JArray data =
                GetJsonArray("http://api.soundcloud.com/users/" + userId + "/favorites/?client_id=" + ClientId + "&limit=200");

            return data;
        }

        public static JArray GetPlaylist(string url)
        {
            JObject data =
                GetJson("http://api.soundcloud.com/resolve.json?url=" + url + "&client_id=" + ClientId);

            JArray t = JArray.Parse(data["tracks"].ToString());

            return t;
        }
        
         public static List<SoundDownloader> GetUserTracks(string username)
        {
            JArray data =
                GetJsonArray("http://api.soundcloud.com/users/" + username + "/tracks/?client_id=" + ClientId);

            List<SoundDownloader> sounds = new List<SoundDownloader>();
            if (data == null)
            {
                return null;
            }

            foreach (JObject j in data)
            {
                TrackInformation trackInfo = null;
                if (j["user"]["username"] != null && j["title"] != null && j["duration"] != null)
                {
                    double duration = Math.Round(double.Parse(j["duration"].ToString()) / 1000 / 60, 2);
                    trackInfo = new TrackInformation(j["user"]["username"].ToString(), j["title"].ToString(), duration);
                }

                if (j["stream_url"] == null)
                {
                    if (j["id"] == null)
                        continue;


                    sounds.Add(new SoundDownloader("https://api.soundcloud.com/tracks/" + j["id"].ToString() + "/stream" + "?client_id=" + ClientId, true,
                        trackInfo));
                }
                else
                {
                    sounds.Add(new SoundDownloader(j["stream_url"].ToString() + "?client_id=" + ClientId, true, trackInfo));
                }

            }
            return sounds;
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

            if (user == null)
            {
                Console.WriteLine("Wrong user, exception in GetAllFavorites");
                return null;
            }

            int favoritesCount = user.FavoritesCount;
            int span = (int)Math.Ceiling((double) favoritesCount/(double) 200);
            List<SoundDownloader> _soundList = new List<SoundDownloader>();

            for (int i = 0; i < span; i++)
            {
                JArray data = GetFavoritesOffset(i*200, user.Id);
                foreach (JObject j in data)
                {
                    TrackInformation trackInfo = null;
                    if (j["user"]["username"] != null && j["title"] != null && j["duration"] != null)
                    {
                        double duration = Math.Round(double.Parse(j["duration"].ToString()) / 1000/60, 2);
                        trackInfo = new TrackInformation(j["user"]["username"].ToString(), j["title"].ToString(), duration);
                    }

                    if (j["stream_url"] == null )
                    {
                        if (j["id"] == null)
                            continue;

                        
                        _soundList.Add(new SoundDownloader("https://api.soundcloud.com/tracks/" + j["id"].ToString() + "/stream" + "?client_id=" + ClientId, true,
                            trackInfo));
                    }
                    else
                    {
                        _soundList.Add(new SoundDownloader(j["stream_url"].ToString() + "?client_id=" + ClientId, true, trackInfo));
                    }
                    
                }
            }
            return _soundList;
        }

        public static List<SoundDownloader> GetDownloadList(JArray soundArray)
        {
            List<SoundDownloader> soundList = new List<SoundDownloader>();
            foreach (JObject j in soundArray)
                {
                    TrackInformation trackInfo = null;
                    if (j["user"]["username"] != null && j["title"] != null && j["duration"] != null)
                    {
                        double duration = Math.Round(double.Parse(j["duration"].ToString()) / 1000/60, 2);
                        trackInfo = new TrackInformation(j["user"]["username"].ToString(), j["title"].ToString(), duration);
                    }

                    if (j["stream_url"] == null )
                    {
                        if (j["id"] == null)
                            continue;

                        
                        soundList.Add(new SoundDownloader("https://api.soundcloud.com/tracks/" + j["id"].ToString() + "/stream" + "?client_id=" + ClientId, true,
                            trackInfo));
                    }
                    else
                    {
                        soundList.Add(new SoundDownloader(j["stream_url"].ToString() + "?client_id=" + ClientId, true, trackInfo));
                    }
                    
                }
            return soundList;
        }

        public static User GetUser(string userName)
        {
            JObject data =
                GetJson("http://api.soundcloud.com/resolve.json?url=http://soundcloud.com/" + userName + "&client_id=" +
                        ClientId);
            if (data == null)
            {
                Console.WriteLine("Wrong username");
                return null;
            }

            User user = new User(int.Parse(data["id"].ToString()), data["kind"].ToString(), data["permalink"].ToString(), data["username"].ToString(),
                data["uri"].ToString(), data["permalink_url"].ToString(), data["avatar_url"].ToString(), data["country"].ToString(), data["first_name"].ToString(),
                data["last_name"].ToString(), data["full_name"].ToString(), data["description"].ToString(), data["city"].ToString(), data["website"].ToString(),
                data["website_title"].ToString(), Boolean.Parse(data["online"].ToString()), int.Parse(data["track_count"].ToString()), int.Parse(data["playlist_count"].ToString()),
                data["plan"].ToString(), int.Parse(data["public_favorites_count"].ToString()), int.Parse(data["followings_count"].ToString()));
            return user;
        }

        public static JObject GetJson(string url)
        {
            try
            {
                var data = WebClient.DownloadString(url);
                return JObject.Parse(data);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                return null;
            }
        }

        public static JArray GetJsonArray(string url)
        {
            try
            {
                var data = WebClient.DownloadString(url);
                return JArray.Parse(data);
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                return null;
            }
        }
    }
}
