using overlife_novel.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для AcceptPage.xaml
    /// </summary>
    public partial class AcceptPage : Page
    {
        static string player = (string)App.Current.Resources["playerName"];
        List<string> wordsRi = new List<string>() { "Это звучит весьма интересно! Будет здорово внести вклад в развитие ЦЕЛОГО ЧЕЛОВЕЧЕСТВА!", 
            "Неужели это настолько влиятельная корпорация?", "Видимо, да. И это здорово!", "На следующей неделе меня пригласили в главное здание Корпорации Нео.",
            "Я пришла туда.", "Ого! А там прилично народу! Даже неловко как-то…", "(Все сразу затихли. Этот человек меня уже пугает…)", 
            "(Главный директор?)", "(Я так и знала, что он важная персона.)" };

        List<string> wordsSatoru = new List<string>() { "Тишина в зале!", "Всем здравствуйте! Вы все здесь, потому что приняли наше приглашение. Ваше согласие всё ещё актуально?",
            "Так как я за демократию, я отпускаю всех, кто ответил нет.", "Хорошо. Полагаю, мне следует представиться.",  
            "Моё имя - Сатору Кишо. Я главный директор Корпорации Нео.", "Я не желаю затягивать свою речь и утомлять вас, поэтому сразу перейду к сути.", "Идём за мной." };

        Uri neo = new Uri("../Images/neo corp.mp4", UriKind.RelativeOrAbsolute);

        public AcceptPage()
        {
            InitializeComponent();
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            int index = wordsRi.IndexOf(says.Text);
            int index2 = wordsSatoru.IndexOf(says.Text);

            if (index >= 0 && index < 3)
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[index + 1], 40, next, says), who, says);
            }
            else if (index == 3)
            {
                await Animations.Disappear(panel, next, who, says, null);
                mediaBack.Source = neo;
                mediaBack.Play();
            }
            else if (index == 4)
            {
                await Animations.Disappear(panel, next, who, says, sprite);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 0);

                mediaBack.Source = new Uri("../Images/neo enter.png", UriKind.RelativeOrAbsolute);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 1);

                sound.Source = new Uri("../Soundtracks/tolpa.mp3", UriKind.RelativeOrAbsolute);
                sound.Play();

                await Animations.Appear(panel, next, who, says, player, wordsRi[index + 1], sprite);
            }
            else if (index == 5)
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/makoto greeting.PNG", UriKind.RelativeOrAbsolute));
                await Medias.TextBlockNovel("???", Animations.TextAnimation("Привет!", 20, next, says), who, says);
                var result = MessageBox.Show("Мне здороваться с ним?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation("А... привет!", 40, next, says), who, says);
                }
                else
                {
                    sprite.Source = null;
                    await Medias.TextBlockNovel(player, Animations.TextAnimation("...", 40, next, says), who, says);
                }
            }
            else if (says.Text == "А... привет!")
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/makoto surprised.PNG", UriKind.RelativeOrAbsolute));
                await Medias.TextBlockNovel("???", Animations.TextAnimation("О, хоть кто-то не проигнорил меня. Ура!", 20, next, says), who, says);
            }
            else if (says.Text.Contains("Ура"))
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/makoto smiling.PNG", UriKind.RelativeOrAbsolute));
                await Medias.TextBlockNovel("???", Animations.TextAnimation("Тоже в предвкушении того, что будет дальше?", 20, next, says), who, says);
            }
            else if (says.Text == "..." || says.Text.Contains("что будет дальше"))
            {
                sprite.Source = null;
                await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSatoru[0], 40, next, says), who, says);
            }
            else if (says.Text == wordsSatoru[0])
            {
                sound.Stop();
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[6], 40, next, says), who, says);
            }
            else if (says.Text == wordsRi[6])
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/satoru smiling.PNG", UriKind.RelativeOrAbsolute));
                await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSatoru[1], 60, next, says), who, says);
                var result = MessageBox.Show("Согласие всё ещё актуально? Или стоит уйти пока не поздно...", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    sprite.Source = new BitmapImage(new Uri("../Sprites/satoru thinking.PNG", UriKind.RelativeOrAbsolute));
                    await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSatoru[2], 40, next, says), who, says);
                    sound.Source = new Uri("../Soundtracks/steps.mp3", UriKind.RelativeOrAbsolute);
                    sound.Play();
                }
                else
                {
                    Medias.ChosenOption(this, "/Final/DeclinePage.xaml", "Отказ от соглашения", panel, who, says, mediaBack, sprite);
                }
            }
            else if (says.Text == wordsSatoru[2])
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/satoru calm.PNG", UriKind.RelativeOrAbsolute));
                await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSatoru[3], 40, next, says), who, says);
            }
            else if (says.Text == wordsSatoru[3])
            {
                sound.Stop();
                await Medias.TextBlockNovel("Сатору Кишо", Animations.TextAnimation(wordsSatoru[4], 40, next, says), who, says);
            }
            else if (says.Text == wordsSatoru[4])
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[7], 40, next, says), who, says);
            }
            else if (says.Text == wordsRi[7])
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[8], 40, next, says), who, says);
            }
            else if (says.Text == wordsRi[8])
            {
                await Medias.TextBlockNovel("Сатору Кишо", Animations.TextAnimation(wordsSatoru[5], 40, next, says), who, says);
            }
            else if (index2 >= 5 && index2 < wordsSatoru.Count)
            {
                if (index2 == wordsSatoru.Count - 1)
                {
                    await Animations.GridOpacityAnim(grid, grid.Opacity, 0);
                    NavigationService.GetNavigationService(this).Navigate(new Uri("/Final/ToBeContinued.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    if (index2 == wordsSatoru.Count - 2)
                        sprite.Source = new BitmapImage(new Uri("../Sprites/satoru smiling.PNG", UriKind.RelativeOrAbsolute));

                    await Medias.TextBlockNovel("Сатору Кишо", Animations.TextAnimation(wordsSatoru[index2 + 1], 40, next, says), who, says);
                }
            }
        }

        private async void mediaBack_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (mediaBack.Source == neo)
            {
                await Animations.Appear(panel, next, who, says, player, wordsRi[4], sprite);
            }
        }

        private void sound_MediaEnded(object sender, RoutedEventArgs e)
        {
            sound.Play();
        }

        private async void page_Loaded(object sender, RoutedEventArgs e)
        {
            await Animations.Appear(panel, next, who, says, player, wordsRi[0], sprite);
        }
    }
}
