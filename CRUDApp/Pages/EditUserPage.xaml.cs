using CRUDApp.DataBase;
using Microsoft.Win32;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace CRUDApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class EditUserPage : Page
    {
        public EditUserPage()
        {
            InitializeComponent();
            RoleCb.ItemsSource = App.Db.Role.ToList();
            RoleCb.DisplayMemberPath = "Name";
            User CurrentUser = GridPage.CurrentUser;
            FNameTb.Text = CurrentUser.FName;
            RoleCb.SelectedIndex = (int)(CurrentUser.idRole - 1);
            LoginTb.Text = CurrentUser.Login;
            PasswordPb.Password = CurrentUser.Password;
            if (CurrentUser.Image != null)
                imgPicture.Source = new BitmapImage(new Uri(CurrentUser.Image));
            ImageSource = CurrentUser.Image;


        }
        public string ImageSource { get; set; }
        public int Count = App.Db.User.Count();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
                imgPicture.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
            ImageSource = ofdPicture.FileName;



        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                GridPage.CurrentUser.FName = FNameTb.Text;
                GridPage.CurrentUser.idRole = RoleCb.SelectedIndex + 1;
                GridPage.CurrentUser.Login = LoginTb.Text;
                GridPage.CurrentUser.Password = PasswordPb.Password;
                GridPage.CurrentUser.Image = ImageSource;
                App.Db.SaveChanges();
                MessageBox.Show("Все прошло нормально");
                NavigationService.GoBack();

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

        }


    }
}
