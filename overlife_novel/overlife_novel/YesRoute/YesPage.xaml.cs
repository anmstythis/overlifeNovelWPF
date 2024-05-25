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
using System.Xml.Linq;

namespace overlife_novel.YesRoute
{
    /// <summary>
    /// Логика взаимодействия для YesPage.xaml
    /// </summary>
    public partial class YesPage : Page
    {
        static string player = (string)App.Current.Resources["playerName"];
        List<string> wordsRi = new List<string> { "(Видимо, я с ней живу, но я почему-то не помню её.)", "Я позавтракала." };
        List<string> wordsSaiko = new List<string> { $"О, {player}, ты проснулась! Доброе утро!", "Завтрак готов, кстати.", "Приятного аппетита!", $"{player}, мне тебя провести до школы или ты пойдёшь сама?" };
        Uri intro = new Uri("../Images/opening eyes.mp4", UriKind.RelativeOrAbsolute);

        public YesPage()
        {
            InitializeComponent();
        }

        private void yesChoice_Loaded(object sender, RoutedEventArgs e)
        {
            mediaBack.Source = intro;
            mediaBack.Play();
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            if (says.Text == wordsSaiko[0])
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[0], 50, next, says), who, says);
            }
            else if (says.Text == wordsRi[0])
            {
                await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSaiko[1], 50, next, says), who, says);
            }
            else if (says.Text == wordsSaiko[1])
            {
                await Animations.Disappear(panel, next, who, says, sprite);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 0);

                mediaBack.Source = new Uri("../Images/омлет.png", UriKind.RelativeOrAbsolute);

                sprite.Source = new BitmapImage(new Uri("../Sprites/saiko open.PNG", UriKind.RelativeOrAbsolute));

                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 1);
                await Animations.Appear(panel, next, who, says, "???", wordsSaiko[2], sprite);

                var result = MessageBox.Show("Мне стоит ей что-нибудь говорить?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation("СПАСИБО!", 50, next, says), who, says);
                }
                else
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation("...", 100, next, says), who, says);
                }
            }
            else if (says.Text == "СПАСИБО!" || says.Text == "...")
            {
                await Animations.Disappear(panel, next, who, says, sprite);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 0);
                await Animations.Appear(panel, next, who, says, player, wordsRi[1], null);
            }
            else if (says.Text == wordsRi[1])
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/saiko outside 2.PNG", UriKind.RelativeOrAbsolute));

                await Animations.Disappear(panel, next, who, says, null);
                mediaBack.Source = new Uri("../Images/日本.png", UriKind.RelativeOrAbsolute);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 1);
                await Animations.Appear(panel, next, who, says, "???", wordsSaiko[3], sprite);

                Medias.OptionWindow("Да. Проведи меня, пожалуйста! 😟\nНет. Я сама пойду.", "Что ответить?", this, "/YesRoute/GoingToSchoolWith.xaml", 
                    "/YesRoute/GoingToSchoolAlone.xaml", panel, who, says, mediaBack, sprite);
            }
            
        }

        private async void mediaBack_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (mediaBack.Source == intro)
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/saiko close.PNG", UriKind.RelativeOrAbsolute));
                await Animations.Appear(panel, next, who, says, "???", wordsSaiko[0], sprite);
            }
        }
    }
}
