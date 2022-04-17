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
    /// Логика взаимодействия для userPage.xaml
    /// </summary>
    public partial class userPage : Page
    {
        FastFoodEntities connection = new FastFoodEntities();
        public userPage()
        {
            InitializeComponent();
            LoadDish();
        }

        void LoadDish()
        {
            var dish = connection.Dish.ToList();
            foreach (var _dish in dish)
            {
                 listBoxDish.Items.Add(_dish.Name); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBoxDish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            labelPrice.Content = "";
            var dish = listBoxDish.SelectedItem as Database.Dish;
            if (dish != null)
            {
                labelPrice.Content += "Цена\n" + dish.Price+"\n";
            }
        }
    }
}
