
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
using Kalendarz_T_K;

namespace Kalendarz_T_K.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        public Item()
        {
            InitializeComponent();
            
        }
        public int ID { get; set; }
        
        public void seter(Wydarzenie wyd)
        { 
            this.ID=wyd.ID;
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
        
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Item));


        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(string), typeof(Item));

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(Item));

        public FontAwesome.WPF.FontAwesomeIcon IconBell
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconBellProperty); }
            set { SetValue(IconBellProperty, value); }
        }
        public static readonly DependencyProperty IconBellProperty = DependencyProperty.Register("IconBell", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));

        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(Item));

        private void MenuButton_MouseDoubleClick_Trash(object sender, MouseButtonEventArgs e)
        {

            MainWindow thiswindow = null;
            IList<Wydarzenie> wydarzenia;
            IList<Termin> Terminy;
            Termin pom;
            bool usuwac = false;
            //Odnajdywanie okna 
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

        private void MenuButton_MouseDoubleClick_Edit(object sender, MouseButtonEventArgs e)

        {
            ChangeWindow changewin = new ChangeWindow();
            changewin.ChangeButton.MouseDoubleClick += ChangeButton_MouseDoubleClick;
            changewin.Owner =Window.GetWindow(this);
            changewin.lblNote.Text = item.Title;
            changewin.lblTime.Text = item.Time;
            changewin.ShowDialog();

        }

        private void ChangeButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IList<Wydarzenie> wydarzenia;
            ChangeWindow thiswindow = null;
            MainWindow mainwindow = null;


            //Odnajdywanie okna 
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
                               // this.Title = thiswindow.txtNote.Text;
                                //this.Time = thiswindow.txtTime.Text;
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
                               // this.Time = thiswindow.txtTime.Text;
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
