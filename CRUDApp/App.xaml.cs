using System.Windows;

namespace CRUDApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DataBase.CRUDEntities Db = new DataBase.CRUDEntities();
    }
}
