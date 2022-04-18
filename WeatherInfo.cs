using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

/// Główna przestrzeń nazw programu kalendarza.
namespace Kalendarz_T_K
{   
    /// Klasa przechowuje informacje pobrane
    /// przy pomocy pogodowego API
    class WeatherInfo
    {
        /// Klasa przechowuje informacje o
        /// współrzędnych geograficznych miast.
        public class coord
        {
            public double lon { get; set; } ///< Długość geograficzna miasta
            public double lat { get; set; } ///< Szerokość geograficzna miasta
        }

        /// Klasa przechowuje inforamcje o 
        /// pogodzie.
        public class weather
        {
            public string main { get; set; } ///< Grupa parametrów pogodowych, np. deszcz
            public string description { get; set; } ///< Stan w grupie parametrów pogodowych
            public string icon { get; set; } ///< Ikona pogody
        }

        /// Klasa przechowuje inforamcje o
        /// temperaturze, minimalnej i
        /// maksymalnej temperaturze,
        /// ciśnieniu i wilgotności.
        public class main
        {
            public double temp { get; set; } ///< Temperatura
            public double temp_min { get; set; } ///< Aktualna minimalna temperatura
            public double temp_max { get; set; } ///< Aktualna maksymalna temperatura
            public double pressure { get; set; } ///< Ciśnienie w hPa
            public double humidity { get; set; } ///< Wilgotność w %
        }
        
        /// Klasa przechowuje informacje o
        /// wietrze.
        public class wind
        {
            public double speed { get; set; } ///< Prędkość wiatru
        }

        /// Klasa przechowuje informacje o 
        /// kodzie państwa, czasie wschodu 
        /// i zachodu słońca.
        public class sys
        {
            public string country { get; set; } ///< Kod państwa, np. PL
            public long sunrise { get; set; } ///< Czas wschodu słońca
            public long sunset { get; set; } ///< Czas zachodu słońca
        }

        /// Klasa przechowuje wszystkie
        /// informacje o pogodzie i
        /// informacje o nazwie miasta.
        public class root
        {
            public coord coord { get; set; } ///< Współrzędne geograficzne
            public List <weather> weather { get; set; } ///< Pogoda
            public main main { get; set; } ///< Temperatura, minimalna i maksymalna temperatura, ciśnienie i wilgotność
            public wind wind { get; set; } ///< Prędkość wiatru
            public sys sys { get; set; } ///< Kod państwa, czas wschodu i zachodu słońca
            public string name { get; set; } ///< Nazwa miasta
        }
    }
}