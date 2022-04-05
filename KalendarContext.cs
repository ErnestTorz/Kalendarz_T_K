using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Collections.ObjectModel;

namespace Kalendarz_T_K
{   
    /// Klasa bazy danych
    public class KalendarContext : DbContext
    {
        // Your context has been configured to use a 'KalendarContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Kalendarz_T_K.KalendarContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'KalendarContext' 
        // connection string in the application configuration file.
        public KalendarContext()
            : base("name=KalendarContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Termin> Terminy { get; set; }
        public DbSet<Wydarzenie> Wydarzenia { get; set; }
    }

    public class KalendarzDbInitializer : DropCreateDatabaseAlways<KalendarContext>
    {

        protected override void Seed(KalendarContext context)
        {


            var terminy = new List<Termin>
                {
                    new Termin()
                    {
                        ID=1001,
                         Data= new DateTime(2022, 3, 1),
                         Wydarzenia = new Collection<Wydarzenie> {
                            new Wydarzenie()
                            {
                                ID = 3001,
                                Notatka="Rajd rowerowy.",
                                Godzina_start="12:00",
                                Godzina_stop="13:00",
                                TerminID=1001,
                                Wykonane=false,
                            },
                            new Wydarzenie()
                            {
                                ID = 3002,
                                Notatka="Obiad.",
                                Godzina_start="13:00",
                                Godzina_stop="14:00",
                                TerminID=1001,
                                Wykonane=false,
                            }

                        }
                    },
                     new Termin()
                    {
                        ID=1002,
                         Data= new DateTime(2022, 3, 2),
                         Wydarzenia = new Collection<Wydarzenie> {
                            new Wydarzenie()
                            {
                                ID = 3003,
                                Notatka="Wyprowadzka.",
                                Godzina_start="10:10",
                                Godzina_stop="12:20",
                                TerminID=1002,
                                Wykonane=false,
                            },
                            new Wydarzenie()
                            {
                                ID = 3004,
                                Notatka="Kolacja.",
                                Godzina_start="20:00",
                                Godzina_stop="21:00",
                                TerminID=1002,
                                Wykonane=false,
                            }

                        }
                     }
            };
                
            terminy.ForEach(s => context.Terminy.Add(s));
            context.SaveChanges();
        }
    }
}
