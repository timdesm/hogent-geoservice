using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class City
    {
        #region Attributes
        public int Id { get; private set; }
        public String Name { get; private set; }
        public int Population { get; private set; }
        public virtual Country Country { get; private set; }
        #endregion

        #region Constructor
        public City(String name, Country country, int population)
        {
            SetName(name);
            SetCountry(country);
            SetPopulation(population);
        }
        #endregion

        #region Methods
        public void SetName(String name)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new InvalidNameException();
            this.Name = name;
        }
        public void SetCountry(Country country)
        {
            if (country == null) throw new InvalidCountryException();
            this.Country = country;
        }
        public void SetPopulation(int population)
        {
            if (population < 0) throw new InvalidPopulationException();
            if (population > this.Country.Population) throw new PopulationGreaterThanCountryPopulation();
            this.Population = population;
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
            return string.Format("City: {0}, {1}", this.Name, this.Population);
        }
        #endregion

        #region Exceptions
        public class InvalidNameException : Exception
        {
            public InvalidNameException() : base(String.Format("The name cannot be empty")) { }
        }
        public class InvalidPopulationException : Exception
        {
            public InvalidPopulationException() : base(String.Format("The population cannot be empty or lower than 0")) { }
        }
        public class PopulationGreaterThanCountryPopulation : Exception
        {
            public PopulationGreaterThanCountryPopulation() : base(String.Format("The population cannot be greater than country population")) { }
        }
        public class InvalidCountryException : Exception
        {
            public InvalidCountryException() : base(String.Format("The country cannot be empty")) { }
        }
        #endregion
    }
}
