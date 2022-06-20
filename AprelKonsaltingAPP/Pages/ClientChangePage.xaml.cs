using AprelKonsaltingAPP.DataBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    public partial class ClientChangePage : Page
    {
        client client1;
        public ClientChangePage(client client)
        {
            InitializeComponent();
            client1 = client;
            DataContext = client1;
        }

        private void ClientChange(object sender, RoutedEventArgs e)
        {
            try
            {
                if (client1.idclients == 0)
                {
                    client1.gender = tbGender.Text.ToLower();
                    EfModel.Init().clients.Add(client1);
                    if (client1.phonenumber == null)
                    {
                        MessageBox.Show("Требуется поле Телефон.");
                        return;
                    }
                    EfModel.Init().SaveChanges();
                    MessageBox.Show("Успешное сохранение");
                    NavigationService.GoBack();
                }
                else
                {
                    client1.gender = tbGender.Text.ToLower();
                    EfModel.Init().SaveChanges();
                    MessageBox.Show("Успешное сохранение");
                    NavigationService.GoBack();
                }
            }
            catch (DbEntityValidationException exp)
            { 
                MessageBox.Show(string.Join(Environment.NewLine, exp.EntityValidationErrors.Last().ValidationErrors.Select(u => u.ErrorMessage)));
            }
        }

        private void AddClientPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == true)
                client1.image = File.ReadAllBytes(op.FileName);
        }
    }
}
