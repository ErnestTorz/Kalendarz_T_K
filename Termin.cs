using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendarz_T_K
{   
    /// Klasa Terminu przeznaczona do bazy danych
    /// 
    /// W niej znajdują się informacje o: ID oraz Dacie
    /// Klasa połączona jest realacją jeden do wielu i może posiadać wiele zdarzeń
    public class Termin
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }

        public virtual ICollection<Wydarzenie> Wydarzenia { get; set; }
    }
}
