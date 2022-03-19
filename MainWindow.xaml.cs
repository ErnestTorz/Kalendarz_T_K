using Kalendarz_T_K.UserControls;
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

        int Wybrany_dzien_jako_int;
        int Wybrany_miesiac_jako_int;
        int Wybrany_rok_jako_int;

        public MainWindow()
        {
            InitializeComponent();
            InitKalendarz(4,1);
            WyswietlDni();

            //Test//
            Item item = new Item();
            item.Title = "Korepetycje";
            item.Color = Brushes.White;
            item.Time = "18:00-19:00";
            item.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleThin;
            item.IconBell = FontAwesome.WPF.FontAwesomeIcon.ClockOutline;
            Tablica_zdarzen.Children.Add(item);
            //Koniec Testu
            



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
        private void InitKalendarz(int row_offset, int col_offset)
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;

            Wybrany_dzien_jako_int = DateTime.Now.Day;
            Wybrany_miesiac_jako_int = DateTime.Now.Month;
            Wybrany_rok_jako_int= DateTime.Now.Year;

            Wybrany_dzien.Text = DateTime.Now.Day.ToString();
            Wybrany_miesiac.Text = DateTime.Now.ToString("MMMM");
            Dzien_tyg.Text= DateTime.Now.ToString("dddd");
           
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
                    TextBlockDaysBase[(Row * 7) + Col].Margin = new Thickness(40, 20, 40, 20);
                    Grid.SetRow(TextBlockDaysBase[(Row * 7) + Col], Row + row_offset);
                    Grid.SetColumn(TextBlockDaysBase[(Row * 7) + Col], Col + col_offset);

                    TextBlockDaysBase[(Row * 7) + Col].Child = TextBlockDays[(Row * 7) + Col];
                    TabDni.Children.Add(TextBlockDaysBase[(Row * 7) + Col]);

                }
            }

        }

        private void Klik_na_dzien(object sender, MouseButtonEventArgs e)
        {
            DateTime courentdate = new DateTime(year, month, Int32.Parse((sender as TextBlock).Text));
            ((sender as TextBlock).Parent as Border).Background = Brushes.LightGreen;

            Wybrany_dzien.Text = (sender as TextBlock).Text;
            Wybrany_miesiac.Text = courentdate.ToString("MMMM");
            Dzien_tyg.Text = courentdate.ToString("dddd");

            Wybrany_rok_jako_int = year;
            Wybrany_miesiac_jako_int = month;
            Wybrany_dzien_jako_int = Int32.Parse((sender as TextBlock).Text);

            MessageBox.Show("Klik na: " + (sender as TextBlock).Text + "." + month + "." + year);
        }

        private void DoPrzodu(object sender, RoutedEventArgs e)
        {
            if (month < 12)
            {
                month++;
            }
            else
            {
                month = month - 11;
                year++;
            }
            WyswietlDni();

        }

        private void DoTylu(object sender, RoutedEventArgs e)
        {
            if (month > 1)
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

        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }

        private void txtNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtNote.Text)) && txtNote.Text.Length > 0)
            {
                lblNote.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblNote.Visibility = Visibility.Visible;
            }
        }

        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }

        private void txtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(string.IsNullOrEmpty(txtTime.Text)) && txtTime.Text.Length > 0)
            {
                lblTime.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                lblTime.Visibility = Visibility.Visible;
            
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Add_event.Foreground = Brushes.Black;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Add_event.Foreground = Brushes.Gray;
        }

        private void Add_Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(txtNote.Text +" "+ txtTime.Text);
        }
    }
}
