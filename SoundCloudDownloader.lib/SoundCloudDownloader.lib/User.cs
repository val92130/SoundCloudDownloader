using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundCloudDownloader.lib
{
    public class User
    {
        private int _id;
        private string _kind, _permalink, _username, _uri, _permalink_url, _avatar_url, _country, _first_name;
        private string _last_name, _full_name, _description, _city, _website, _website_title;
        private bool _online;
        private int _track_count, _playlist_count;
        private string _plan;
        private int _favorites_count, _following_count;

        public User(int id, string kind, string permalink, string username, string uri, string permalink_url,
            string avatar_url, string country, string first_name, string last_name, string full_name, string description,
            string city, string website, string website_title, bool online, int track_count, int playlist_count, string plan, int favorites_count, int following_count
            )
        {
            this._id = id;
            this._kind = kind;
            this._permalink = permalink;
            this._username = username;
            this._uri = uri;
            this._permalink_url = permalink_url;
            this._avatar_url = avatar_url;
            this._country = country;
            this._first_name = first_name;
            this._last_name = last_name;
            this._full_name = full_name;
            this._description = description;
            this._city = city;
            this._website = website;
            this._website_title = website_title;
            this._online = online;
            this._track_count = track_count;
            this._playlist_count = playlist_count;
            this._plan = plan;
            this._favorites_count = favorites_count;
            this._following_count = following_count;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        public string Permalink
        {
            get { return _permalink; }
            set { _permalink = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Uri1
        {
            get { return _uri; }
            set { _uri = value; }
        }

        public string PermalinkUrl
        {
            get { return _permalink_url; }
            set { _permalink_url = value; }
        }

        public string AvatarUrl
        {
            get { return _avatar_url; }
            set { _avatar_url = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string FirstName
        {
            get { return _first_name; }
            set { _first_name = value; }
        }

        public string LastName
        {
            get { return _last_name; }
            set { _last_name = value; }
        }

        public string FullName
        {
            get { return _full_name; }
            set { _full_name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string Website
        {
            get { return _website; }
            set { _website = value; }
        }

        public string WebsiteTitle
        {
            get { return _website_title; }
            set { _website_title = value; }
        }

        public bool Online
        {
            get { return _online; }
            set { _online = value; }
        }

        public int TrackCount
        {
            get { return _track_count; }
            set { _track_count = value; }
        }

        public int PlaylistCount
        {
            get { return _playlist_count; }
            set { _playlist_count = value; }
        }

        public string Plan
        {
            get { return _plan; }
            set { _plan = value; }
        }

        public int FavoritesCount
        {
            get { return _favorites_count; }
            set { _favorites_count = value; }
        }

        public int FollowingCount
        {
            get { return _following_count; }
            set { _following_count = value; }
        }
    }
}
