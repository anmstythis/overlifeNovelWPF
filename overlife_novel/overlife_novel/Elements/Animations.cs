using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace overlife_novel.Elements
{
    internal class Animations
    {
        public static void StackPanelAnim(StackPanel stackPanel, double from, double to) //анимация панельки
        {
            var animation = AnimProperty(from, to, 0.3);
            stackPanel.BeginAnimation(StackPanel.WidthProperty, animation);
        }

        public static async Task<string> TextAnimation(string word, int speed, Button next, TextBlock says) //анимация текста
        {
            if (next != null)
            {
                next.IsEnabled = false;
            }
            says.Text = string.Empty;

            for (int i = 0; i < word.Length; i++)
            {
                says.Text += word[i];
                await Task.Delay(speed);
            }
            if (next != null)
            {
                next.IsEnabled = true;
            }
            return says.Text;
        }

        public async static Task<bool> GridOpacityAnim(Grid grid, double from, double to) //прозрачность грида
        {
            var animation = AnimProperty(from, to, 1);
            grid.BeginAnimation(Grid.OpacityProperty, animation);

            await Task.Delay(1000);

            return true;
        }

        private static DoubleAnimationUsingKeyFrames AnimProperty(double from, double to, double time) //анимация
        {
            DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            anim.Duration = TimeSpan.FromSeconds(1);
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time - time))));
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time))));

            return anim;
        }
        public async static Task<bool> ImgOpacityAnim(System.Windows.Controls.Image img, double from, double to)
        {
            var animation = AnimProperty(from, to, 1);
            img.BeginAnimation(Grid.OpacityProperty, animation);

            await Task.Delay(1000);

            return true;
        }

        public async static Task<bool> MediaOpacityAnim(MediaElement media, double from, double to) //прозрачность медиа файла
        {
            var animation = AnimProperty(from, to, 1);
            media.BeginAnimation(MediaElement.OpacityProperty, animation);

            await Task.Delay(1000);

            return true;
        }

        public async static Task<bool> LWOpacityAnim(ListView grid, double from, double to) //прозрачность табло
        {
            var animation = AnimProperty(from, to, 1);
            grid.BeginAnimation(ListView.OpacityProperty, animation);

            await Task.Delay(1000);
            return true;
        }

        public async static Task<bool> Appear(StackPanel panel, Button next, TextBlock who, TextBlock says, string person, string words, 
            System.Windows.Controls.Image img) //появление панельки
        {
            StackPanelAnim(panel, panel.ActualWidth, 700); //панелька
            await Task.Delay(300);
            if (img != null) //если есть спрайт
            {  
                img.Opacity = 1;
            }
            await Medias.TextBlockNovel(person, Animations.TextAnimation(words, 50, next, says), who, says); //вывод текста
            return true;
        }

        public async static Task<bool> Disappear(StackPanel panel, Button next, TextBlock who, TextBlock says, System.Windows.Controls.Image img) //исчезновение панельки
        {
            if (img != null) //если есть спрайт
            {
                img.Opacity = 0;
            }
            await Medias.TextBlockNovel(string.Empty, Animations.TextAnimation(string.Empty, 0, next, says), who, says); //текст исчезает
            StackPanelAnim(panel, panel.ActualWidth, 0); //панелька уменьшается в ширине
            await Task.Delay(300);
            return true;
        }
    }
}
