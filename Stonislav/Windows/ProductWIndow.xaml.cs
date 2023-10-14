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
using System.Windows.Shapes;

namespace Stonislav.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductWIndow.xaml
    /// </summary>
    public partial class ProductWIndow : Window
    {
        public Frame MainFrame { get; set; }
        public ProductWIndow()
        {
            InitializeComponent();
            GetListService();
        }

        private void GetListService()
        {
            LvProduct.SelectionChanged += (s, e) => LvProduct.SelectedItem = null;
            LvProduct.ItemsSource = ClassHelper.EF.Context.Product.ToList();
        }
        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            AddProductWIndow addProductWindow = new AddProductWIndow();
            addProductWindow.ShowDialog();
            GetListService();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var service = button.DataContext as DataBase.Product;

            AddProductWIndow editServiceWindow = new AddProductWIndow(service);
            editServiceWindow.ShowDialog();
            GetListService();
        }


        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            var service = button.DataContext as DataBase.Product;
            var result = MessageBox.Show($"Вы уверены что хотите удалить услугу \"{service.Title}\"?", "Удаление услуги", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClassHelper.EF.Context.Product.Remove(service);
                ClassHelper.EF.Context.SaveChanges();
            }
            else
            {
                return;
            }

            GetListService();
        }

    }
}