using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

/// Główna przestrzeń nazw programu kalendarza.
namespace Kalendarz_T_K
{   
    /// Klasa reprezentuje encję **Wydarzenie** w bazie danych.
    ///
    /// Przechowuje informacje takie jak ID i notatka dotycząca wydarzenia,
    /// planowana godzina rozpoczęcia i zakończenia wydarzenia oraz czy 
    /// zostało ono zakończone. Encja **Wydarzenia** jest w relacji wiele
    /// do jednego z encją **Termin** - w 1 terminie może być 0, 1 lub 
    /// więcej wydarzeń.
    public class Wydarzenie
    {
        public int ID { get; set; } ///< ID wydarzenia będące kluczem głównym encji **Wydarzenie**
        public string Notatka { get; set; } ///< Notatka dotycząca wydarzenia
        public string Godzina_start{ get; set; } ///< Godzina rozpoczęcia wydarzenia
        public string Godzina_stop { get; set; } ///< Godzina zakończenia wydarzenia
        public bool Wykonane { get; set; } ///< Informacja dotycząca tego czy wydarzenie zostało zakończone

        [ForeignKey("Termin")]
        public int TerminID { get; set; } ///< ID terminu wydarzenia będące kluczem obcym encji **Wydarzenie**
        
        public virtual Termin Termin { get; set; } ///< Obiekt klasy Termin reprezentujący powiązanie encji **Wydarzenie** z encją **Termin**
    }
}