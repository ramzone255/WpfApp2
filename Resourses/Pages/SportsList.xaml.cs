using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace WpfApp2.Resourses.Pages
{
    /// <summary>
    /// Логика взаимодействия для SportsList.xaml
    /// </summary>
    public partial class SportsList : Page
    {
        public SportsList()
        {      
            InitializeComponent();
            var currentUser = PR1_chessEntities.GetContext().Sportsman.ToList();
            LViewSport.ItemsSource = currentUser;
            DataContext = LViewSport;
            CmbFilterCategory.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
            CmbFilterCategory.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Select(x => x.Category).Distinct().ToList();
            CmbFilterState.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
            CmbFilterState.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Select(x => x.Place).Distinct().ToList();
        }

        private void Sport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Sportsmans());
        }
        private void Web_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.championat.com/chess/rating/fide-men/2023-01-01/297/");
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TbSerch.Clear();
            LViewSport.ItemsSource = PR1_chessEntities.GetContext().Sportsman.ToList();
        }

        private void CmbFilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Filter = CmbFilterCategory.SelectedValue.ToString();
            LViewSport.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Where(x => x.Category == Filter).ToList();
        }

        private void TbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = TbSerch.Text;
            LViewSport.ItemsSource = PR1_chessEntities.GetContext().Sportsman.
                Where(x => x.Name.Contains(search)).ToList();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartPage());
        }

        private void CmbFilterState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string Filter2 = CmbFilterState.SelectedValue.ToString();
            LViewSport.ItemsSource = PR1_chessEntities.GetContext().Sportsman.Where(x => x.Place == Filter2).ToList();
        }

        private void SportList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SportAdd((sender as Button).DataContext as Sportsman));
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            var ExcelApp = new Excel.Application();

            Excel.Workbook wb = ExcelApp.Workbooks.Add();

            Excel.Worksheet worksheet = ExcelApp.Worksheets.Item[1];

            int indexRows = 1;

            worksheet.Cells[2][indexRows] = "Имя";
            worksheet.Cells[3][indexRows] = "Дата рождения";
            worksheet.Cells[4][indexRows] = "Категория";
            worksheet.Cells[5][indexRows] = "Место";
            worksheet.Cells[6][indexRows] = "Ивент";
            worksheet.Cells[7][indexRows] = "Страна";

            var printItems = LViewSport.Items;

            foreach (Sportsman item in printItems)
            {
                worksheet.Cells[1][indexRows + 1] = indexRows;
                worksheet.Cells[2][indexRows + 1] = item.Name;
                worksheet.Cells[3][indexRows + 1] = item.Birth;
                worksheet.Cells[4][indexRows + 1] = item.Category;
                worksheet.Cells[5][indexRows + 1] = item.Place;
                worksheet.Cells[6][indexRows + 1] = item.Ivent.Name;
                worksheet.Cells[7][indexRows + 1] = item.State.Name;

                indexRows++;
            }
            Excel.Range range = worksheet.Range[worksheet.Cells[2][indexRows + 1],
                    worksheet.Cells[7][indexRows + 1]];

            range.ColumnWidth = 20;

            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            ExcelApp.Visible = true;
        }

        private void Word_Click(object sender, RoutedEventArgs e)
        {
            var SportsmanInWord = PR1_chessEntities.GetContext().Sportsman.ToList();

            var SportApplication = new Word.Application();

            Word.Document document = SportApplication.Documents.Add();

            Word.Paragraph empParagraph = document.Paragraphs.Add();
            Word.Range empRange = empParagraph.Range;
            empRange.Text = "Sportsmans";
            empRange.Font.Bold = 4;
            empRange.Font.Italic = 4;
            empRange.Font.Color = Word.WdColor.wdColorBlack;
            empRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table paymentsTable = document.Tables.Add(tableRange, SportsmanInWord.Count() + 1, 6);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;

            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Имя";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Дата рождения";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Категория";
            cellRange = paymentsTable.Cell(1, 4).Range;
            cellRange.Text = "Место";
            cellRange = paymentsTable.Cell(1, 5).Range;
            cellRange.Text = "Ивент";
            cellRange = paymentsTable.Cell(1, 6).Range;
            cellRange.Text = "Страна";

            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 0; i < SportsmanInWord.Count(); i++)
            {
                var SportsmansCurrent = SportsmanInWord[i];

                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = SportsmansCurrent.Name;

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = SportsmansCurrent.Birth.ToString();

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = SportsmansCurrent.Category;

                cellRange = paymentsTable.Cell(i + 2, 4).Range;
                cellRange.Text = SportsmansCurrent.Place;

                cellRange = paymentsTable.Cell(i + 2, 5).Range;
                cellRange.Text = SportsmansCurrent.Ivent.Name;

                cellRange = paymentsTable.Cell(i + 2, 6).Range;
                cellRange.Text = SportsmansCurrent.State.Name;
            }

            SportApplication.Visible = true;

            document.SaveAs2(@"C:\Users\User\OneDrive\Desktop\Проказников\WpfApp2\Resourses\Files\Sportsmans.docx");
        }

        private void PDF_Click(object sender, RoutedEventArgs e)
        {
            var SportsmanInPDF = PR1_chessEntities.GetContext().Sportsman.ToList();

            var SportApplicationPDF = new Word.Application();

            Word.Document document = SportApplicationPDF.Documents.Add();

            Word.Paragraph empParagraph = document.Paragraphs.Add();
            Word.Range empRange = empParagraph.Range;
            empRange.Text = "Sportsmans";
            empRange.Font.Bold = 4;
            empRange.Font.Italic = 4;
            empRange.Font.Color = Word.WdColor.wdColorBlack;
            empRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table paymentsTable = document.Tables.Add(tableRange, SportsmanInPDF.Count() + 1, 6);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;

            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Имя";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Дата рождения";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Категория";
            cellRange = paymentsTable.Cell(1, 4).Range;
            cellRange.Text = "Место";
            cellRange = paymentsTable.Cell(1, 5).Range;
            cellRange.Text = "Ивент";
            cellRange = paymentsTable.Cell(1, 6).Range;
            cellRange.Text = "Страна";


            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 0; i < SportsmanInPDF.Count(); i++)
            {
                var SportsmansCurrent = SportsmanInPDF[i];

                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = SportsmansCurrent.Name;

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = SportsmansCurrent.Birth.ToString();

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = SportsmansCurrent.Category;

                cellRange = paymentsTable.Cell(i + 2, 4).Range;
                cellRange.Text = SportsmansCurrent.Place;

                cellRange = paymentsTable.Cell(i + 2, 5).Range;
                cellRange.Text = SportsmansCurrent.Ivent.Name;

                cellRange = paymentsTable.Cell(i + 2, 6).Range;
                cellRange.Text = SportsmansCurrent.State.Name;
            }

            SportApplicationPDF.Visible = true;

            document.SaveAs2(@"C:\Users\User\OneDrive\Desktop\Проказников\WpfApp2\Resourses\Files\SportsmansPDF.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }

        private void Printer_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application ExcelAppPrint = new Excel.Application();
            Excel.Workbook wb = ExcelAppPrint.Workbooks.Open($"{Directory.GetCurrentDirectory()}\\Шаблон.xlsx");
            Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets[1];
            int indexRows = 1;
            ws.Cells[2][indexRows] = "Имя";
            ws.Cells[3][indexRows] = "Дата рождения";
            ws.Cells[4][indexRows] = "Категория";
            ws.Cells[5][indexRows] = "Место";
            ws.Cells[6][indexRows] = "Ивент";
            ws.Cells[7][indexRows] = "Страна";

            var printItems = LViewSport.Items;
            foreach (Sportsman item in printItems)
            {
                ws.Cells[1][indexRows + 1] = indexRows;
                ws.Cells[2][indexRows + 1] = item.Name;
                ws.Cells[3][indexRows + 1] = item.Birth.ToString();
                ws.Cells[4][indexRows + 1] = item.Category;
                ws.Cells[5][indexRows + 1] = item.Place;
                ws.Cells[6][indexRows + 1] = item.Ivent.Name;
                ws.Cells[7][indexRows + 1] = item.State.Name;

                indexRows++;
            }
            ws.Cells[indexRows + 2, 3] = "Подпись";
            ws.Cells[indexRows + 2, 5] = "Проказников И. В.";
            ExcelAppPrint.Visible = true;
        }
    }
}
