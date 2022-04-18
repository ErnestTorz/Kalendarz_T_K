using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

/// Główna przestrzeń nazw programu kalendarza.
namespace Kalendarz_T_K
{   
    /// Klasa reprezentuje encję **Termin** w bazie danych.
    /// 
    /// Przechowuje informacje takie jak ID terminu i data. 
    /// Encja **Termin** jest w relacji jeden do wielu z 
    /// encją **Wydarzenie** - w 1 terminie może być 0, 1 
    /// lub więcej wydarzeń.
    public class Termin
    {
        public int ID { get; set; } ///< ID terminu będące kluczem głównym encji **Termin**
        public DateTime Data { get; set; } ///< Data

        public virtual ICollection<Wydarzenie> Wydarzenia { get; set; } ///< Kolekcja obiektów klasy Wydarzenie reprezentująca powiązanie encji **Termin** z encją **Wydarzenie**
    }
}