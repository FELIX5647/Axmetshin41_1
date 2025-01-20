using Axmetshin41;
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

namespace Axmetshin41
{
    
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var currentService = Axmetshin41Entities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentService;
            ComboType.SelectedIndex = 0;
            UpdateService();
        }

        private void UpdateService()
        {
            var currentService = Axmetshin41Entities.GetContext().Product.ToList();
            if(ComboType.SelectedIndex == 0)
            {
                currentService = currentService.Where(p => p.Discount >= 0 && p.Discount <= 100).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentService = currentService.Where(p => p.Discount >= 0 && p.Discount <= 9.99).ToList();
            }
            if(ComboType.SelectedIndex == 2)
            {
                currentService = currentService.Where(p => p.Discount >= 10 && p.Discount <= 14.99).ToList();
            }
            if(ComboType.SelectedIndex == 3)
            {
                currentService = currentService.Where(p => p.Discount >= 15 && p.Discount <= 100).ToList();
            }
            currentService = currentService.Where(p => p.ProductName.ToLower().Contains(TBSearch.Text.ToLower())).ToList();
            ProductListView.ItemsSource = currentService.ToList();
            if (RBtnCostUp.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentService.OrderBy(p => p.ProductCost).ToList();
            }
            if (RBtnCostDown.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentService.OrderByDescending(p => p.ProductCost).ToList();
            }
            Productcounht.Text = "Кол-во "+ currentService.Count + " из " + Axmetshin41Entities.GetContext().Product.ToList().Count;
        }
        private void BtnGo_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void RBtnCostUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateService();
        }

        private void RBtnCostDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateService();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService();
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateService();
        }
        
    }
}
