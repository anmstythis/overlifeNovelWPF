using overlife_novel.Elements;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace overlife_novel.Final
{
    /// <summary>
    /// Логика взаимодействия для Invitation.xaml
    /// </summary>
    public partial class Invitation : Page
    {
        static string player = (string)App.Current.Resources["playerName"];
        public Invitation()
        {
            InitializeComponent();
            notification.Source = new Uri("../Soundtracks/notif.mp3", UriKind.Relative);
            notification.Play();
        }

        private async void notification_MediaEnded(object sender, RoutedEventArgs e)
        {
            await Animations.Appear(panel, next, who, says, player, "Пришло уведомление...", null);
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            if (says.Text.Contains("уведомление") && inviteTxt.Text == string.Empty)
            {
                next.Visibility = Visibility.Hidden;
                accept.Visibility = Visibility.Visible;
                decline.Visibility = Visibility.Visible;
                
                await Animations.TextAnimation($"Здравствуйте, {player}! Мы приглашаем вас принять участие в интерактивной игре. Это необычная игра. " +
                    $"Принимая в ней участие вы нам поможете провести исследование. Нам это очень важно. " +
                    $"Таким образом, вы, как и мы, сможете внести вклад в развитие человечества. Мы будем очень благодарны вам! \n\n\t\t\t\t\tКорпорация Нео.", 25, next, inviteTxt);
                await Animations.Disappear(panel, next, who, says, null);

                accept.IsEnabled = true;
                decline.IsEnabled = true;
            }
        }

        private async void accept_Click(object sender, RoutedEventArgs e)
        {
            await Animations.GridOpacityAnim(grid, grid.Opacity, 0);
            Medias.ChosenOption(this, "/Final/AcceptPage.xaml", "Поход в Корпорацию Нео", panel, who, says, null, null);
        }

        private async void decline_Click(object sender, RoutedEventArgs e)
        {
            await Animations.GridOpacityAnim(grid, grid.Opacity, 0);
            Medias.ChosenOption(this, "/Final/DeclinePage.xaml", "Отклонение приглашения", panel, who, says, null, null);
        }
    }
}
