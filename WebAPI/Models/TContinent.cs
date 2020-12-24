using BusinessLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TContinent
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public int Population { get; set; }
        public virtual List<String> Countries { get; set; }

        [JsonConstructor]
        public TContinent(String name)
        {
            this.Name = name;
        }

        public TContinent(Continent continent)
        {
            this.Id = String.Format("https://localhost:44389/api/continent/{0}", continent.Id);
            this.Name = continent.Name;
            this.Population = continent.Population;
            this.Countries = continent.Countries.Select(x => String.Format("https://localhost:44389/api/continent/{0}/country/{1}", continent.Id, x.Id)).ToList<String>();
        }
    }
}
