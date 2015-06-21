using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SoundCloudDownloader.lib;
using DataGrid = System.Windows.Controls.DataGrid;
using MessageBox = System.Windows.MessageBox;

namespace SoundCloudDownloader.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SoundDownloader> _favoritesList;
        private List<TrackInformation> _tracksInfo; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void downloadTrackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Util.ValidUser(urlTextBox.Text.ToString()))
            {
                _favoritesList = SoundCloud.GetAllFavorites(urlTextBox.Text.ToString());
                if (songsGrid.IsLoaded)
                {
                    _tracksInfo = GetTrackInformations(_favoritesList);
                    songsGrid.ItemsSource = _tracksInfo;
                }
                //SoundCloud.DownloadTrack(urlTextBox.Text.ToString(), str);
            }
            else
            {
                MessageBox.Show("Invalid username");
            }

        }

//  SaveFileDialog saveFileDialog = new SaveFileDialog();
//                saveFileDialog.Filter = "Mp3 file (*.mp3)|*.mp3";
//                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
//                if (saveFileDialog.ShowDialog() == true)
//{

//}

    private List<TrackInformation> GetTrackInformations(List<SoundDownloader> soundList)
        {
            List<TrackInformation> trackList = new List<TrackInformation>();

            foreach (SoundDownloader s in soundList)
            {
                s.OnCompleted += new SoundDownloader.OnCompletedEventHandler(OnCompletedDownload);
                trackList.Add(s.TrackInformation);
            }

            return trackList;

        }

    private void OnCompletedDownload(object sender)
    {
        SoundDownloader sound = sender as SoundDownloader;
        this.Dispatcher.Invoke(() =>
        {
            songsGrid.Items.Refresh();
            songsGrid.ItemsSource = _tracksInfo;
        });


    }

        private void songsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var items = new List<TrackInformation>();            

            var grid = sender as DataGrid;
            grid.ItemsSource = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog saveFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            //saveFileDialog.Filter = "Mp3 file (*.mp3)|*.mp3";
            saveFileDialog.RootFolder = Environment.SpecialFolder.MyMusic;
            DialogResult d = saveFileDialog.ShowDialog();

            if (d == System.Windows.Forms.DialogResult.OK)
            {
                Thread t = new Thread(() =>
                {
                    DownloadQueue queue = new DownloadQueue(saveFileDialog.SelectedPath, _favoritesList);
                    queue.StartDownload();
                });
                t.Start();
            }

        }
    }
}
