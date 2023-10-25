using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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

namespace WpfApp2.Resourses.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        
        public StartPage()
        {
            InitializeComponent();
            DtgIvents.ItemsSource = PR1_chessEntities.GetContext().Ivent.ToList();
            CmbFilterName.ItemsSource = PR1_chessEntities.GetContext().Ivent.ToList();
            CmbFilterName.ItemsSource = PR1_chessEntities.GetContext().Ivent.Select(x => x.Name).Distinct().ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SecondPage(null));
        }

        private void Web_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://ratings.ruchess.ru/tournaments");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var Remove = DtgIvents.SelectedItems.Cast<Ivent>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {Remove.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PR1_chessEntities.GetContext().Ivent.RemoveRange(Remove);
                    PR1_chessEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DtgIvents.ItemsSource = PR1_chessEntities.GetContext().Ivent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void TbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TbSerch.Text;
            DtgIvents.ItemsSource = PR1_chessEntities.GetContext().Ivent.
                Where(x => x.Name.Contains(search)).ToList();
        }

        private void CmbFilterName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string NameFilter = CmbFilterName.SelectedValue.ToString();
            DtgIvents.ItemsSource = PR1_chessEntities.GetContext().Ivent.Where(x => x.Name == NameFilter).ToList();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TbSerch.Clear();
            DtgIvents.ItemsSource = PR1_chessEntities.GetContext().Ivent.ToList();
        }

        private void ReAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SecondPage((sender as Button).DataContext as Ivent));
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sportsmans());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                PR1_chessEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DtgIvents.ItemsSource = PR1_chessEntities.GetContext().Ivent.ToList();
            }
        }

        private void SportList_Click(object sender, RoutedEventArgs e)
        {
                NavigationService.Navigate(new SportsList());
        }
    }
}
