using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using SoundCloudDownloader.lib;

namespace SoundCloudDownloader.app
{
    /// <summary>
    /// Interaction logic for MultipleTrackDownloaderWindow.xaml
    /// </summary>
    public partial class PlaylistDownloaderWindow : Window
    {
        private MainWindow _mainWindow;
        public PlaylistDownloaderWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            string text = playlistTextBox.Text;

            JArray tracks = SoundCloud.GetPlaylist(text);
            if (tracks == null)
            {
                MessageBox.Show("Error, incorrect url");
                return;
            }

            List<SoundDownloader> soundList = SoundCloud.GetDownloadList(tracks);
            SoundDownloader lastElem = soundList[soundList.Count - 1];
            lastElem.OnCompleted += OnFinishedDownload;
            MessageBox.Show("Download starting");
            Thread thread = new Thread(() =>
            {
                foreach (SoundDownloader s in soundList)
                {
                    s.StartDownload(_mainWindow.SelectedPath);
                }
            });
            thread.Start();
        }

        private void OnFinishedDownload(object sender)
        {
            MessageBox.Show("Download complete");
        }
    }
}
