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
        double Amount = 0;
        //FastFoodEntities connection = new FastFoodEntities();
        danilEntities connection = new danilEntities();
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

        private void Button_Click(object sender, RoutedEventArgs e) // ДОБАВЛЕНИЕ БЛЮДА В ЗАКАЗ
        {

            labelYourOrder.Content = "Ваш заказ";
            if (listBoxDish.SelectedIndex!=-1)
            {
                listBoxOrder.Items.Add(listBoxDish.SelectedItem.ToString());
                labelAmount.Content = "Стоимость заказа:" + Amount.ToString() + " $\n";

                var dish = connection.Dish.ToList();
                labelPrice.Content = "";
                var dish1 = listBoxDish.SelectedItem;

                foreach (var _dish in dish)
                {
                    if (dish1.ToString() == _dish.Name)
                    {
                        double price = Convert.ToDouble(_dish.Price);
                        Amount += price;
                        labelAmount.Content = "Стоимость заказа:" + Amount.ToString() + " $\n";
                    }
                }
                

            }
        }

        private void listBoxDish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dish = connection.Dish.ToList();
            labelPrice.Content = "";
            var dish1 = listBoxDish.SelectedItem;
            foreach  (var _dish in dish)
            {
                if (dish1.ToString() == _dish.Name)
                {
                    double price = Convert.ToDouble(_dish.Price);
                    labelPrice.Content = "Цена\n" + price.ToString() + " $\n";
                   

                }
            }

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//УДАЛЕНИЕ БЛЮДА ИЗ ЗАКАЗА
        {
            if (listBoxOrder.SelectedIndex != -1)
            {
                labelAmount.Content=Amount.ToString();

                var dish = connection.Dish.ToList();
                labelPrice.Content = "";
                var dish1 = listBoxOrder.SelectedItem;

                foreach (var _dish in dish)
                {
                    if (dish1.ToString() == _dish.Name)
                    {
                        double price = Convert.ToDouble(_dish.Price);
                        Amount -= price;
                        labelAmount.Content = "Стоимость заказа:" + Amount.ToString() + " $\n";
                       

                    }
                }

                listBoxOrder.Items.RemoveAt(this.listBoxOrder.SelectedIndex);
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //СДЕЛАТЬ ЗАКАЗ <---- ТУТ НЕ РАБОТАЕТ 
        {
            Database.danilEntities connection = new Database.danilEntities();
            Database.Order order = new Order();
            var id = connection.Order.ToList().Count() + 1;
            var countDish = connection.OrderCompound.ToList().Count() + 1;
            string nameClient = MainWindow.Name;
            order.Client = nameClient;
            order.Employee = "9921161534";
            order.Date = DateTime.Now;
            order.Status = "В работе";
            connection.Order.Add(order);
            if (listBoxOrder.Items.Count <= 0)
            {
                MessageBox.Show("Вы не добавили блюда в корзину");
            }
            else
            {
                foreach (var _dishes in listBoxOrder.Items)
                {
                    Database.OrderCompound orderCompound = new Database.OrderCompound();
                    var dish = _dishes as Database.Dish;
                    try
                    {
                        if (dish != null)
                        {
                            orderCompound.Dish = dish.ID;
                            orderCompound.Order = id;
                            orderCompound.Price = Convert.ToInt32(Amount);
                            orderCompound.Count = 1;
                            orderCompound.Status = "В работе";
                            connection.OrderCompound.Add(orderCompound);
                            listBoxDish.Items.Clear();
                            listBoxOrder.Items.Clear();
                            labelPrice.Content = "";
                            labelAmount.Content = "";
                            labelYourOrder.Content = "";
                            connection.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка в оформление заказа!");
                        }
                        int result = connection.SaveChanges();
                        if (result > 0)
                        {
                            listBoxOrder.Items.Clear();
                            MessageBox.Show("Ваш заказ добавлен в очередь приготовления");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка в оформление заказа!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
