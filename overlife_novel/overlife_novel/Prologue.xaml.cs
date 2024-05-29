using overlife_novel.Elements;
using overlife_novel.YesRoute;
using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JsonLibrary;
using System.IO;
using System.Globalization;

namespace overlife_novel
{
    /// <summary>
    /// Логика взаимодействия для Prologue.xaml
    /// </summary>
    public partial class Prologue : Page
    {
        bool notNameless;
        bool isCalled;
        string player;
        List<string> wordsRi = new List<string> { "Где я?... Кто я?...", "Как меня зовут?", "... Вот как меня зовут...", "?... Кто это меня зовёт?" };
        public Prologue()
        {
            InitializeComponent();
            Animations.GridOpacityAnim(prol, prol.Opacity, 1);
        }

        private async void next_Click(object sender, RoutedEventArgs e)
        {
            int index = wordsRi.IndexOf(says.Text);
            if (index != wordsRi.Count - 1)
            {
                if (index == 0)
                {
                    says.Text = await Animations.TextAnimation(wordsRi[index + 1], 50, next, says);
                    Animations.StackPanelAnim(stack, stack.ActualWidth, 500);
                }

                else if (index == 2)
                {
                    await Medias.TextBlockNovel("???", Animations.TextAnimation($"{player}!", 100, next, says), who, says);
                    isCalled = true;
                }

                else if (isCalled)
                {
                    await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[3], 50, next, says), who, says);
                    Medias.OptionWindow("Да. Лучше отозваться.\nНет. Я думаю, не стоит идти...", "Идти на зов?", this, "/YesRoute/YesPage.xaml",
                        "/NoRoute/NoPage.xaml", panel, who, says, null, null, "Пробуждение", "Сон?");
                }
            }
        }

        void MessageOutput(string name)
        {
            var result = MessageBox.Show($"Ваше имя {name}. Вы согласны с этим?", "?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                player = name;

                Animations.StackPanelAnim(stack, stack.ActualWidth, 0);

                notNameless = true;
            }
            else
            {
                notNameless = false;
            }
        }

        private async void saveName_Click(object sender, RoutedEventArgs e)
        {
            if (yourName.Text != string.Empty)
            {
                MessageOutput(yourName.Text);
            }
            else
            {
                MessageOutput("Рицу");
            }

            if (notNameless)
            {
                App.Current.Resources["playerName"] = player;
                wordsRi[2] = player + wordsRi[2];

                await Medias.TextBlockNovel(player, Animations.TextAnimation(wordsRi[2], 100, next, says), who, says);
            }
        }

        private async void prologue_Loaded(object sender, RoutedEventArgs e)
        {
            await Animations.Appear(panel, next, who, says, "???", wordsRi[0], null);
        }
    }
}
