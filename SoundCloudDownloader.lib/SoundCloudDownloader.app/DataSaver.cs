using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using SoundCloudDownloader.lib;

namespace SoundCloudDownloader.app
{
    public class DataSaver
    {
        private List<TrackInformation> _soundList;
        public DataSaver()
        {
            _soundList = new List<TrackInformation>();
        }
        public void Save()
        {
            if (_soundList == null)
                return;
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, _soundList);
                }
            }
            catch (IOException i)
            {
                throw;
            }
        }
        public void SaveTrack(TrackInformation sound)
        {
            _soundList.Add(sound);
            this.Save();
        }

        public List<TrackInformation> Load()
        {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    _soundList = (List<TrackInformation>)bin.Deserialize(stream);
                
                }
            }
            catch (IOException)
            {
            }

            return _soundList;
        }
    }
}
