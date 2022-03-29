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
using Newtonsoft.Json;
using System.Net;
using FontAwesome.WPF;

namespace Kalendarz_T_K
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
     
        TextBlock[] TextBlockDays = new TextBlock[42];
        Border[] TextBlockDaysBase = new Border[42];

        int year;
        int month;

        int Wybrany_dzien_jako_int;
        int Wybrany_miesiac_jako_int;
        int Wybrany_rok_jako_int;

        string Miasto = "Wrocław";

        public MainWindow()
        {
            InitializeComponent();
            InitKalendarz(4, 1);
            PobierzPogode();
           // TestBazy();


        }

        public void WyswietlWydarzenia()
        {
            IList<Termin> Terminy;
            IList<Wydarzenie> WydarzeniaWczyt;
            List<Wydarzenie> Wydarzeniapom;
            List<Wydarzenie> Wydarzenia;
            DateTime WybranyTermin = new DateTime(Wybrany_rok_jako_int, Wybrany_miesiac_jako_int, Wybrany_dzien_jako_int);
            Tablica_zdarzen.Children.Clear();
            using (var context = new KalendarContext())
            {
                Terminy = context.Terminy.ToList();
                WydarzeniaWczyt = context.Wydarzenia.ToList();
            }
           
            
           
            foreach (Termin t in Terminy)
            {
                if (t.Data.ToString("d") == WybranyTermin.ToString("d"))
                {
                    if (t.Wydarzenia.Count > 0)
                    {
                        Wydarzeniapom=t.Wydarzenia.ToList();
                        Wydarzenia = Wydarzeniapom.OrderBy(o => o.Godzina_start).ThenBy(o=>o.Godzina_stop).ToList();

                        foreach (Wydarzenie w in Wydarzenia)
                        {
                            
                            Item item = new Item();
                            item.seter(w);
                            Tablica_zdarzen.Children.Add(item);
                        }
                    }
                }
            }

        }
        private void TestBazy()
        {
            IList<Termin> Terminy;
            IList<Wydarzenie> Wydarzenia;
            using (var context = new KalendarContext())
            {
                Terminy = context.Terminy.ToList();
                Wydarzenia = context.Wydarzenia.ToList();
            }
            foreach (Termin t in Terminy)
            {
                MessageBox.Show("ID: " + t.ID.ToString() + ", " + t.Data.ToString("d"));
            }
            foreach (Wydarzenie w in Wydarzenia)
            {
                MessageBox.Show("ID: " + w.ID.ToString() + ", " + w.Notatka + ", " + w.TerminID.ToString());
            }

        }

        public void WyswietlDni()
        {
            IList<Termin> Terminy;
            IList<Wydarzenie> Wydarzenia;
            DateTime courentdate;
            using (var context = new KalendarContext())
            {
                Terminy = context.Terminy.ToList();
                Wydarzenia = context.Wydarzenia.ToList();

                AktualnieWidzianyRok.Text = year.ToString();
                DateTime startofthemonth = new DateTime(year, month, 1);

                int days = DateTime.DaysInMonth(year, month);
                int dayoftheweek = ((int)startofthemonth.DayOfWeek == 0) ? 7 : (int)startofthemonth.DayOfWeek;
                for (int i = 0; i < dayoftheweek - 1; i++)
                {
                    TextBlockDaysBase[i].Background = Brushes.Transparent;
                    (TextBlockDaysBase[i].Child as TextBlock).Text = "";
                }
                int a = 1;
                for (int i = dayoftheweek - 1; i < days + dayoftheweek - 1; i++)
                {
                    TextBlockDaysBase[i].Background = Brushes.Transparent;
                    (TextBlockDaysBase[i].Child as TextBlock).Text = a.ToString();
                    courentdate = new DateTime(year, month, a);
                    foreach (Termin t in Terminy)
                    {

                        if (t.Data.ToString("d") == courentdate.ToString("d"))
                        {
                            TextBlockDaysBase[i].Background = Brushes.LightGray;
                        }
                    }
                    a++;
                }

                for (int i = days + dayoftheweek - 1; i < 42; i++)
                {
                    TextBlockDaysBase[i].Background = Brushes.Transparent;
                    (TextBlockDaysBase[i].Child as TextBlock).Text = "";


                }
                if (year == DateTime.Now.Year && month == DateTime.Now.Month)
                {
                    TextBlockDaysBase[dayoftheweek - 2 + DateTime.Now.Day].Background = Brushes.IndianRed;
                }

            }
        }
        private void InitKalendarz(int row_offset, int col_offset)
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;

            Wybrany_dzien_jako_int = DateTime.Now.Day;
            Wybrany_miesiac_jako_int = DateTime.Now.Month;
            Wybrany_rok_jako_int = DateTime.Now.Year;

            Wybrany_dzien.Text = DateTime.Now.Day.ToString();
            Wybrany_miesiac.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year.ToString();
            Dzien_tyg.Text = DateTime.Now.ToString("dddd");

            Liczba_zadan.Text = Tablica_zdarzen.Children.Count.ToString() + " zadań ";
            Liczba_pozostalych_dni.Text = "- zostało 0 dni";

            AktualnieWidzianyRokMinus2.Text = (year - 2).ToString();
            AktualnieWidzianyRokMinus1.Text = (year - 1).ToString();
            AktualnieWidzianyRok.Text = year.ToString();
            AktualnieWidzianyRokPlus1.Text = (year + 1).ToString();
            AktualnieWidzianyRokPlus2.Text = (year + 2).ToString();

            (MonthsPanel.Children[month - 1] as Button).Foreground = Brushes.BurlyWood;
            (MonthsPanel.Children[month - 1] as Button).FontSize = 24;

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
                    TextBlockDaysBase[(Row * 7) + Col].Margin = new Thickness(45, 18, 45, 18);
                    Grid.SetRow(TextBlockDaysBase[(Row * 7) + Col], Row + row_offset);
                    Grid.SetColumn(TextBlockDaysBase[(Row * 7) + Col], Col + col_offset);

                    TextBlockDaysBase[(Row * 7) + Col].Child = TextBlockDays[(Row * 7) + Col];
                    TabDni.Children.Add(TextBlockDaysBase[(Row * 7) + Col]);

                }
            }
            WyswietlDni();
            WyswietlWydarzenia();

        }

        private void Klik_na_dzien(object sender, MouseButtonEventArgs e)
        {
            DateTime courentdate = new DateTime(year, month, Int32.Parse((sender as TextBlock).Text));
            // ((sender as TextBlock).Parent as Border).Background = Brushes.LightGray;

            Wybrany_dzien.Text = (sender as TextBlock).Text;
            Wybrany_miesiac.Text = courentdate.ToString("MMMM") + " " + courentdate.Year.ToString();
            Dzien_tyg.Text = courentdate.ToString("dddd");

            Wybrany_rok_jako_int = year;
            Wybrany_miesiac_jako_int = month;
            Wybrany_dzien_jako_int = Int32.Parse((sender as TextBlock).Text);


            WyswietlWydarzenia();

            Liczba_zadan.Text = Tablica_zdarzen.Children.Count.ToString() + " zadań ";
            if ((Math.Ceiling((courentdate - DateTime.Now).TotalDays)) <= 0)
            {
                Liczba_pozostalych_dni.Text = "- zostało 0 dni";
            }
            else if ((Math.Ceiling((courentdate - DateTime.Now).TotalDays)) == 1)
            {
                Liczba_pozostalych_dni.Text = "- został 1 dzień";
            }
            else
            {
                Liczba_pozostalych_dni.Text = "- zostało " + (Math.Ceiling((courentdate - DateTime.Now).TotalDays)).ToString() + " dni";
            }
            //MessageBox.Show("Klik na: " + (sender as TextBlock).Text + "." + month + "." + year);
        }

        private void DoPrzodu(object sender, RoutedEventArgs e)
        {
            year++;
            AktualnieWidzianyRokMinus2.Text = (year - 2).ToString();
            AktualnieWidzianyRokMinus1.Text = (year - 1).ToString();
            AktualnieWidzianyRok.Text = year.ToString();
            AktualnieWidzianyRokPlus1.Text = (year + 1).ToString();
            AktualnieWidzianyRokPlus2.Text = (year + 2).ToString();

            AktualnieWidzianyRokMinus2.Foreground = Brushes.LightGray;
            AktualnieWidzianyRokMinus1.Foreground = Brushes.LightGray;
            AktualnieWidzianyRokPlus1.Foreground = Brushes.LightGray;
            AktualnieWidzianyRokPlus2.Foreground = Brushes.LightGray;

            if (year != DateTime.Now.Year)
            {
                if (year - DateTime.Now.Year <= 2 && year - DateTime.Now.Year > 0)
                {
                    (yearPanel.Children[3 - (year - DateTime.Now.Year)] as TextBlock).Foreground = Brushes.IndianRed;
                }

                if (year - DateTime.Now.Year >= -2 && year - DateTime.Now.Year < 0)
                {
                    (yearPanel.Children[3 - (year - DateTime.Now.Year)] as TextBlock).Foreground = Brushes.IndianRed;
                }
            }

            m1.Foreground = Brushes.LightGray; m1.FontSize = 20;
            m2.Foreground = Brushes.LightGray; m2.FontSize = 20;
            m3.Foreground = Brushes.LightGray; m3.FontSize = 20;
            m4.Foreground = Brushes.LightGray; m4.FontSize = 20;
            m5.Foreground = Brushes.LightGray; m5.FontSize = 20;
            m6.Foreground = Brushes.LightGray; m6.FontSize = 20;
            m7.Foreground = Brushes.LightGray; m7.FontSize = 20;
            m8.Foreground = Brushes.LightGray; m8.FontSize = 20;
            m9.Foreground = Brushes.LightGray; m9.FontSize = 20;
            m10.Foreground = Brushes.LightGray; m10.FontSize = 20;
            m11.Foreground = Brushes.LightGray; m11.FontSize = 20;
            m12.Foreground = Brushes.LightGray; m12.FontSize = 20;

            (MonthsPanel.Children[month - 1] as Button).FontSize = 24;
            (MonthsPanel.Children[month - 1] as Button).Foreground = Brushes.BurlyWood;


            if (DateTime.Now.Year == year && month != DateTime.Now.Month)
            {
                (MonthsPanel.Children[DateTime.Now.Month - 1] as Button).Foreground = Brushes.IndianRed;
            }



            WyswietlDni();


        }

        private void DoTylu(object sender, RoutedEventArgs e)
        {
            year--;
            AktualnieWidzianyRokMinus2.Text = (year - 2).ToString();
            AktualnieWidzianyRokMinus1.Text = (year - 1).ToString();
            AktualnieWidzianyRok.Text = year.ToString();
            AktualnieWidzianyRokPlus1.Text = (year + 1).ToString();
            AktualnieWidzianyRokPlus2.Text = (year + 2).ToString();

            AktualnieWidzianyRokMinus2.Foreground = Brushes.LightGray;
            AktualnieWidzianyRokMinus1.Foreground = Brushes.LightGray;
            AktualnieWidzianyRokPlus1.Foreground = Brushes.LightGray;
            AktualnieWidzianyRokPlus2.Foreground = Brushes.LightGray;

            if (year != DateTime.Now.Year)
            {
                if (year - DateTime.Now.Year <= 2 && year - DateTime.Now.Year > 0)
                {
                    (yearPanel.Children[3 - (year - DateTime.Now.Year)] as TextBlock).Foreground = Brushes.IndianRed;
                }

                if (year - DateTime.Now.Year >= -2 && year - DateTime.Now.Year < 0)
                {
                    (yearPanel.Children[3 - (year - DateTime.Now.Year)] as TextBlock).Foreground = Brushes.IndianRed;
                }
            }

            m1.Foreground = Brushes.LightGray; m1.FontSize = 20;
            m2.Foreground = Brushes.LightGray; m2.FontSize = 20;
            m3.Foreground = Brushes.LightGray; m3.FontSize = 20;
            m4.Foreground = Brushes.LightGray; m4.FontSize = 20;
            m5.Foreground = Brushes.LightGray; m5.FontSize = 20;
            m6.Foreground = Brushes.LightGray; m6.FontSize = 20;
            m7.Foreground = Brushes.LightGray; m7.FontSize = 20;
            m8.Foreground = Brushes.LightGray; m8.FontSize = 20;
            m9.Foreground = Brushes.LightGray; m9.FontSize = 20;
            m10.Foreground = Brushes.LightGray; m10.FontSize = 20;
            m11.Foreground = Brushes.LightGray; m11.FontSize = 20;
            m12.Foreground = Brushes.LightGray; m12.FontSize = 20;

            (MonthsPanel.Children[month - 1] as Button).FontSize = 24;
            (MonthsPanel.Children[month - 1] as Button).Foreground = Brushes.BurlyWood;


            if (DateTime.Now.Year == year && month != DateTime.Now.Month)
            {
                (MonthsPanel.Children[DateTime.Now.Month - 1] as Button).Foreground = Brushes.IndianRed;
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
            Add_event.Foreground = Brushes.White;
        }

        private void Add_Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DateTime courentdate = new DateTime(Wybrany_rok_jako_int, Wybrany_miesiac_jako_int, Wybrany_dzien_jako_int);
            IList<Termin> Terminy;
            char[] separator = { ' ', '-' };
            int pomID = -1;
            DateTime result1;
            DateTime result2;
            string[] strlist = txtTime.Text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < strlist.Length; i++)
            {
                strlist[i] = strlist[i].Replace(" ", String.Empty);
            }


            if (strlist.Count() == 2 && (txtNote.Text.Length != 0) &&txtNote.Text.Count(char.IsWhiteSpace)!=txtNote.Text.Length && DateTime.TryParse(strlist[0], out result1) && DateTime.TryParse(strlist[1], out result2))
            {
                if (result2 >= result1 && result1.ToString("t") == strlist[0] && result2.ToString("t") == strlist[1])
                {
                    
                        using (var context = new KalendarContext())
                        {
                            Terminy = context.Terminy.ToList();
                        }
                        foreach (var ter in Terminy)
                        {
                            if (ter.Data == courentdate)
                            {
                                pomID = ter.ID;
                            }
                        }
                        if (pomID == -1)
                        {
                            Termin term = new Termin { Data = courentdate };
                            using (var context = new KalendarContext())
                            {
                                context.Terminy.Add(term);
                                context.SaveChanges();
                                pomID = term.ID;
                            }
                        }

                        Wydarzenie wydarzenie = new Wydarzenie { Notatka = txtNote.Text, Godzina_start = strlist[0], Godzina_stop = strlist[1], TerminID = pomID, Wykonane = false };
                        using (var context = new KalendarContext())
                        {
                            context.Wydarzenia.Add(wydarzenie);
                            context.SaveChanges();

                        }
                        WyswietlWydarzenia();
                        WyswietlDni();
                        Liczba_zadan.Text = Tablica_zdarzen.Children.Count.ToString() + " zadań ";
                        txtNote.Text = "";
                        txtTime.Text = "";
                  
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe dane");
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane");
            }
        }

        private void MonthSelectClick(object sender, RoutedEventArgs e)
        {
            m1.Foreground = Brushes.LightGray; m1.FontSize = 20;
            m2.Foreground = Brushes.LightGray; m2.FontSize = 20;
            m3.Foreground = Brushes.LightGray; m3.FontSize = 20;
            m4.Foreground = Brushes.LightGray; m4.FontSize = 20;
            m5.Foreground = Brushes.LightGray; m5.FontSize = 20;
            m6.Foreground = Brushes.LightGray; m6.FontSize = 20;
            m7.Foreground = Brushes.LightGray; m7.FontSize = 20;
            m8.Foreground = Brushes.LightGray; m8.FontSize = 20;
            m9.Foreground = Brushes.LightGray; m9.FontSize = 20;
            m10.Foreground = Brushes.LightGray; m10.FontSize = 20;
            m11.Foreground = Brushes.LightGray; m11.FontSize = 20;
            m12.Foreground = Brushes.LightGray; m12.FontSize = 20;

            (sender as Button).FontSize = 24;
            (sender as Button).Foreground = Brushes.BurlyWood;
            month = Int32.Parse((sender as Button).Content.ToString());

            if (DateTime.Now.Year == year && month != DateTime.Now.Month)
            {
                (MonthsPanel.Children[DateTime.Now.Month - 1] as Button).Foreground = Brushes.IndianRed;
            }

            WyswietlDni();
        }

        void PobierzPogode() {

            string APIKey = "b7df7f386e5b02eb65554d11cc1fdfb5";
            
            

            using (WebClient web= new WebClient())
            {
                string url = string.Format( "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&lang=pl",Miasto,APIKey);
                try
                {
                    var json = web.DownloadString(url);
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                    var path = "https://api.openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path, UriKind.Absolute);
                    bitmap.EndInit();
                    //ObrazekPogody.Source = "https://api.openweathermap.org/img/w/04d.png";
                    ObrazekPogody.Source = bitmap;
                    MiastoTXT.Text = Miasto;
                    Temperatura.Text = Info.main.temp.ToString() + " °C";
                    OpisPogody.Text = Info.weather[0].description;
                    KrajTXT.Text = Info.sys.country;
                    TemperaturaMIN.Text = "Temp min: " + Info.main.temp_min.ToString() + "°C";
                    TemperaturaMAX.Text = "Temp max: " + Info.main.temp_max.ToString() + " °C";
                }
                catch (Exception ex) { MessageBox.Show("Error: "+ex.Message.ToString()+"\nBrak połączenia z API. Sprawdz ustawienia sieciowe i zresetuj aplikację\nlub korzystaj z aplikacji bez informacji o pogodzie"); }


            }
        
        }

        private void RefesrshButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string APIKey = "b7df7f386e5b02eb65554d11cc1fdfb5";
            


            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&lang=pl", Miasto, APIKey);
                try
                {
                    var json = web.DownloadString(url);
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                    var path = "https://api.openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path, UriKind.Absolute);
                    bitmap.EndInit();
                    //ObrazekPogody.Source = "https://api.openweathermap.org/img/w/04d.png";
                    ObrazekPogody.Source = bitmap;
                    MiastoTXT.Text = Miasto;
                    Temperatura.Text = Info.main.temp.ToString() + " °C";
                    OpisPogody.Text = Info.weather[0].description;
                    KrajTXT.Text = Info.sys.country;
                    TemperaturaMIN.Text = "Temp min: " + Info.main.temp_min.ToString() + "°C";
                    TemperaturaMAX.Text = "Temp max: " + Info.main.temp_max.ToString() + " °C";
                    MessageBox.Show("Odświeżono pogodę");
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message.ToString() + "\nBrak połączenia z API. Sprawdz ustawienia sieciowe i zresetuj aplikację\nlub korzystaj z aplikacji bez informacji o pogodzie"); }


            }

        }

        private void RefesrshButton_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as ImageAwesome).Foreground=Brushes.Black ;
        }

        private void RefesrshButton_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as ImageAwesome).Foreground = Brushes.White;
        }

        private void MiastoTXT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CityChange changewin = new CityChange();
            changewin.ChangeButton.MouseDoubleClick += ChangeButton_MouseDoubleClick; ;
            changewin.Owner = Window.GetWindow(this);

            changewin.ShowDialog();
        }

        private void ChangeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string APIKey = "b7df7f386e5b02eb65554d11cc1fdfb5";


            CityChange thiswindow = null;
           


            //Odnajdywanie okna 
            foreach (Window window in Application.Current.Windows.OfType<CityChange>())
            {
                thiswindow = ((CityChange)window);
            }

            string Miasto = thiswindow.txtCity.Text;


            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&lang=pl", Miasto, APIKey);
                try
                {
                    var json = web.DownloadString(url);
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                    var path = "https://api.openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(path, UriKind.Absolute);
                    bitmap.EndInit();
                    //ObrazekPogody.Source = "https://api.openweathermap.org/img/w/04d.png";
                    ObrazekPogody.Source = bitmap;
                    
                    Temperatura.Text = Info.main.temp.ToString() + " °C";
                    OpisPogody.Text = Info.weather[0].description;
                    KrajTXT.Text = Info.sys.country;
                    TemperaturaMIN.Text = "Temp min: " + Info.main.temp_min.ToString() + "°C";
                    TemperaturaMAX.Text = "Temp max: " + Info.main.temp_max.ToString() + " °C";
                    MiastoTXT.Text = Miasto;
                    thiswindow.Close();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message.ToString() + "\nBrak połączenia z API. Sprawdz ustawienia sieciowe.\n Upewnij się, że wprowadzane miasto jest prawidłowe \nlub korzystaj z aplikacji bez informacji o pogodzie."); }


            }
        }

        private void MiastoTXT_MouseEnter(object sender, MouseEventArgs e)
        {
            MiastoTXT.Foreground = Brushes.Black;
        }

        private void MiastoTXT_MouseLeave(object sender, MouseEventArgs e)
        {
            MiastoTXT.Foreground = Brushes.White;
        }
    }
}
