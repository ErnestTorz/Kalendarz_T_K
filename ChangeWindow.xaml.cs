using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows;
using System.Linq;
using System.Text;
using System;

/// Główna przestrzeń nazw programu kalendarza.
namespace Kalendarz_T_K
{
    /// Klasa obsługująca wyłącznie szatę graficzną okna zmiany informacji o wydarzeniu.
    public partial class ChangeWindow : Window
    {
        /// Metoda inicjalizuje okno zmiany informacji o wydarzeniu.
        public ChangeWindow()
        {
            InitializeComponent();
        }

        /// Metoda obsługuje wydarzenie...
        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }

        /// Metoda obsługuje wydarzenie...
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

        /// Metoda obsługuje wydarzenie...
        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }
        
        /// Metoda obsługuje wydarzenie...
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

        /// Metoda obsługuje wydarzenie najechania myszką
        /// na przycisk przez użytkownika.
        /// 
        /// Po najechaniu myszką na przycisk przez
        /// użytkownika zmienia kolor przycisku
        /// na czarny, a kolor napisu na biały.
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ChangeButtonBorder.Background = Brushes.Black;
            ChangeButtonText.Foreground = Brushes.White;
        }

        /// Metoda obsługuje wydarzenie zjechania myszką
        /// z przycisku przez użytkownika.
        /// 
        /// Po zjechaniu myszką z przycisku przez
        /// użytkownika zmienia kolor przycisku
        /// na jasny szary, a kolor napisu na biały.
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeButtonBorder.Background = Brushes.LightGray;
            ChangeButtonText.Foreground = Brushes.White;
        }
    }
}