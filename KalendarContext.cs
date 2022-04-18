using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

/// G³ówna przestrzeñ nazw programu kalendarza.
namespace Kalendarz_T_K
{   
    /// Klasa kontekstu bazy danych programu kalendarza.
    public class KalendarContext : DbContext
    {
        /// Metoda inicjalizuje kontekst bazy danych.
        ///
        /// Ponadto wy³¹cza tzw. leniwe ³adowanie.
        public KalendarContext() : base("name=KalendarContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Termin> Terminy { get; set; } ///< Encja **Termin**
        public DbSet<Wydarzenie> Wydarzenia { get; set; } ///< Encja **Wydarzenie**
    }

    /// Klasa inicjalizatora bazy danych programu kalendarza.
    public class KalendarzDbInitializer : DropCreateDatabaseAlways<KalendarContext>
    {
        /// Metoda wype³niaj¹ca bazê danych pocz¹tkowymi wartoœciami.
        protected override void Seed(KalendarContext context)
        {
            var terminy = new List<Termin>
                {
                    new Termin()
                    {
                        ID = 1001,
                        Data = new DateTime(2022, 3, 1),
                        Wydarzenia = new Collection<Wydarzenie> {
                            new Wydarzenie()
                            {
                                ID = 3001,
                                Notatka = "Rajd rowerowy.",
                                Godzina_start = "12:00",
                                Godzina_stop = "13:00",
                                TerminID = 1001,
                                Wykonane = false,
                            },
                            new Wydarzenie()
                            {
                                ID = 3002,
                                Notatka = "Obiad.",
                                Godzina_start = "13:00",
                                Godzina_stop = "14:00",
                                TerminID = 1001,
                                Wykonane = false,
                            }
                        }
                    },
                    new Termin()
                    {
                        ID = 1002,
                        Data = new DateTime(2022, 3, 2),
                        Wydarzenia = new Collection<Wydarzenie> {
                            new Wydarzenie()
                            {
                                ID = 3003,
                                Notatka = "Wyprowadzka.",
                                Godzina_start = "10:10",
                                Godzina_stop = "12:20",
                                TerminID = 1002,
                                Wykonane = false,
                            },
                            new Wydarzenie()
                            {
                                ID = 3004,
                                Notatka = "Kolacja.",
                                Godzina_start = "20:00",
                                Godzina_stop = "21:00",
                                TerminID = 1002,
                                Wykonane = false,
                            }
                        }
                     }
            };
            terminy.ForEach(s => context.Terminy.Add(s));
            context.SaveChanges();
        }
    }
}