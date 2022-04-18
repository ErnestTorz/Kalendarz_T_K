using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Navigation;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows;
using Kalendarz_T_K;
using System.Linq;
using System.Text;
using System;

/// Przestrzeń nazw kontrolek programu kalendarza.
namespace Kalendarz_T_K.UserControls
{
    /// Klasa wyświetla wydarzenia z bazy danych oraz 
    /// obsługuje ich dodawanie, usuwanie i edytowanie.
    public partial class Item : UserControl
    {
        /// Metoda inicjalizuje...
        public Item()
        {
            InitializeComponent();
        }

        public int ID { get; set; } ///< ID...

        /// Metoda pobiera informacje z klasy wydarzeń 
        /// w celu ich graficznej prezentacji. 
        /// 
        /// @param wyd Wydarzenie
        public void seter(Wydarzenie wyd)
        { 
            this.ID = wyd.ID;
            this.Title = wyd.Notatka;
            this.Color = Brushes.White;
            this.Time = wyd.Godzina_start + " - " + wyd.Godzina_stop;
           
            if (wyd.Wykonane == true)
            {
                this.Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;
            }
            else
            {
                this.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleThin;
            }

            this.IconBell = FontAwesome.WPF.FontAwesomeIcon.ClockOutline;
            this.Color = Brushes.White;
        }

