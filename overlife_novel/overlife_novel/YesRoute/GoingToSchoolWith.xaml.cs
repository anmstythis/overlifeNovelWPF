using overlife_novel.Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
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
using static System.Net.Mime.MediaTypeNames;

namespace overlife_novel.YesRoute
{
    /// <summary>
    /// Логика взаимодействия для GoingToSchoolWith.xaml
    /// </summary>
    public partial class GoingToSchoolWith : Page
    {
        static string player = (string)App.Current.Resources["playerName"];

        List<string> wordsRi = new List<string>() { "(Я отправилась в школу вместе с этой женщиной.)", 
            "(Стоп, а что такое школа? Куда я иду?)", "(Если я у неё это спрошу, то может это будет весьма странно.)", 
            "(Ну да, это весьма странно, что я ни с того ни с сего потеряла память…)", "Я добралась до школы в целости сохранности (это же школа, да?).", "(А куда она идёт?)",
            "Эм... А... а ты куда идёшь?", "(Я уже не помню, что происходит обычно… Эх…)", 
            "(А на вид школа вроде ничего. На свой страх и риск войду во врата школы с потерянной памятью…)"};

        List<string> wordsSaiko = new List<string>() { $"Пока, {player}! Удачи тебе в школе!", "На работу, как обычно.", "Давай, иди в школу, а то опоздаешь." };
        public GoingToSchoolWith()
        {
            InitializeComponent();
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            int index = wordsRi.IndexOf(says.Text);

            if (index < 3 && index >= 0)
            {
                sprite.Source = null;
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[index + 1], 50, next, says), who, says); 
            }
            else if (index == 3)
            {
                await Animations.Disappear(panel, next, who, says, null);
                mediaBack.Source = new Uri("../Images/школа.png", UriKind.RelativeOrAbsolute);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 1);
                await Animations.Appear(panel, next, who, says, player, wordsRi[index + 1], sprite);

            }
            else if (index == 4)
            {
                sound.Stop();
                sprite.Source = new BitmapImage(new Uri("../Sprites/saiko outside 2.PNG", UriKind.RelativeOrAbsolute));
                await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSaiko[0], 50, next, says), who, says);
            }
            else if (says.Text == wordsSaiko[0])
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[5], 50, next, says), who, says);

                var result = MessageBox.Show("Мне стоит спросить это у неё?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[6], 100, next, says), who, says);
                }
                else
                {
                    sprite.Source = null;
                    await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[8], 50, next, says), who, says);
                }
            }
            else if (says.Text == wordsRi[6])
            {
                sprite.Source = new BitmapImage(new Uri("../Sprites/saiko outside.PNG", UriKind.RelativeOrAbsolute));
                await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSaiko[1], 50, next, says), who, says);
            }
            else if (says.Text == wordsSaiko[1])
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[7], 50, next, says), who, says);
            }
            else if (says.Text == wordsRi[7])
            {
                await Medias.TextBlockNovel("???", Animations.TextAnimation(wordsSaiko[2], 50, next, says), who, says);

                var result = MessageBox.Show("Попрощаться с ней?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation("Хорошо, уже иду! Пока!", 50, next, says), who, says);
                }
                else
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation("...", 200, next, says), who, says);
                }
            }
            else if (says.Text == "Хорошо, уже иду! Пока!" || says.Text == "...")
            {
                sprite.Source = null;
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[8], 50, next, says), who, says);
            }
            else if (says.Text == wordsRi[8])
            {
                Medias.ChosenOption(this, "/YesRoute/InSchool.xaml", "Школа", panel, who, says, mediaBack, sprite);
            }
        }

        private async void page_Loaded(object sender, RoutedEventArgs e)
        {
            sound.Source = new Uri("../Soundtracks/steps ground.mp3", UriKind.RelativeOrAbsolute);
            sound.Play();
            await Animations.Appear(panel, next, who, says, player, wordsRi[0], null);
        }
    }
}
