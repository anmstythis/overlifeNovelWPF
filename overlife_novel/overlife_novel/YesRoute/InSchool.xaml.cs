using overlife_novel.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
    /// Логика взаимодействия для InSchool.xaml
    /// </summary>
    public partial class InSchool : Page
    {
        static string player = (string)App.Current.Resources["playerName"];

        List<string> wordsRi = new List<string>() {"Я увидела на табло список всех классов.",
        "Я тихо просидела на уроках. Я ничего не поняла из того, что рассказывали, но, к счастью, никто меня не спрашивал.",
        "Однако, я ощутила то, что девочка с розовыми волосами явно меня недолюбливает.", "Как же скучно все-таки в школе.", "Я пошла домой."};

        string[,] arrayClass = { {"1 - A",  "A101" }, { "2 - A", "A201" }, { "3 - A", "A301" }, { "1 - B", "B101" }, { "2 - B", "B201" }, { "3 - B", "B301" },
        { "1 - C", "C101" }, { "2 - C", "C201" }, { "3 - C", "C301" }, { "1 - D", "D101" }, { "2 - D", "D201" }, { "3 - D", "D301" },
        { "1 - E", "E101" }, { "2 - E", "E201" }, { "3 - E", "E301" }};

        List<string> selectedClasses = new List<string>();

        int tries;
        bool isHinted = false;
        public InSchool()
        {
            InitializeComponent();
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            int index = wordsRi.IndexOf(says.Text);
            if (index > 0 && index < wordsRi.Count - 1)
            {
                if (index == 1)
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
            if (says.Text == wordsRi[0])
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
            else if (says.Text.Contains("в кабинет"))
            {
                await Animations.Disappear(panel, next, who, says, null);
                await Animations.LWOpacityAnim(classTable, classTable.Opacity, 0);

                mediaBack.Source = new Uri("../Images/cabinet.png", UriKind.RelativeOrAbsolute);
                await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 1);

                await Animations.Appear(panel, next, who, says, "???", "Вы кто?", null);

                if (tries > 5)
                {
                    classTable.SelectedIndex = 3;
                }
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
            else if (who.Text == "Учитель") //попала в тот класс
            {
                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[1], 30, next, says), who, says);
            }
            else if (who.Text == "Учитель?") //попала не в тот класс
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
            else if (index == wordsRi.Count - 1)
            {
                Medias.ChosenOption(this, "/Final/Invitation.xaml", "Приглашение в Корпорацию Нео", panel, who, says, mediaBack, sprite);
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
                    if (!isHinted)
                    {
                        await Medias.TextBlockNovel("???", Animations.TextAnimation($"{player}, Вы из класса {arrayClass[3, 0]}. Вам нужно идти в кабинет {arrayClass[3, 1]}.", 50, next, says), who, says);
                        isHinted = true;
                    }
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
            await Animations.Disappear(panel, next, who, says, null);
            await Animations.MediaOpacityAnim(mediaBack, mediaBack.Opacity, 0);
            await Animations.Appear(panel, next, who, says, player, wordsRi[0], null);
        }
    }
}
