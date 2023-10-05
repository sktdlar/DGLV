using CRUDApp.DataBase;
using Microsoft.Win32;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Drawing;

namespace CRUDApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
            RoleCb.ItemsSource = App.Db.Role.ToList();
            RoleCb.DisplayMemberPath = "Name";


        }
        public string ImageSource = "\\Source\\Cross.png";
        public int Count = App.Db.User.Count();
        public byte[] ImageBinary;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            OpenFileDialog ofdPicture = new OpenFileDialog();
            ofdPicture.Filter =
                "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
            ofdPicture.FilterIndex = 1;

            if (ofdPicture.ShowDialog() == true)
                imgPicture.Source =
                    new BitmapImage(new Uri(ofdPicture.FileName));
            ImageSource = ofdPicture.FileName;
            ImageBinary = File.ReadAllBytes(ImageSource);
            System.Drawing.Image image;
            using(var memoryStream = new MemoryStream(ImageBinary))
            {
                image = System.Drawing.Image.FromStream(memoryStream);
            }
            }
            catch
            {
                MessageBox.Show("что-то не так(((");
            }



        }

        private void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RoleCb.SelectedItem is null)
                {
                    MessageBox.Show("Выберите роль!");
                }
                else
                {
                    App.Db.User.Add(new User()
                    {
                        FName = FNameTb.Text,
                        idRole = RoleCb.SelectedIndex + 1,
                        Login = LoginTb.Text,
                        Password = PasswordPb.Password,
                        Image = ImageSource,
                        ImageBinary = this.ImageBinary
                    });
                    App.Db.SaveChanges();
                    MessageBox.Show("Все прошло нормально");
                    NavigationService.GoBack();


                }

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
