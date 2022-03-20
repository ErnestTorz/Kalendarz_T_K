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
using Kalendarz_T_K;

namespace Kalendarz_T_K
{
    public class Event
    {
        private Item item = new Item();
        public int day;
        public int month;
        public int year;
        public Event(string Title, string start_time, string end_time,int Day,int Month,int Year, bool check)
        {
            item.Title = Title;
            item.Color = Brushes.White;
            item.Time = start_time + " - " + end_time;
            day = Day;
            month = Month;
            year = Year;
            if (check == true)
            {
                item.Icon = FontAwesome.WPF.FontAwesomeIcon.CheckCircle;
            }
            else
            {
                item.Icon = FontAwesome.WPF.FontAwesomeIcon.CircleThin;
            }

            item.IconBell = FontAwesome.WPF.FontAwesomeIcon.ClockOutline;
            item.Color = Brushes.White;
        }
        public Item Item {
            get { return item; }
            set { item = value;} 
        }
        
            

       

    }
}
