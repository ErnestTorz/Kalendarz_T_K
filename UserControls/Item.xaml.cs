
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
            //Odnajdywanie okna 
            foreach (Window window in Application.Current.Windows.OfType<MainWindow>())
            {
                thiswindow = ((MainWindow)window);
            }
            StackPanel parent = this.Parent as StackPanel;
            parent.Children.Remove(this);
            thiswindow.Liczba_zadan.Text = thiswindow.Tablica_zdarzen.Children.Count.ToString() + " zadań ";
        }

        private void MenuButton_MouseDoubleClick_Check(object sender, MouseButtonEventArgs e)
        {
            StackPanel parent = this.Parent as StackPanel;
            if (this.Icon == FontAwesome.WPF.FontAwesomeIcon.CircleThin)
            {
                this.Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;
            }
            else
            {
                this.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleThin;
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
            ChangeWindow thiswindow = null;
            //Odnajdywanie okna 
            foreach (Window window in Application.Current.Windows.OfType<ChangeWindow>())
            {
                thiswindow = ((ChangeWindow)window);
            }

            char[] separator = { ' ', '-' };
            string[] strlist = thiswindow.txtTime.Text.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
            if (strlist.Count() == 2 && thiswindow.txtNote.Text.Length != 0)
            {
                item.Title = thiswindow.txtNote.Text;
                item.Time = thiswindow.txtTime.Text;
                thiswindow.Close();
            }
            else if (thiswindow.txtNote.Text.Length != 0)
            {
                item.Title = thiswindow.txtNote.Text;
                thiswindow.Close();
            }
            else if (strlist.Count() == 2)
            {
                item.Time = thiswindow.txtTime.Text;
                thiswindow.Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowe dane przkazane do zmiany");
            }

        }
    }
}
