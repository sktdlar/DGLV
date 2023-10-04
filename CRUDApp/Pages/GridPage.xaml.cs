using CRUDApp.DataBase;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CRUDApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для GridPage.xaml
    /// </summary>
    public partial class GridPage : Page
    {
        public static User CurrentUser { get; set; }

        public GridPage()
        {
            InitializeComponent();
            DgUsers.ItemsSource = App.Db.User.ToList();


        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DgUsers.ItemsSource = App.Db.User.ToList();
        }

        private void SelectImageBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddUserPage());
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser = (User)DgUsers.SelectedItem;
            NavigationService.Navigate(new Pages.EditUserPage());

        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Db.User.Remove((User)DgUsers.SelectedItem);
            App.Db.SaveChanges();
            DgUsers.ItemsSource = App.Db.User.ToList();
        }

        private void OpenListView_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ListViewPage());
        }
    }

}
