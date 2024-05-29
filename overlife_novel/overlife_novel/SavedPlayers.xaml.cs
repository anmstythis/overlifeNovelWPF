using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using JsonLibrary;
using overlife_novel.Elements;
using static System.Net.Mime.MediaTypeNames;

namespace overlife_novel
{
    /// <summary>
    /// Логика взаимодействия для SavedPlayers.xaml
    /// </summary>
    public partial class SavedPlayers : Page
    {
        ObservableCollection<PlayerData> data = new ObservableCollection<PlayerData>();
        List<Players> savedList;
        public SavedPlayers()
        {
            InitializeComponent();

            savedList = JsonSave.jsonDeserialize<List<Players>>(MainWindow.path);
            foreach (var item in savedList)
            {
                PlayerData player = new PlayerData();
                player.name.Text = item.Name;
                player.location.Text = item.StateName;
                data.Add(player);
            }
            playersList.ItemsSource = data;
        }

        private async void continue_Click(object sender, RoutedEventArgs e)
        {
            int index = playersList.SelectedIndex;
            var result = MessageBox.Show($"Вы продолжите игру как {savedList[index].Name}.", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Resources["playerID"] = index;
                App.Current.Resources["playerName"] = savedList[index].Name;
                await Animations.GridOpacityAnim(grid, grid.Opacity, 0);
                NavigationService.GetNavigationService(this).Navigate(new Uri(savedList[index].State, UriKind.RelativeOrAbsolute));
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            int index = playersList.SelectedIndex;
            var result = MessageBox.Show($"Вы удалите слот \"{savedList[index].StateName}\".", "Вы уверены?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                JsonSave.removeObject<Players>(MainWindow.path, index);
                data.RemoveAt(index);
            }
            playersList.ItemsSource = data;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("/StartGame.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
