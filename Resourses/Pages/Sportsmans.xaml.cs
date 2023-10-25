using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для Sportsmans.xaml
    /// </summary>
    public partial class Sportsmans : Page
    {
        public Sportsmans()
        {
            InitializeComponent();
            DtgSportsman.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
            CmbFilterCategory.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
            CmbFilterCategory.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Select(x => x.Category).Distinct().ToList();
            CmbFilterState.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
            CmbFilterState.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Select(x => x.Place).Distinct().ToList();
        }

        private void Web_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.championat.com/chess/rating/fide-men/2023-01-01/297/");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SportAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            var Remove1 = DtgSportsman.SelectedItems.Cast<Sportsman>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {Remove1.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PR1_chessEntities.GetContext().Sportsman.RemoveRange(Remove1);
                    PR1_chessEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DtgSportsman.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        private void ReAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SportAdd((sender as Button).DataContext as Sportsman));
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TbSerch.Clear();
            DtgSportsman.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
        }

        private void CmbFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Filter = CmbFilterCategory.SelectedValue.ToString();
            DtgSportsman.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Where(x => x.Category == Filter).ToList();
        }

        private void TbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TbSerch.Text;
            DtgSportsman.ItemsSource = PR1_chessEntities.GetContext().Sportsman.
                Where(x => x.Name.Contains(search)).ToList();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }

        private void Sport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmbFilterState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Filter2 = CmbFilterState.SelectedValue.ToString();
            DtgSportsman.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Where(x => x.Place == Filter2).ToList();
        }

        private void Page_IsVisibleChanger(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility== Visibility.Visible)
            {
                PR1_chessEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p =>p.Reload()); 
                DtgSportsman.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
            }
        }

        private void SportList_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SportsList());
        }

        private void Charts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChartPage());
        }
    }
}
