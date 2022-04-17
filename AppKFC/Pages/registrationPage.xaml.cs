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
    /// Логика взаимодействия для registrationPage.xaml
    /// </summary>
    public partial class registrationPage : Page
    {
        FastFoodEntities connection = new FastFoodEntities();

        public registrationPage()
        {
            InitializeComponent();
            textBoxLogin.ToolTip = "Введите ваш логин";
            passwordBoxPassword.ToolTip = "Введите ваш пароль";
        }

        private void Button_Click(object sender, RoutedEventArgs e) //ВХОД В УЧЕТКУ
        {
            var employees = connection.Employee.ToList();
            foreach (var _employees in employees)
            {
                if (textBoxLogin.ToString() == _employees.Phone)
                {
                    if (passwordBoxPassword.ToString() == _employees.Password)
                    {
                        NavigationService.Navigate(MainWindow.pageMainPage);
                    }
                }
            }
        }
    }
}
