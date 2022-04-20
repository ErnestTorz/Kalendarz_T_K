using System.Windows.Controls;
using System.Windows;

/// Przestrzeń nazw kontrolek programu kalendarza.
namespace Kalendarz_T_K.UserControls
{
    /// Logika interakcji dla klasy MenuButton.xaml
    public partial class MenuButton : UserControl
    {   
        /// Metoda inicjalizuje przycisk menu
        public MenuButton()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.
            Register("Caption",typeof(string),typeof(MenuButton));

        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.
            Register("Icon", typeof(FontAwesome.WPF.FontAwesomeIcon), typeof(MenuButton));
    }
}