using BusinessLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TCountry
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public int Population { get; set; }
        public int Surface { get; set; }
        public List<String> Cities { get; set; }
        public List<String> Capitals { get; set; }
        public List<String> Rivers { get; set; }

        [JsonConstructor]
        public TCountry(String name, int population, int surface)
        {
            this.Name = name;
            this.Population = population;
            this.Surface = surface;
        }

        public TCountry(Country country)
        {
            this.Id = String.Format("https://localhost:44389/api/continent/{0}/country/{1}", country.Continent.Id, country.Id);
            this.Name = country.Name;
            this.Population = country.Population;
            this.Surface = country.Surface;
            this.Cities = country.Cities.Select(x => String.Format("https://localhost:44389/api/continent/{0}/country/{1}/city/{2}", country.Continent.Id, country.Id, x.Id)).ToList<String>();
            this.Capitals = country.Capitals.Select(x => String.Format("https://localhost:44389/api/continent/{0}/country/{1}/city/{2}", country.Continent.Id, country.Id, x.Id)).ToList<String>();
            this.Rivers = country.Rivers.Select(x => String.Format("https://localhost:44389/api/river/{0}", x.Id)).ToList<String>();
        }
    }
}
