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
            "(А на вид школа вроде ничего. На свой страх и риск войду во врата школы с потерянной памятью…)", "Я увидела на табло список всех классов.",
        "Я тихо просидела на уроках. Я ничего не поняла из того, что рассказывали, но, к счастью, никто меня не спрашивал.",
        "Однако, я ощутила то, что девочка с розовыми волосами явно меня недолюбливает.", "Как же скучно все-таки в школе.", "Я пошла домой."};

        List<string> wordsSaiko = new List<string>() { $"Пока, {player}! Удачи тебе в школе!", "На работу, как обычно.", "Давай, иди в школу, а то опоздаешь." };

        string[,] arrayClass = { {"1 - A",  "A101" }, { "2 - A", "A201" }, { "3 - A", "A301" }, { "1 - B", "B101" }, { "2 - B", "B201" }, { "3 - B", "B301" },
        { "1 - C", "C101" }, { "2 - C", "C201" }, { "3 - C", "C301" }, { "1 - D", "D101" }, { "2 - D", "D201" }, { "3 - D", "D301" },
        { "1 - E", "E101" }, { "2 - E", "E201" }, { "3 - E", "E301" }};

        List<string> selectedClasses = new List<string>();

        int tries;
        public GoingToSchoolWith()
        {
            InitializeComponent();
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            int index = wordsRi.IndexOf(says.Text);
            
            if (index < 3 && index >= 0 || index >= 10 && index < wordsRi.Count-1)
            {
                if (index == 10)
                {
                    sprite.Source = new BitmapImage(new Uri("../Sprites/tomoe.PNG", UriKind.RelativeOrAbsolute));
                    sprite.Opacity = 1;
                    await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[index + 1], 50, next, says), who, says);
                }
                else
                {
                    sprite.Source = null;
                    await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[index + 1], 50, next, says), who, says);
                }
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
                await Animations.Disappear(panel, next, who, says, null);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 0);
                await Animations.Appear(panel, next, who, says, player, wordsRi[9], null);
            }
            else if (says.Text == wordsRi[9])
            {
                next.IsEnabled = false;

                classTable.Visibility = Visibility.Visible;
                for (int i = 0; i < arrayClass.GetLength(0); i++)
                {
                    classTable.Items.Add(arrayClass[i, 0]);
                }
                await Animations.LWOpacityAnim(classTable, classTable.Opacity, 1);

                var isAppeared = await WhereIStudy();

                if (isAppeared)
                {
                    next.IsEnabled = true;
                }
            }
            else if (says.Text.Contains("Я пошла в кабинет") || says.Text.Contains("Вам нужно идти в кабинет"))
            {
                await Animations.Disappear(panel, next, who, says, null);
                await Animations.LWOpacityAnim(classTable, classTable.Opacity, 0);
        
                mediaBack.Source = new Uri("../Images/cabinet.png", UriKind.RelativeOrAbsolute);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 1);

                await Animations.Appear(panel, next, who, says, "???", "Вы кто?", null);
            }
            else if (says.Text == "Вы кто?")
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation($"... {player}...", 50, next, says), who, says);
            }
            else if (says.Text == $"... {player}...")
            {
                if (classTable.SelectedIndex == 3)
                {
                    await Medias.TextBlockNovel("Учитель", Animations.TextAnimation($"Входите, {player}. Впредь больше не опаздывайте.", 50, next, says), who, says);
                }
                else
                {
                    await Medias.TextBlockNovel("Учитель?", Animations.TextAnimation($"Хмм... В моем классе нет человека по имени {player}.", 50, next, says), who, says);
                }
            }
            else if (who.Text == "Учитель")
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[10], 30, next, says), who, says);
            }
            else if (who.Text == "Учитель?")
            {
                next.IsEnabled = false;
                await Animations.Disappear(panel, next, who, says, null);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 0);

                await Animations.LWOpacityAnim(classTable, classTable.Opacity, 1);
                var isAppeared = await WhereIStudy();

                if (isAppeared)
                {
                    next.IsEnabled = true;
                }
            }
            else if (index == wordsRi.Count-1)
            {
                Medias.ChosenOption(this, "/Final/Invitation.xaml", panel, who, says, mediaBack, sprite);
            }
        }

        private async Task<bool> WhereIStudy()
        {
            await Animations.Appear(panel, next, who, says, player, "А в каком классе я учусь?", null);
            classTable.IsEnabled = true;

            return true;
        }
        private async Task<bool> isFinished(string word)
        {
            await Animations.TextAnimation(word, 50, next, says);
            return true;
        }
        private async void classTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = classTable.SelectedIndex;
            bool finish = false;
            classTable.IsEnabled = false;

            if (!selectedClasses.Contains(classTable.SelectedItem.ToString()))
            {
                tries++;
                selectedClasses.Add(classTable.SelectedItem.ToString());
                if (tries <= 5) //будет дано ровно 5 попыток, чтобы угадать класс
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation($"Я пошла в кабинет {arrayClass[index, 1]}.", 50, next, says), who, says);
                }
                else
                {
                    await Medias.TextBlockNovel("???", Animations.TextAnimation($"{player}, Вы из класса {arrayClass[3, 0]}. Вам нужно идти в кабинет {arrayClass[3, 1]}.", 50, next, says), who, says);
                    classTable.SelectedIndex = 3;
                }
            }
            else
            {
                finish = await isFinished("Эм... Я уже была в этом кабинете...");
                if (finish)
                {
                    classTable.IsEnabled = true;
                }
                
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
