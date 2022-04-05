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
using System.Windows.Shapes;

namespace Kalendarz_T_K
{
    /// Klasa służąca do obsługi tylko i wyłącznie szaty graficznie okna zmiany informacji o wydarzeniu
    public partial class ChangeWindow : Window
    {
        public ChangeWindow()
        {
            InitializeComponent();
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
            ChangeButtonBorder.Background = Brushes.Black;
            ChangeButtonText.Foreground = Brushes.White;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeButtonBorder.Background = Brushes.LightGray;
            ChangeButtonText.Foreground = Brushes.White;
        }


    }


}
