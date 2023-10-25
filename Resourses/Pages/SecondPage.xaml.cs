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

namespace WpfApp2.Resourses.Pages
{
    /// <summary>
    /// Логика взаимодействия для SecondPage.xaml
    /// </summary>
    public partial class SecondPage : Page
    {
        private Ivent _currentIvent = new Ivent();
        public SecondPage(Ivent seletedIvent)
        {
            if (seletedIvent != null)
            {
                _currentIvent = seletedIvent;
            }

            InitializeComponent();
            DataContext = _currentIvent;
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentIvent.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentIvent.Date == null)
                errors.AppendLine("Укажите дату");
            if(errors.Length > 0) 
            {
                MessageBox.Show(errors.ToString());
                return; 
            }
            if (_currentIvent.Id_ivent == 0)
                PR1_chessEntities.GetContext().Ivent.Add(_currentIvent);
            PR1_chessEntities.GetContext().SaveChanges();

            try
            {
                PR1_chessEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
                NavigationService.Navigate(new StartPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
