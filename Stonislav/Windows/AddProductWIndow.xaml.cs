using Microsoft.Win32;
using Stonislav.DataBase;
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
    /// Логика взаимодействия для AddProductWIndow.xaml
    /// </summary>
    public partial class AddProductWIndow : Window
    {
        private string pathImage;
        private bool isEdit = false;
        Product editProduct = null;
        public AddProductWIndow()
        {
            InitializeComponent();
        }

        public AddProductWIndow(Product product)
        {

            InitializeComponent();
            isEdit = true;
            editProduct = product;

            ImgImageProduct.Source = new BitmapImage(new Uri(editProduct.Image));
            TbTitle.Text = editProduct.Title;
            TbPrice.Text = Convert.ToString(editProduct.Price);
            TbInStock.Text = Convert.ToString(editProduct.InStock);

        }

        private void BtnImageSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            if (openfiledialog.ShowDialog() == true)
            {
                ImgImageProduct.Source = new BitmapImage(new Uri(openfiledialog.FileName));
                pathImage = openfiledialog.FileName;
            }
        }

        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbTitle.Text))
            {
                MessageBox.Show("Поле Title не заполнено");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbPrice.Text))
            {
                MessageBox.Show("Поле Price не заполнено");
                return;
            }
            if (!Char.IsDigit(TbPrice.Text, 0))
            {
                MessageBox.Show("Поле Price не может содержать не цифры");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbInStock.Text))
            {
                MessageBox.Show("Поле In stock не заполнено");
                return;
            }
            if (!Char.IsDigit(TbInStock.Text, 0))
            {
                MessageBox.Show("Поле In stock не может содержать не цифры");
                return;
            }

            if (isEdit == false)
            {
                Product addProduct = new Product
                {
                    Title = TbTitle.Text,
                    Price = Convert.ToDecimal(TbPrice.Text),
                    InStock = Convert.ToInt32(TbInStock.Text)
                };
                if (pathImage != null)
                {
                    addProduct.Image = pathImage;
                }

                ClassHelper.EF.Context.Product.Add(addProduct);
                ClassHelper.EF.Context.SaveChanges();

                MessageBox.Show("Услуга добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                editProduct.Title = TbTitle.Text;
                editProduct.Price = Convert.ToDecimal(TbPrice.Text);
                editProduct.InStock = Convert.ToInt32(TbInStock.Text);

                ClassHelper.EF.Context.SaveChanges();

                MessageBox.Show("Данные успешно сохранены");

                this.Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}