using BusinessLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TCity
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public int Population { get; set; }
        public String Country { get; set; }

        [JsonConstructor]
        public TCity(String name, int population)
        {
            this.Name = name;
            this.Population = population;
        }

        public TCity(City city)
        {
            this.Id = String.Format("https://localhost:44389/api/continent/{0}/country/{1}/city/{2}", city.Country.Continent.Id, city.Country.Id, city.Id);
            this.Name = city.Name;
            this.Population = city.Population;
            this.Country = String.Format("https://localhost:44389/api/continent/{0}/country/{1}", city.Country.Continent.Id, city.Country.Id);
        }
    }
}
