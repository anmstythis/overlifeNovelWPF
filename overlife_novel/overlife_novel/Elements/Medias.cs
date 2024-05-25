using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace overlife_novel.Elements
{
    internal class Medias
    {
        public static async Task<bool> TextBlockNovel(string person, Task<string> words, TextBlock who, TextBlock says)
        {
            who.Text = person;
            says.Text = await words;
            return true;
        }

        public static async void ChosenOption(DependencyObject obj, string link,
            StackPanel stack, TextBlock who, TextBlock says, MediaElement media,
            Image img)
        {
            await Animations.Disappear(stack, null, who, says, img);
            if (media != null)
            {
                await Animations.MediaOpacityAnim(media, media.Opacity, 0);
            }
            if (link != null)
            {
                NavigationService.GetNavigationService(obj).Navigate(new Uri(link, UriKind.RelativeOrAbsolute));
            }
        }

        public static void OptionWindow(string text, string caption, DependencyObject obj, string linkFirst,
            string linkSecond, StackPanel stack, TextBlock who, TextBlock says, MediaElement media, Image img)
        {
            var result = MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ChosenOption(obj, linkFirst, stack, who, says, media, img);
            }
            else
            {
                ChosenOption(obj, linkSecond, stack, who, says, media, img);
            }
        }
    }
}
