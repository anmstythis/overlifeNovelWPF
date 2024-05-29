using JsonLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace overlife_novel.Elements
{
    internal class Medias
    {
        public static async Task<bool> TextBlockNovel(string person, Task<string> words, TextBlock who, TextBlock says) //вывод текста. регуляция панельки
        {
            who.Text = person;
            says.Text = await words;
            return true;
        }

        public static async void ChosenOption(DependencyObject obj, string link, string nameRoute,
            StackPanel stack, TextBlock who, TextBlock says, MediaElement media,
            Image img) //переход на новую страницу
        {
            await Animations.Disappear(stack, null, who, says, img);
            if (media != null) //если есть медиа элемент на фоне
            {
                await Animations.MediaOpacityAnim(media, media.Opacity, 0);
            }
            if (link != null) //если ссылка передана
            {
                MainWindow.player.State = link;
                MainWindow.player.StateName = nameRoute;

                List<Players> playerList = new List<Players>() { MainWindow.player };
                if (!File.Exists(MainWindow.path)) //если файла нет
                {
                    JsonSave.jsonSerialize(playerList, MainWindow.path);
                }
                else
                {
                    if (link != "/YesRoute/YesPage.xaml" && link != "/NoRoute/NoPage.xaml") //если игрок не на прологе
                    {
                        if (App.Current.Resources["playerID"] is null) //если начата новая игра
                        {
                            JsonSave.updateObject(-1, MainWindow.player, MainWindow.path); //-1 значит, что индекс брать оттуда не надо
                        }
                        else //если загружена уже начатая игра
                        {
                            int index = (int)App.Current.Resources["playerID"];
                            MainWindow.player.Name = (string)App.Current.Resources["playerName"];
                            JsonSave.updateObject(index, MainWindow.player, MainWindow.path);
                        }
                    }
                    else
                    {
                        Players jsonfile_new = new Players(MainWindow.player.Name, MainWindow.player.State, MainWindow.player.StateName); //если игрок на прологе
                        JsonSave.appendObject(jsonfile_new, MainWindow.path);
                    }
                }

                NavigationService.GetNavigationService(obj).Navigate(new Uri(link, UriKind.RelativeOrAbsolute));

            }
        }

        public static void OptionWindow(string text, string caption, DependencyObject obj, string linkFirst,
            string linkSecond, StackPanel stack, TextBlock who, TextBlock says, MediaElement media, Image img, string state1, string state2)
        {
            var result = MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            MainWindow.player.Name = (string)App.Current.Resources["playerName"]; //имя игрока
            
            if (result == MessageBoxResult.Yes)
            {
                ChosenOption(obj, linkFirst, state1, stack, who, says, media, img);
            }
            else
            {
                ChosenOption(obj, linkSecond, state2, stack, who, says, media, img);
            }
        }
    }
}
