using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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

namespace overlife_novel.Final
{
    /// <summary>
    /// Логика взаимодействия для ToBeContinued.xaml
    /// </summary>
    public partial class ToBeContinued : Page
    {
        public ToBeContinued()
        {
            InitializeComponent();
            media.Source = new Uri("../Images/to be continued rus.mp4", UriKind.Relative);
            media.Play();
        }

        private void media_MediaEnded(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
