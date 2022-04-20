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

        /// Metoda obsługuje wydarzenie najechania myszką
        /// na pole tekstowe przez użytkownika.
        /// 
        /// Po najechaniu myszką na pole teskstowe przez
        /// użytkownika insterfejs na pierwszy plan wydobywa Text box, z pod warstwy graficznej
        private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }


        /// Metoda obsługuje wydarzenie zmiany tekstu
        /// w polu tekstowym
        /// 
        /// Po zmianie tekstu przez używtkonika, tekst pomocniczy zanika gdy Text box 
        /// nie jest pusty, w innym przypadku teskst pomocniczy jest widoczny
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

        /// Metoda obsługuje wydarzenie najechania myszką
        /// na pole tekstowe przez użytkownika.
        /// 
        /// Po najechaniu myszką na pole teskstowe przez
        /// użytkownika insterfejs na pierwszy plan wydobywa Text box, z pod warstwy graficznej
        private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }


        /// Metoda obsługuje wydarzenie zmiany tekstu
        /// w polu tekstowym
        /// 
        /// Po zmianie tekstu przez używtkonika, tekst pomocniczy zanika gdy Text box 
        /// nie jest pusty, w innym przypadku teskst pomocniczy jest widoczny
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