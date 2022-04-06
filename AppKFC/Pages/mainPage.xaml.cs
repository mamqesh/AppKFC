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
        FastFoodEntities connection = new FastFoodEntities();
        public mainPage()
        {
            InitializeComponent();
            LoadRecipe();
        }

        private void LoadRecipe()
        {
            //var orders = connection.OrderCompound.ToList();
            //foreach (var _orders in orders)
            //{
            //    if (_orders.Status == "В работе")
            //    {
            //        ListOrders.Items.Add(_orders.Dish1.Name);
            //    }
            //}
        }


        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (labelListInProccess.Content != "")
            {
                var back = labelListInProccess.Content;
                ListOrders.Items.Add(back.ToString());
                labelListInProccess.Content = "";
            }
            else
            {
                MessageBox.Show("Вы ничего не выбрали");
            }
        }

        private void buttonReady_Click(object sender, RoutedEventArgs e)
        {
            if (labelListInProccess.Content == "")
            {
                MessageBox.Show("Вы ничего не выбрали!");
            }
            else
            {
                labelListInProccess.Content = "";
                ListIngredients.Items.Clear();
                MessageBox.Show("Готов к выдаче");
            }

        }

    private void buttonDone_Click(object sender, RoutedEventArgs e)
        {
            string dish;
            var ingridients = connection.DishCompound.ToList();
            ListIngredients.Items.Clear();
            if (labelListInProccess.Content != "")
            {
                MessageBox.Show("В работе " + labelListInProccess.Content + "\nВыберите Отмена или Готово");
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
                        if (dish == _ingridients.Dish1.Name)
                        {
                            Order ordered = new Order();
                            ordered.Status = "В работе";
                            ListIngredients.Items.Add(_ingridients.Ingredient1.Name);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите блюдо");
                }
            }
        }




        //public Oficant()
        //{
        //    InitializeComponent();

        //    var dishes = Baza.Dish.ToList();

        //    foreach (var _dish in dishes)
        //    {
        //        test2.Items.Add(_dish.Name);
        //    }
        //}

        //user_07Entities Baza = new user_07Entities();
        //Dictionary<int, Dish[]> Zakaz = new Dictionary<int, Dish[]>();
        //int a = 1;

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    test3.Items.Add(test2.SelectedItem);
        //}
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    if (test3.Items.Count == 0)
        //    {
        //        MessageBox.Show("Выберите блюдо");
        //        return;
        //    }

        //    nomervid.Items.Add(a);
        //    Dish[] dishes = new Dish[test3.Items.Count];
        //    for (int i = 0; i < test3.Items.Count; i++)
        //    {
        //        dishes[i] = test3.Items[i] as Dish;
        //    }
        //    Zakaz[a] = dishes;
        //    a++;
        //    test3.Items.Clear();
        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{

        //    if (nomervid.SelectedIndex > -1)
        //    {
        //        nomervid.Items.RemoveAt(nomervid.SelectedIndex);
        //    }
        //    if (test3.SelectedIndex > -1)
        //    {
        //        test3.Items.RemoveAt(test3.SelectedIndex);
        //    }
        //    if (Vidacha.SelectedIndex > -1)
        //    {
        //        Vidacha.Items.RemoveAt(Vidacha.SelectedIndex);
        //    }

        //}
        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    Vidacha.Items.Add(nomervid.SelectedItem);
        //    if (nomervid.SelectedIndex > -1)
        //    {
        //        nomervid.Items.RemoveAt(nomervid.SelectedIndex);
        //    }
        //}

    }
}
