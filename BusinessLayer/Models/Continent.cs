using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Models
{
    public class Continent
    {
        #region Attributes
        public int Id { get; private set; }
        public String Name { get; private set; }
        public int Population { get; private set; } = 0;
        public virtual ICollection<Country> Countries { get; private set; } = new List<Country>();
        #endregion

        #region Constructor
        public Continent() { }
        public Continent(String name)
        {
            SetName(name);
        }
        #endregion

        #region Methods
        public void SetName(String name)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new InvalidNameException();
            this.Name = name;
        } 
        public void AddCountries(List<Country> countries)
        {
            if(countries == null) throw new InvalidCountryException();
            foreach (Country country in countries)
                AddCountry(country);
        }
        public void AddCountry(Country country)
        {
            if (country == null) throw new InvalidCountryException();
            if (this.Countries.Contains(country)) throw new CountryAlreadyInListException(country, this);
            this.Countries.Add(country);
            this.Population = this.Countries.Sum(x => x.Population);
        }
        public void RemoveCountry(Country country)
        {
            if (country == null) throw new InvalidCountryException();
            if (!this.Countries.Contains(country)) throw new CountryNotInListException(country, this);
            this.Countries.Remove(country);
            this.Population = this.Countries.Sum(x => x.Population); 
        }
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            City y = (City)obj;
            if (this.Id == y.Id && this.Name == y.Name) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Name);
        }
        public override string ToString()
        {
            return string.Format("Continent: {0}", this.Name);
        }
        #endregion

        #region Exceptions
        public class InvalidNameException : Exception
        {
            public InvalidNameException() : base(String.Format("The name cannot be empty")) { }
        }
        public class InvalidCountryException : Exception
        {
            public InvalidCountryException() : base(String.Format("The country cannot be empty")) { }
        }
        public class CountryAlreadyInListException : Exception
        {
            public CountryAlreadyInListException(Country country, Continent continent) : base(String.Format("Country {0} is already listed in {1}", country.Name, continent.Name)) { }
        }
        public class CountryNotInListException : Exception
        {
            public CountryNotInListException(Country country, Continent continent) : base(String.Format("Country {0} was not found in {1}", country.Name, continent.Name)) { }
        }
        #endregion
    }
}
