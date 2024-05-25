using overlife_novel.Final;
using overlife_novel.YesRoute;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace overlife_novel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //pageFrame.Content = new Invitation();
            //pageFrame.Content = new AcceptPage();
            //pageFrame.Content = new GoingToSchoolWith();
            pageFrame.Content = new StartGame();
        }
    }
}