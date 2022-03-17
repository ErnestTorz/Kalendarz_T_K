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

namespace Kalendarz_T_K
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        TextBlock[] TextBlockDays = new TextBlock[42];
        Border [] TextBlockDaysBase = new Border[42];

        int year ;
        int month;
        int Wybrany_miesiac_jako_int;
        public MainWindow()
        {
            InitializeComponent();

            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            Wybrany_miesiac_jako_int = DateTime.Now.Month;


            Wybrany_dzien.Text = DateTime.Now.Day.ToString();
            Wybrany_miesiac.Text = DateTime.Now.ToString("MMMM");
            
           
            for (int Row = 0; Row < 6; Row++)
            {
                for (int Col = 0; Col < 7; Col++)
                {
                    TextBlockDaysBase[(Row * 7) + Col] = new Border();
                    TextBlockDays[(Row * 7) + Col] = new TextBlock();

                    TextBlockDays[(Row * 7) + Col].FontSize = 23;
                    TextBlockDays[(Row * 7) + Col].Text = "";
                    TextBlockDays[(Row * 7) + Col].VerticalAlignment = VerticalAlignment.Center;
                    TextBlockDays[(Row * 7) + Col].HorizontalAlignment = HorizontalAlignment.Center;
                    TextBlockDays[(Row * 7) + Col].Background = Brushes.Transparent;
                    TextBlockDays[(Row * 7) + Col].MouseDown += Klik_na_dzien;

                    TextBlockDaysBase[(Row * 7) + Col].Background = Brushes.Transparent;
                    TextBlockDaysBase[(Row * 7) + Col].CornerRadius = new CornerRadius(360);
                    TextBlockDaysBase[(Row * 7) + Col].Margin = new Thickness(40,20,40,20);
                    Grid.SetRow(TextBlockDaysBase[(Row * 7) + Col], Row + 3);
                    Grid.SetColumn(TextBlockDaysBase[(Row * 7) + Col], Col + 1);

                    TextBlockDaysBase[(Row * 7) + Col].Child = TextBlockDays[(Row * 7) + Col];
                    TabDni.Children.Add(TextBlockDaysBase[(Row * 7) + Col]);
                                     
                }
            }
            WyswietlDni();
        }

       private void Klik_na_dzien(object sender, MouseButtonEventArgs e)
       {
            DateTime courentdate = new DateTime(year, month, Int32.Parse((sender as TextBlock).Text));
            ((sender as TextBlock).Parent as Border).Background = Brushes.LightGreen;
            //Wybrany_dzien.Text = (sender as TextBlock).Text + "." + month + "." + year;
            Wybrany_dzien.Text = (sender as TextBlock).Text;
            Wybrany_miesiac.Text = courentdate.ToString("MMMM");
            Wybrany_miesiac_jako_int = courentdate.Month;
            MessageBox.Show("Klik na: " + (sender as TextBlock).Text + "." + month + "." + year);
        }

        private void DoPrzodu(object sender, RoutedEventArgs e)
        {
            if (month < 12)
            {
                month++;
            }
            else { 
                month = month-11;
                year++;
            }
            WyswietlDni();

        }

        private void DoTylu(object sender, RoutedEventArgs e)
        {
            if (month >1)
            {
                month--;
            }
            else
            {
                month = month + 11;
                year--;
            }
            WyswietlDni();
        }

        private void WyswietlDni()
        {
            
            Miesiac_i_rok.Text = month.ToString()+"."+year.ToString();
            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayoftheweek = ((int)startofthemonth.DayOfWeek == 0) ? 7 : (int)startofthemonth.DayOfWeek;
            for (int i =0 ; i < dayoftheweek-1; i++)
            {
                TextBlockDaysBase[i].Background = Brushes.Transparent;
                (TextBlockDaysBase[i].Child as TextBlock).Text = "" ;
            }
            int a = 1;
            for (int i = dayoftheweek-1; i <= days+dayoftheweek - 1; i++)
            {
                TextBlockDaysBase[i].Background = Brushes.Transparent;
                (TextBlockDaysBase[i].Child as TextBlock).Text = a.ToString() ;
                a++;
            }
            for (int i = days + dayoftheweek - 1; i < 42; i++)
            {
                TextBlockDaysBase[i].Background = Brushes.Transparent;
                (TextBlockDaysBase[i].Child as TextBlock).Text ="";
                

            }
            if(year== DateTime.Now.Year && month == DateTime.Now.Month)
            {
                TextBlockDaysBase[dayoftheweek - 2 + DateTime.Now.Day].Background = Brushes.OrangeRed;
            }
          

        }
    }
}
