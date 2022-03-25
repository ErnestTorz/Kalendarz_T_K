using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendarz_T_K
{
    public class Termin
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }

        public virtual ICollection<Wydarzenie> Wydarzenia { get; set; }
    }
}
