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

namespace AppKFC
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        danilEntities connection = new danilEntities();   
        public static Pages.mainPage pageMainPage;
        public static Pages.userPage pageUserPage;
        public static Pages.registrationPage pageRegistrationPage;
        public static Pages.registrationPageClient pageRegistrationPageClient;
        public static Pages.firstPage pageFirstPage;
        public static Pages.loginPageClient pageLoginPageClient;
        public static Pages.regAdmin pageRegAdmin;
        public static Pages.loginAdm pageLoginAdmin;
        public static Pages.adminPage pageAdminPage;
        public static string Name;

        public MainWindow()
        {
            InitializeComponent();
            pageMainPage = new Pages.mainPage();
            pageUserPage = new Pages.userPage();
            pageRegistrationPage = new Pages.registrationPage();
            pageRegistrationPageClient = new Pages.registrationPageClient();
            pageFirstPage = new Pages.firstPage();
            pageLoginPageClient = new Pages.loginPageClient();
            pageRegAdmin = new Pages.regAdmin();
            pageLoginAdmin = new Pages.loginAdm();
            pageAdminPage = new Pages.adminPage();
            var admin = connection.Administrator.ToList().Count();
            if (admin == 0)
            {
                string messageBoxText = "Не найден пользователь Администратор. Для полного использования " +
                   "функционала приложения нужно создать Администратора. Создать его?";
                string caption = "Проверка наличия администратора";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MainFrame.NavigationService.Navigate(pageRegAdmin);
                        break;
                    case MessageBoxResult.No:
                        MainFrame.NavigationService.Navigate(pageFirstPage);
                        break;
                }
            }
            else
            {
            MainFrame.Navigate(pageFirstPage);
            }
        }

      
    }
}
