using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace AsyncWpfApp
{
    /// <summary>
    /// Interaction logic for Demo01SynchronFreeze.xaml
    /// </summary>
    public partial class Demo01SynchronFreeze : Window
    {
        public Demo01SynchronFreeze()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WebClient client = new WebClient();
            statusTextBlock.Text = "Start download";
            byte[] data = client.DownloadData(@"http://www.microsoft.com/");
            statusTextBlock.Text = "Done "  + data.Length;
        }
    }
}
