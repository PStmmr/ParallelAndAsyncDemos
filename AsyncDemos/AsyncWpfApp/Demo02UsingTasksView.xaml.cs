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
using AsyncDemoLib;

namespace AsyncWpfApp
{
    /// <summary>
    /// Interaction logic for Demo02UsingTasksView.xaml
    /// </summary>
    public partial class Demo02UsingTasksView : Window
    {
        public Demo02UsingTasksView()
        {
            InitializeComponent();
        }

        private void OnDownloadTapButtonClicked(object sender, RoutedEventArgs e)
        {
            this.statusTextBlock.Text = "Start downloading ...";

            MyDownloaderTap myDownloader = new MyDownloaderTap();
            Task<byte[]> downloadTask = myDownloader.DownloadOnePageAsync(new Uri(@"http://www.tgw-group.com"));
            downloadTask.ContinueWith(aTask =>
            {
                statusTextBlock.Text = "COMPLETED!";
                if (aTask.Exception == null)
                {
                    statusTextBlock.Text += " \n " + aTask.Result.Length +
                                            " bytes downloaded";
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void OnDownloadAwaitButtonClicked(object sender, RoutedEventArgs e)
        {
            this.statusTextBlock.Text = "Start downloading ...";

            MyDownloaderTap myDownloader = new MyDownloaderTap();
            byte[] data = await myDownloader.DownloadOnePageAsync(new Uri(@"http://www.tgw-group.com"));
            statusTextBlock.Text = data.Length + " bytes downloaded";
        }

    }
}
