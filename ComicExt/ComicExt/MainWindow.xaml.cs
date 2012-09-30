using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using ICSharpCode.SharpZipLib.Zip;
using Schematrix;
using System.IO;
using ICSharpCode;

namespace ComicExt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Unrar unrar;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if (Directory.Exists(textBoxFileName.Text))
                {
                    //buscamos los cbz
                    List<String> cbzFiles =
                        new List<String>(Directory.GetFiles(textBoxFileName.Text, "*.cbz", SearchOption.AllDirectories));

                    UnZip(cbzFiles);


                    //start a new thread to unzip it
                    //Thread th = new Thread(new ThreadStart(UnZip));
                    //th.Start();
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void UnZip(List<string> cbzlist)
        {
            foreach (var fileName in cbzlist)
            {
                toolStripStatusLabel1.Content = "UnZipping...";
                var newfolder = fileName.Substring(0, fileName.Length - 4);
                if (Directory.Exists(newfolder)) 
                    Directory.Delete(newfolder, true); 
                System.IO.Directory.CreateDirectory(newfolder);
                ZipHelp.UnZip(fileName,"", newfolder);
                List<String> picFiles = new List<String>(Directory.GetFiles(newfolder, "*.jpg", SearchOption.AllDirectories));
                picFiles.AddRange(new List<String>(Directory.GetFiles(newfolder, "*.png", SearchOption.AllDirectories)));
                int c = 0;
                foreach (var picFile in picFiles)
                {
                    switch (Path.GetExtension(picFile))
                    {
                        case ".jpg":
                            System.IO.File.Move(picFile, newfolder + @"\" + c + ".jpg");
                            break;
                        case ".png":
                            System.IO.File.Move(picFile, newfolder + @"\" + c + ".png");
                            break;
                    }
                    c++;
                }

                toolStripStatusLabel1.Content = "Done";
            }
        }

        private void cbzazip_Click(object sender, RoutedEventArgs e)
        {
            List<String> cbzFiles = new List<String>(Directory.GetFiles(textBoxFileName.Text, "*.cbz", SearchOption.AllDirectories));
            foreach (var cbzFile in cbzFiles)
            {
                System.IO.File.Move(cbzFile, cbzFile.Substring(0, cbzFile.Length - 4) + ".zip");
            }
        }

        private void zipacbz_Click(object sender, RoutedEventArgs e)
        {
            List<String> cbzFiles = new List<String>(Directory.GetFiles(textBoxFileName.Text, "*.zip", SearchOption.AllDirectories));
            foreach (var cbzFile in cbzFiles)
            {
                System.IO.File.Move(cbzFile, cbzFile.Substring(0, cbzFile.Length - 4) + ".cbz");
            }
        }
        
    }
}
