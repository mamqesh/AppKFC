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
using System.Data.Entity;
using AppKFC.Database;

namespace AppKFC.Pages
{
    /// <summary>
    /// Логика взаимодействия для adminPage.xaml
    /// </summary>
    public partial class adminPage : Page
    {
        danilEntities connection = new danilEntities();
        public Database.Client client { get; set; }
        public List<Database.Client> clients { get; set; }
        public Database.Employee employee { get; set; }
        public List<Database.Employee> employees { get; set; }
        public Database.Dish dish { get; set; }
        public List<Database.Dish> dishes { get; set; }
        public adminPage()
        {
            InitializeComponent();
            clients = connection.Client.ToList();
            employees = connection.Employee.ToList();
            dishes = connection.Dish.ToList();
            DataContext = this;
            LoadIDDish();
        }
        void LoadIDDish()
        {
            int idCount = connection.Dish.ToList().Count() + 1;
            textBoxAddDishID.Text = idCount.ToString();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int result = connection.SaveChanges();
                if (result > 0)
                {
                    MessageBox.Show("Данные отредактированны", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        void LoadCleints()
        {
            textBoxClientPhone.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxClientNameSurnamePatronymic.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxAddress.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            listBoxClients.GetBindingExpression(ListBox.ItemsSourceProperty)?.UpdateTarget();
        }
        void LoadEmployee()
        {
            textBoxEmployeePhone.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxEmployeeName.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxEmployeeSurname.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxEmployeePatronymic.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxEmployeePassword.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            listBoxEmployees.GetBindingExpression(ListBox.ItemsSourceProperty)?.UpdateTarget();
        }
        void LoadDishes()
        {
            textBoxDishID.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxDishName.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            textBoxDishPrice.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
            lishBoxDish.GetBindingExpression(ListBox.ItemsSourceProperty)?.UpdateTarget();
        }
        private void listBoxClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            client = listBoxClients.SelectedItem as Database.Client;
            clients = connection.Client.ToList();
            LoadCleints();
        }

        private void textBoxSearchClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                var text = textBox.Text.Trim();
                clients = connection.Client.Where(c => DbFunctions.Like(c.FullName, "%" + text + "%")).ToList();
                LoadCleints();
                clients = connection.Client.ToList();
            }
        }

        private void listBoxEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            employee = listBoxEmployees.SelectedItem as Database.Employee;
            LoadEmployee();
            employees = connection.Employee.ToList();
        }

        private void textBoxEmployeeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                var text = textBox.Text.Trim();
                employees = connection.Employee.Where(emp => DbFunctions.Like(emp.Name, "%" + text + "%") || DbFunctions.Like(emp.Surname, "%" + text + "%") || DbFunctions.Like(emp.Patronymic, "%" + text + "%")).ToList();
                LoadEmployee();
                employees = connection.Employee.ToList();
            }
        }

        private void lishBoxDish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dish = lishBoxDish.SelectedItem as Database.Dish;
            LoadDishes();
            dishes = connection.Dish.ToList();

        }

        private void textBoxSearchDish_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                var text = textBox.Text.Trim();
                dishes = connection.Dish.Where(d => DbFunctions.Like(d.Name, "%" + text + "%")).ToList();
                LoadDishes();
                dishes = connection.Dish.ToList();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxAddDishName.Text.Length > 0 && textBoxAddDishPrice.Text.Length > 0)
                {
                    string name = textBoxAddDishName.Text.Trim();
                    string price = textBoxAddDishPrice.Text.Trim();

                    Database.Dish dish = new Database.Dish();
                    dish.ID = int.Parse(textBoxAddDishID.Text.Trim());
                    dish.Name = name;
                    dish.Price = int.Parse(price);
                    dish.Count = 0;
                    connection.Dish.Add(dish);
                    int result = connection.SaveChanges();
                    if (result > 0)
                    {
                        MessageBox.Show("Данные успешно добавлены", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Question);
                        textBoxAddDishName.Clear();
                        textBoxAddDishPrice.Clear();
                        LoadIDDish();
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
