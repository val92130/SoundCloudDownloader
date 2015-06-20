using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SoundCloudDownloader.lib;

namespace SoundCloudDownloader.app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void downloadTrackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Util.ValidUser(urlTextBox.Text.ToString()))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Mp3 file (*.mp3)|*.mp3";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                if (saveFileDialog.ShowDialog() == true)
                {
                    string str = saveFileDialog.FileName;
                    List<SoundDownloader> favorites = SoundCloud.GetAllFavorites(urlTextBox.Text.ToString());
                    //SoundCloud.DownloadTrack(urlTextBox.Text.ToString(), str);
                }
            }
            else
            {
                MessageBox.Show("Invalid username");
            }

        }
    }
}
