using AprelKonsaltingAPP.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class ClientListPage : Page
    {
        private bool MGender = false;
        private bool WGender = false;
        private bool DateFlag = false;
        private bool PhoneNumberFlag = false;
        private bool EmailFlag = false;


        private async void UpdateData()
        {
            IEnumerable<client> clients = EfModel.Init().clients.Where(s => s.surname.Contains(Search.Text) || s.firstname.Contains(Search.Text) || s.middlename.Contains(Search.Text));
            if (MGender)
            {
                clients = clients.Where(u => u.gender == "м");
            }
            if (WGender)
            {
                clients = clients.Where(u => u.gender == "ж");
            }
            if (DateFlag)
            {
                clients = clients.Where(u => u.DOB.Value.Day == DateTime.Now.Day && u.DOB.Value.Month == DateTime.Now.Month);
            }
            if (PhoneNumberFlag)
            {
                File.Delete("ClientsPhoneNumbers.txt");
                foreach (client client in clients)
                {
                    StreamWriter sw = new StreamWriter("ClientsPhoneNumbers.txt", true);
                    sw.WriteLine($"{client.phonenumber}");
                    sw.Dispose();   
                    PhoneNumberFlag = false;
                }
                MessageBox.Show("ClientsPhoneNumbers.txt Составлен.");
            }
            if (EmailFlag)
            {
                File.Delete("ClientsEmails.txt");

                foreach (client client in clients)
                {
                    StreamWriter sw = new StreamWriter("ClientsEmails.txt", true);
                    sw.WriteLine($"{client.email}");
                    sw.Dispose();
                    EmailFlag = false;
                }
                MessageBox.Show("ClientsEmails.txt Составлен.");
            }
            await Task.Run(() => Dispatcher.Invoke(() => ClientList.ItemsSource = clients.ToList()));
        }

        public ClientListPage()
        {
            InitializeComponent();
            UpdateData();
            DateColorChange();
        }

        private void ChangeClientBT(object sender, RoutedEventArgs e)
        {
            client client = (sender as Button).DataContext as client;
            NavigationService.Navigate(new ClientChangePage(client));
        }

        private void DeleteClientBT(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить клиента?","Внимание!",MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
            {
                client client = (sender as Button).DataContext as client;
                EfModel.Init().clients.Remove(client);
                EfModel.Init().SaveChanges();
                UpdateData();
            }
            else 
            {
                return;
            }
         
        }
        private void DateColorChange()
        {
            Date.Text = DateTime.Now.ToString("yyyy-MM-dd");
            {
                int year = DateTime.Now.Year;
                var Date31Dec = new DateTime(year, 12, 31);
                var Date7Jan = new DateTime(year, 01, 07);
                var Date23Feb = new DateTime(year, 02, 23);
                var Date8Mar = new DateTime(year, 03, 08);
                var Date1May = new DateTime(year, 05, 01);
                var Date9May = new DateTime(year, 05, 09);
                var Date12Jun = new DateTime(year, 06, 12);
                var Date4Nov = new DateTime(year, 11, 04);
                var parsedDate = DateTime.Parse(Date.Text);
                if (parsedDate == Date31Dec)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(46, 46, 206));
                if (parsedDate == Date7Jan)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(224, 130, 5));
                if (parsedDate == Date23Feb)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(51, 84, 65));
                if (parsedDate == Date8Mar)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(247, 129, 207));
                if (parsedDate == Date1May)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(96, 204, 122));
                if (parsedDate == Date9May)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(239, 127, 24));
                if (parsedDate == Date12Jun)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(249, 83, 83));
                if (parsedDate == Date4Nov)
                    Date.Foreground = new SolidColorBrush(Color.FromRgb(67, 192, 221));
            }
            {
                int year = DateTime.Now.Year;
                var Date31Dec = new DateTime(year, 12, 31);
                var Date7Jan = new DateTime(year, 01, 07);
                var Date23Feb = new DateTime(year, 02, 23);
                var Date8Mar = new DateTime(year, 03, 08);
                var Date1May = new DateTime(year, 05, 01);
                var Date9May = new DateTime(year, 05, 09);
                var Date12Jun = new DateTime(year, 06, 12);
                var Date4Nov = new DateTime(year, 11, 04);
                var parsedDate = DateTime.Parse(Date.Text);
                if (parsedDate == Date31Dec)
                    Prazd.Text = "Новый Год";
                if (parsedDate == Date7Jan)
                    Prazd.Text = "Рождество Христово";
                if (parsedDate == Date23Feb)
                    Prazd.Text = "День защитника Отечества";
                if (parsedDate == Date8Mar)
                    Prazd.Text = "Международный женский день";
                if (parsedDate == Date1May)
                    Prazd.Text = "Праздник Весны и Труда";
                if (parsedDate == Date9May)
                    Prazd.Text = "День Победы";
                if (parsedDate == Date12Jun)
                    Prazd.Text = "День России";
                if (parsedDate == Date4Nov)
                    Prazd.Text = "День народного единства";
            }
        }
        private void AddClientBT(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientChangePage(new client()));
        }

        private void SearhChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void MaleSearch(object sender, RoutedEventArgs e)
        {
            if (MGender == false)
            {
                BTMSearch.Background = new SolidColorBrush(Color.FromRgb(161, 97, 229));
                MGender = true;
                UpdateData();
            }
            else
            {
                BTMSearch.Background = new SolidColorBrush(Color.FromRgb(67, 156, 6));
                MGender = false ;
                UpdateData();
            }
        }

        private void FemaleSearch(object sender, RoutedEventArgs e)
        {
            if (WGender == false)
            {
                BTFSearch.Background = new SolidColorBrush(Color.FromRgb(161, 97, 229));
                WGender = true;
                UpdateData();
            }
            else
            {
                BTFSearch.Background = new SolidColorBrush(Color.FromRgb(67, 156, 6));
                WGender = false;
                UpdateData();
            }
        }

        private void DOBSearch(object sender, RoutedEventArgs e)
        {
            if (DateFlag == false)
            {
                BTDOBSearch.Background = new SolidColorBrush(Color.FromRgb(161, 97, 229));
                DateFlag = true;
                UpdateData();
            }
            else
            {
                BTDOBSearch.Background = new SolidColorBrush(Color.FromRgb(67, 156, 6));
                DateFlag = false;
                UpdateData();
            }
        }

        private void UpdateDataEvent(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateData();
        }

        private void NumberClick(object sender, RoutedEventArgs e)
        {
            PhoneNumberFlag = true;
            UpdateData();
        }

        private void EmailClick(object sender, RoutedEventArgs e)
        {
            EmailFlag = true;
            UpdateData();
        }
    }
}
