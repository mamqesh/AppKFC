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
    /// Логика взаимодействия для loginAdm.xaml
    /// </summary>
    public partial class loginAdm : Page
    {
        danilEntities connection = new danilEntities();
        public loginAdm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxAdmLogin.Text.Length>0&&textBoxAdmPassword.Text.Length>0)
                {
                    string login = textBoxAdmLogin.Text.Trim();
                    string password = textBoxAdmPassword.Text.Trim();
                    var admin = connection.Administrator.Where(a => a.Login == login && a.Password == password).FirstOrDefault();
                    if (admin != null)
                    {
                        NavigationService.Navigate(MainWindow.pageAdminPage);
                    }
                    else
                    {
                        MessageBox.Show("Данный учетная запись не найдена", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
