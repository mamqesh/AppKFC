using System;
using System.Collections.Generic;
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
using AppKFC.Database;

namespace AppKFC.Pages
{
    /// <summary>
    /// Логика взаимодействия для regAdmin.xaml
    /// </summary>
    public partial class regAdmin : Page
    {
        danilEntities connection = new danilEntities();
        public regAdmin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxAdmLogin.Text.Length > 0 && textBoxAdmPassword.Text.Length > 0 )
                {
                    string login = textBoxAdmLogin.Text.Trim();
                    string password = textBoxAdmPassword.Text.Trim();
                    string loginRepeat = textBoxAdmLogin_Copy.Text.Trim();
                    string passwordRepeat = textBoxAdmPassword_Copy.Text.Trim();
                    if (login==loginRepeat&&password==passwordRepeat)
                    {
                        Database.Administrator administrator = new Database.Administrator();
                        administrator.Login = login;
                        administrator.Password = password;
                        connection.Administrator.Add(administrator);
                        int result = connection.SaveChanges();
                        if (result>0)
                        {
                            MessageBox.Show("Администратор успешно зарегестрирован\nЛогин: " + login + "\nПароль: " + password,"Предупреждение", MessageBoxButton.OK, MessageBoxImage.Question);
                            NavigationService.Navigate(MainWindow.pageFirstPage);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данные не совпадают", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля авторизации", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
