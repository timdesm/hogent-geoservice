using BusinessLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TRiver
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public int Length { get; set; }
        public List<String> Countries { get; set; }

        [JsonConstructor]
        public TRiver(String name, int length, List<int> countries)
        {
            this.Name = name;
            this.Length = length;
            this.Countries = countries.Select(x => x.ToString()).ToList<String>();
        }

        public TRiver(River river)
        {
            this.Id = String.Format("https://localhost:44389/api/river/{0}", river.Id);
            this.Name = river.Name;
            this.Length = river.Length;
            this.Countries = river.Countries.Select(x => String.Format("https://localhost:44389/api/continent/{0}/country/{1}", x.Continent.Id, x.Id)).ToList<String>();
        }
    }
}
