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
        public static void StackPanelAnim(StackPanel stackPanel, double? from, double? to, double time)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = from;
            anim.To = to;
            anim.Duration = TimeSpan.FromSeconds(time);

            stackPanel.BeginAnimation(StackPanel.WidthProperty, anim);
        }

        public static async Task<string> TextAnimation(string word, int speed, Button next, TextBlock says)
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

        public async static Task<bool> GridOpacityAnim(Grid grid, double from, double to)
        {
            DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            anim.Duration = TimeSpan.FromSeconds(1);
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            grid.BeginAnimation(Grid.OpacityProperty, anim);

            await Task.Delay(1000);

            return true;
        }

        public async static Task<bool> ImgOpacityAnim(System.Windows.Controls.Image img, double from, double to)
        {
            DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            anim.Duration = TimeSpan.FromSeconds(1);
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            img.BeginAnimation(Grid.OpacityProperty, anim);

            await Task.Delay(1000);

            return true;
        }

        public async static Task<bool> MediaOpacityAnim(MediaElement media, double from, double to)
        {
            DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            anim.Duration = TimeSpan.FromSeconds(1);
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            media.BeginAnimation(MediaElement.OpacityProperty, anim);

            await Task.Delay(1000);

            return true;
        }

        public async static Task<bool> LWOpacityAnim(ListView grid, double from, double to)
        {
            DoubleAnimationUsingKeyFrames anim = new DoubleAnimationUsingKeyFrames();
            anim.Duration = TimeSpan.FromSeconds(1);
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            anim.KeyFrames.Add(new EasingDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            grid.BeginAnimation(ListView.OpacityProperty, anim);

            await Task.Delay(1000);
            return true;
        }

        public async static Task<bool> Appear(StackPanel panel, Button next, TextBlock who, TextBlock says, string person, string words, 
            System.Windows.Controls.Image img)
        {
            StackPanelAnim(panel, panel.ActualWidth, 700, 0.3);
            await Task.Delay(300);
            if (img != null)
            {  
                img.Opacity = 1;
            }
            await Medias.TextBlockNovel(person, Animations.TextAnimation(words, 50, next, says), who, says);
            return true;
        }

        public async static Task<bool> Disappear(StackPanel panel, Button next, TextBlock who, TextBlock says, System.Windows.Controls.Image img)
        {
            if (img != null)
            {
                img.Opacity = 0;
            }
            await Medias.TextBlockNovel(string.Empty, Animations.TextAnimation(string.Empty, 0, next, says), who, says);
            StackPanelAnim(panel, panel.ActualWidth, 0, 0.3);
            await Task.Delay(300);
            return true;
        }
    }
}
