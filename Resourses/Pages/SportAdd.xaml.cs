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
    /// Логика взаимодействия для SportAdd.xaml
    /// </summary>
    public partial class SportAdd : Page
    {
        private Sportsman _currentSportsman = new Sportsman();
        public SportAdd(Sportsman seletedSportsman)
        {
            if (seletedSportsman != null)
            {
                _currentSportsman = seletedSportsman;
            }

            InitializeComponent();
            DataContext = _currentSportsman;
            CmbState.ItemsSource = PR1_chessEntities.GetContext().State.ToList();
            CmbState.SelectedValuePath = "Id_state";
            CmbState.DisplayMemberPath = "Name";

            CmbIvent.ItemsSource = PR1_chessEntities.GetContext().Ivent.ToList();
            CmbIvent.SelectedValuePath = "Id_ivent";
            CmbIvent.DisplayMemberPath = "Name";
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentSportsman.Name))
            {
                errors.AppendLine("Укажите имя спортсмена");
            }
            if (_currentSportsman.Birth == null)
                errors.AppendLine("Укажите дату рождения");
            if (string.IsNullOrWhiteSpace(_currentSportsman.Category))
                errors.AppendLine("Укажите категорию");
            if (string.IsNullOrWhiteSpace(_currentSportsman.Place))
                errors.AppendLine("Укажите место");
            if (_currentSportsman.Id_ivent == null)
                errors.AppendLine("Укажите турнир");
            if (_currentSportsman.Id_state == null)
                errors.AppendLine("Укажите страну");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentSportsman.ID == 0)
                PR1_chessEntities.GetContext().Sportsman.Add(_currentSportsman);
            try
            {
                PR1_chessEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
                NavigationService.Navigate(new Sportsmans());
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
