using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendarz_T_K
{   /// Klasa do bazy danych przechowująca informacje o wydarzeniu
    ///
    /// Klasa przechowuje takie informacje jak: ID, Notatke, Godzinę rozpoczęcia wydarzneia, godzine zkończenia wydarzenia, oraz informacje czy zadanie zostało wyonane
    /// Klasa mołączona jest również relacją z Terminem i może go posiadać tylko w jednym wydaniu
    public class Wydarzenie
    {
        public int ID { get; set; }

        public string Notatka { get; set; }

        public string Godzina_start{ get; set; }

        public string Godzina_stop { get; set; }

        public bool Wykonane { get; set; }

        [ForeignKey("Termin")]
        public int TerminID { get; set; }
        
        public virtual Termin Termin { get; set; }
    }
}
