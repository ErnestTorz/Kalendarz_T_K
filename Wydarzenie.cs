using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendarz_T_K
{
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