        /// Metoda daje możliwość ustawienia
        /// tytułu w pliku .xaml.
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Item));

        /// Metoda daje możliwość ustawienia
        /// czasu w pliku .xaml.
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(Item));
        
        /// Metoda daje możliwość ustawienia
        /// koloru w pliku .xaml.
        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(Item));
        
        /// Metoda daje możliwość ustawienia
        /// ikony w pliku .xaml.
        public FontAwesome.WPF.FontAwesomeIcon IconBell
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconBellProperty); }
            set { SetValue(IconBellProperty, value); }
        }
        
        public static readonly DependencyProperty IconBellProperty = DependencyProperty.Register("IconBell", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));
        
        /// Metoda daje możliwość ustawienia
        /// ikony w pliku .xaml.
        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));
        
        /// Metoda obsługuje wydarzenie kliknięcia
        /// na ikonę usuwania przez użytkownika.
        /// 
        /// Wyszukuje dane z bazy danych przy pomocy
        /// ID. Jeśli znajdzie dane, to odpowiednie
        /// wydarzenie jest usuwane. Jeśli znalezione
        /// wydarzenie jest jedynym w tym terminie, to
        /// dane związane z terminem też są usuwane.
        private void MenuButton_MouseDoubleClick_Trash(object sender, MouseButtonEventArgs e)
        {
            MainWindow thiswindow = null;
            IList<Wydarzenie> wydarzenia;
            IList<Termin> Terminy;
            Termin pom;
            bool usuwac = false;

            // Znajdywanie okna 
            foreach (Window window in Application.Current.Windows.OfType<MainWindow>())
            {
                thiswindow = ((MainWindow)window);
            }
            using (var context = new KalendarContext())
            {
                wydarzenia = context.Wydarzenia.ToList();
                Terminy =context.Terminy.ToList();
                foreach (var wyd in wydarzenia)
                {
                    if (wyd.ID == this.ID)
                    {
                        pom = wyd.Termin;
                        if (wyd.Termin.Wydarzenia.Count <= 1)
                        {
                            usuwac = true;
                        }
                        else { usuwac = false; }
                        context.Wydarzenia.Remove(wyd);
                        context.SaveChanges();
                        if (usuwac == true)
                        {
                            context.Terminy.Remove(pom);
                            context.SaveChanges();
                            thiswindow.WyswietlDni();
                        }
                    }
                 }
            }

            StackPanel parent = this.Parent as StackPanel;
            parent.Children.Remove(this);
            thiswindow.Liczba_zadan.Text = thiswindow.Tablica_zdarzen.Children.Count.ToString() + " zadań ";
        }

        /// Metoda obsługuje wydarzenie zaznaczenia
        /// danego wydarzenia jako wykonanego/nie
        /// wykonanego przez użytkownika.
        /// 
        /// Jeśli użytkownik zaznaczył wydarzenie jako 
        /// wykonane/nie wykonane, to dane w bazie
        /// danych zostają zaktualnizowane.
        private void MenuButton_MouseDoubleClick_Check(object sender, MouseButtonEventArgs e)
        {
            IList<Wydarzenie> wydarzenia;

            using (var context = new KalendarContext())
            {
                wydarzenia = context.Wydarzenia.ToList();

                foreach (var wyd in wydarzenia)
                {
                    if (wyd.ID == this.ID)
                    {
                        if (wyd.Wykonane == false)
                        {
                            this.Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;
                            context.Entry(wyd).Entity.Wykonane = true;  
                        }
                        else
                        {
                            this.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleThin;
                            context.Entry(wyd).Entity.Wykonane = false;
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        /// Metoda obsługuje wydarzenie edycji
        /// wydarzenia przez użytkownika.
        /// 
        /// Wyświetla okno edycji wydarzenia.
        /// Zapewnia obsługę podwójnego kliknięcia.
        private void MenuButton_MouseDoubleClick_Edit(object sender, MouseButtonEventArgs e)
        {
            ChangeWindow changewin = new ChangeWindow();
            changewin.ChangeButton.MouseDoubleClick += ChangeButton_MouseDoubleClick;
            changewin.Owner =Window.GetWindow(this);
            changewin.lblNote.Text = item.Title;
            changewin.lblTime.Text = item.Time;
            changewin.ShowDialog();
        }

        /// Metoda obsługuje edycję wydarzenia
        /// przez użytkownika. Obsługuje wydarzenie
        /// kliknięcia na przycisk do edycji danych
        /// w oknie edycji danych wydarzenia przez 
        /// użytkownika.
        /// 
        /// Jeśli wprowadzone dane są prawidłowe, to 
        /// szata graficzna jest odświeżona i 
        /// odpowiednie dane, wybrane na podstawie ID,
        /// są zmienione w bazie danych.
        private void ChangeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IList<Wydarzenie> wydarzenia;
            ChangeWindow thiswindow = null;
            MainWindow mainwindow = null;

            // Znajdywanie okna 
            foreach (Window window in Application.Current.Windows.OfType<ChangeWindow>())
            {
                thiswindow = ((ChangeWindow)window);
            }
            foreach (Window window in Application.Current.Windows.OfType<MainWindow>())
            {
                mainwindow = ((MainWindow)window);
            }

            char[] separator = { ' ', '-' };
            string[] strlist = thiswindow.txtTime.Text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
            
            for (int i = 0; i < strlist.Length; i++)
            {
                strlist[i] = strlist[i].Replace(" ", String.Empty);
            }

            DateTime result1;
            DateTime result2;

            using (var context = new KalendarContext())
            {   wydarzenia = context.Wydarzenia.ToList();
                foreach (var wyd in wydarzenia)
                {
                    if (wyd.ID == this.ID)
                    {
                        if (strlist.Count() == 2 && thiswindow.txtNote.Text.Length != 0 && thiswindow.txtNote.Text.Count(char.IsWhiteSpace) != thiswindow.txtNote.Text.Length && DateTime.TryParse(strlist[0], out result1) && DateTime.TryParse(strlist[1], out result2))
                        {
                            if (result2 >= result1 && result1.ToString("t") == strlist[0] && result2.ToString("t") == strlist[1])
                            {
                                context.Entry(wyd).Entity.Notatka = thiswindow.txtNote.Text;
                                context.Entry(wyd).Entity.Godzina_start = strlist[0];
                                context.Entry(wyd).Entity.Godzina_stop = strlist[1];
                                thiswindow.Close();
                                context.SaveChanges();
                                mainwindow.WyswietlWydarzenia();
                            }
                            else
                            {
                                MessageBox.Show("Nieprawidłowe dane przkazane do zmiany");
                            }                          
                        }
                        else if (thiswindow.txtNote.Text.Length != 0 && thiswindow.txtNote.Text.Count(char.IsWhiteSpace) != thiswindow.txtNote.Text.Length && thiswindow.txtTime.Text.Length == 0)
                        {
                            this.Title = thiswindow.txtNote.Text;
                            context.Entry(wyd).Entity.Notatka = thiswindow.txtNote.Text;
                            thiswindow.Close();
                            context.SaveChanges();
                            mainwindow.WyswietlWydarzenia();
                        }
                        else if (thiswindow.txtNote.Text.Length==0 && strlist.Count() == 2 && DateTime.TryParse(strlist[0], out result1) && DateTime.TryParse(strlist[1], out result2))
                        {
                            if (result2 >= result1 && result1.ToString("t") == strlist[0] && result2.ToString("t") == strlist[1])
                            {
                                context.Entry(wyd).Entity.Godzina_start = strlist[0];
                                context.Entry(wyd).Entity.Godzina_stop = strlist[1];
                                thiswindow.Close();
                                context.SaveChanges();
                                mainwindow.WyswietlWydarzenia();
                            }
                            else
                            {
                                MessageBox.Show("Nieprawidłowe dane przkazane do zmiany");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nieprawidłowe dane przkazane do zmiany");
                        }                       
                    }
                }
            }
        }
    }
}