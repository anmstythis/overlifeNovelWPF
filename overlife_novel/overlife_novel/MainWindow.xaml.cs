using overlife_novel.Final;
using overlife_novel.YesRoute;
using System.Text;
using System.Windows;
using JsonLibrary;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using overlife_novel.Elements;

namespace overlife_novel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Players player = new Players();
        public static string path = "C:\\Users\\My\\OneDrive\\Документы\\конспекты\\ОАиП C#\\визуальная новелла\\overlife_novel\\overlife_novel\\saved players.json";
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