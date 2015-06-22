using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SoundCloudDownloader.lib;

namespace SoundCloudDownloader.app
{
    /// <summary>
    /// Interaction logic for MultipleTrackDownloaderWindow.xaml
    /// </summary>
    public partial class MultipleTrackDownloaderWindow : Window
    {
        private MainWindow _mainWindow;
        public MultipleTrackDownloaderWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }

        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            string text = new TextRange(textBoxLinks.Document.ContentStart, textBoxLinks.Document.ContentEnd).Text; 
            string[] tracks = text.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
            List<string> validTracks = new List<string>();

            foreach (string s in tracks)
            {
                if (Util.ValidTrackLink(s))
                {
                    validTracks.Add(s);
                }
            }

            MessageBox.Show("Found " + validTracks.Count + " tracks, downloading now ! Songs will be saved at : " +
                            _mainWindow.SelectedPath);

            foreach (string t in validTracks)
            {
                SoundCloud.DownloadTrack(t, _mainWindow.SelectedPath);
            }

            MessageBox.Show("Done ! ");
        }
    }
}
