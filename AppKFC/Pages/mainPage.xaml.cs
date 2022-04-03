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
    /// Логика взаимодействия для mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        public mainPage()
        {
            InitializeComponent();
        }

        private void LoadRecipe()
        {
            FastFoodEntities connection = new FastFoodEntities();
            var orders = connection.OrderCompound.ToList();
            foreach (var _orders in orders)
            {
                if (_orders.Status == "В работе")
                {
                    ListOrders.Items.Add(_orders.Dish.Name);
                }
            }
        }

        private void btnDone(object sender, RoutedEventArgs e)
        {
            FastFoodEntities connection = new FastFoodEntities();
            string dish;
            var ingridients = connection.DishCompound.ToList();
            ListIngredients.Items.Clear();
            if (labelListInProccess.Content != "")
            {
                MessageBox.Show("Вы уже готовите" + labelListInProccess.Content + "\nВыберите Отмена или Готово");
            }
            else
            {
                if (ListOrders.SelectedIndex != -1)
                {
                    dish = this.ListOrders.SelectedItem.ToString();
                    labelListInProccess.Content = this.ListOrders.SelectedItem.ToString();
                    ListOrders.Items.RemoveAt(ListOrders.SelectedIndex);
                    foreach (var _ingridients in ingridients)
                    {
                        if (dish==_ingridients.Dish.Name)
                        {
                            Order ordered = new Order();
                            ordered.Status = "В работе";
                            ListIngredients.Items.Add(_ingridients.Ingredient.Name);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите блюдо");
                }
            }
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            if (labelListInProccess.Content !="")
            {
                var back = labelListInProccess.Content;
                ListOrders.Items.Add(back.ToString());
                labelListInProccess.Content = "";
            }
            else
            {
                MessageBox.Show("Вы ничего не готовите!");
            }
        }

        private void btnDeny(object sender, RoutedEventArgs e)
        {
            FastFoodEntities connection = new FastFoodEntities();
            if (labelListInProccess.Content=="")
            {
                MessageBox.Show("Вы ничего не готовите!");
            }
            else
            {
                labelListInProccess.Content = "";
                ListIngredients.Items.Clear();
                MessageBox.Show("Готов к выдаче");
            }
        }
    }
}
