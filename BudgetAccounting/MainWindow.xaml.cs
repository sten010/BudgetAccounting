using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace BudgetAccounting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public List<budgetAccount> Accounts = new();
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CreateAccoun(NameText.Text, DescriptionText.Text, Convert.ToDouble(MoneyText.Text));
        }
        private void Change_Click(object sender, RoutedEventArgs e)
        {
            ChangeAccoun(BudgetList.SelectedIndex, NameText.Text, DescriptionText.Text, Convert.ToDouble(MoneyText.Text));
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteAccoun(BudgetList.SelectedIndex);
        }
        private void BudgetList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BudgetList.SelectedItem is not budgetAccount currect) return;
            NameText.Text = currect?.Name.Text;
            DescriptionText.Text = currect?.Description.Text;
            MoneyText.Text = currect?.Money.Text;
        }
        private void CreateAccoun( string name, string description, double money)
        {
            budgetAccount budgetAccount = new budgetAccount();
            if (money > 0)
            {
                budgetAccount.AccountImage.Source = new BitmapImage(new Uri(@"/Image/plus.png", UriKind.Relative));
            }
            else
            {
                budgetAccount.AccountImage.Source = new BitmapImage(new Uri(@"/Image/minus.png" , UriKind.Relative));
            }
            budgetAccount.Name.Text = name;
            budgetAccount.Description.Text = description;
            budgetAccount.Money.Text = money.ToString();
            Accounts.Add(budgetAccount);
            RefreshValue();
        }
        private void ChangeAccoun(int id , string name, string description, double money)
        {
            var item = Accounts[id];
            if (money > 0)
            {
                item.AccountImage.Source = new BitmapImage(new Uri(@"/Image/plus.png", UriKind.Relative));
            }
            else
            {
                item.AccountImage.Source = new BitmapImage(new Uri(@"/Image/minus.png", UriKind.Relative));
            }
            item.Name.Text = name;
            item.Description.Text = description;
            item.Money.Text = money.ToString();
        }
        private void DeleteAccoun(int id)
        {
            Accounts.RemoveAt(id);
            RefreshValue();
        }

        private void RefreshValue()
        {
            BudgetList.ItemsSource = null;
            BudgetList.ItemsSource = Accounts;
            CountList.Content = string.Format("Все запиисей:{0}", Accounts.Count);
            TotalMoney.Content = string.Format("Итого:{0}", Accounts.Sum(c =>Convert.ToDouble(c.Money.Text)));
        }

        
    }
}
