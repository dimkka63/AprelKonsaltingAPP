using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace AprelKonsaltingAPP.Pages
{
    public partial class DBConnectionPage : Page
    {
        public DBConnectionPage()
        {
            InitializeComponent();
            DBHost.Text = Properties.Settings.Default.Host;
            DBPass.Password = Properties.Settings.Default.Password;
            DBUser.Text = Properties.Settings.Default.UserName;
            DBPort.Text = Properties.Settings.Default.Port;
            DBDataBase.Text = Properties.Settings.Default.DataBase;
        }

        private void CheckProperties(object sender, RoutedEventArgs e)
        {
            string Connection = $"server={Properties.Settings.Default.Host};port={Properties.Settings.Default.Port};user id={Properties.Settings.Default.UserName};password={Properties.Settings.Default.Password};database={Properties.Settings.Default.DataBase}";
            try
            {
                MySqlConnection MyConnection = new MySqlConnection(Connection);

                MyConnection.Open();

                if (MyConnection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Успешное подключение");
                }
                else
                {
                    MessageBox.Show("Не подключено");
                }
                MyConnection.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка, не удалось установить подключение, проверьте введенные данные или обратитесь за помощью к администратору");
            }
        }

        private void SaveProperties(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Host = DBHost.Text;
            Properties.Settings.Default.Password = DBPass.Password;
            Properties.Settings.Default.UserName = DBUser.Text;
            Properties.Settings.Default.Port = DBPort.Text;
            Properties.Settings.Default.DataBase = DBDataBase.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Данные сохранены");
        }

        private void EnterProgram(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientListPage());
        }
    }
}
