using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLayer.Models
{
    public class Country
    {
        #region Attributes
        public int Id { get; private set; }
        public String Name { get; private set; }
        public int Population { get; private set; }
        public int Surface { get; private set; }
        public Continent Continent { get; private set; }
        public virtual ICollection<City> Cities { get; private set; } = new List<City>();
        public virtual ICollection<City> Capitals { get; private set; } = new List<City>();
        public virtual ICollection<River> Rivers { get; private set; } = new List<River>();
        #endregion

        #region Constructor
        public Country() { }
        public Country(String name, int population, int surface, Continent continent)
        {
            SetName(name);
            SetPopulation(population);
            SetSurface(surface);
            SetContinent(continent);
        }
        #endregion

        #region Methods
        public void SetName(String name)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new InvalidNameException();
            this.Name = name;
        }
        public void SetPopulation(int population)
        {
            if (population < 1) throw new InvalidPopulationException();
            this.Population = population;
        }
        public void SetSurface(int surface)
        {
            if (surface < 1) throw new InvalidSurfaceException();
            this.Surface = surface;
        }
        public void SetContinent(Continent continent)
        {
            if (continent == null) throw new InvalidContinentException();
            this.Continent = continent;
        }
        #region Cities
        public void AddCities(List<City> cities)
        {
            if(cities == null) throw new InvalidCityException();
            foreach (City city in cities)
                AddCity(city);
        }
        public void AddCity(City city)
        {
            if (city == null) throw new InvalidCityException();
            if (this.Cities.Contains(city)) throw new CityAlreadyInListException(city, this);
            this.Cities.Add(city);
        }
        public void RemoveCity(City city)
        {
            if(city == null) throw new InvalidCityException();
            if (!this.Cities.Contains(city)) throw new CityNotInListException(city, this);
            this.Cities.Remove(city);
        }
        #endregion
        #region Capitals
        public void AddCapitals(List<City> capitals)
        {
            if (capitals == null) throw new InvalidCapitalException();
            foreach (City capital in capitals)
                AddCapital(capital);
        }
        public void AddCapital(City capital)
        {
            if (capital == null) throw new InvalidCapitalException();
            if (this.Capitals.Contains(capital)) throw new CapitalAlreadyInListException(capital, this);
            if (!this.Cities.Contains(capital)) throw new CapitalNotInCitiesException(capital, this);
            this.Capitals.Add(capital);
        }
        public void RemoveCapital(City capital)
        {
            if (capital == null) throw new InvalidCapitalException();
            if (!this.Capitals.Contains(capital)) throw new CapitalNotInListException(capital, this);
            this.Capitals.Remove(capital);
        }
        #endregion
        #region Rivers
        public void AddRivers(List<River> rivers)
        {
            if (rivers == null) throw new InvalidRiverException();
            foreach (River river in rivers)
                AddRiver(river);
        }
        public void AddRiver(River river)
        {
            if (river == null) throw new InvalidRiverException();
            if (this.Rivers.Contains(river)) throw new RiverAlreadyInListException(river, this);
            this.Rivers.Add(river);
        }
        public void RemoveRiver(River river)
        {
            if (river == null) throw new InvalidRiverException();
            if (!this.Rivers.Contains(river)) throw new RiverNotInListException(river, this);
            this.Rivers.Remove(river);
        }
        #endregion
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            Country y = (Country) obj;
            if (this.Id == y.Id && this.Name == y.Name) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Name);
        }
        public override string ToString()
        {
            return string.Format("Country: {0}, {1}, {2}", this.Name, this.Population, this.Surface);
        }
        #endregion

        #region Exceptions
        public class InvalidNameException : Exception
        {
            public InvalidNameException() : base(String.Format("The name cannot be empty")) { }
        }
        public class InvalidPopulationException : Exception
        {
            public InvalidPopulationException() : base(String.Format("The population cannot be empty or lower than 1")) { }
        }
        public class InvalidSurfaceException : Exception
        {
            public InvalidSurfaceException() : base(String.Format("The surface cannot be empty or lower than 1")) { }
        }
        public class InvalidContinentException : Exception
        {
            public InvalidContinentException() : base(String.Format("The continent cannot be empty")) { }
        }
        public class InvalidCityException : Exception
        {
            public InvalidCityException() : base(String.Format("The city cannot be empty")) { }
        }
        public class CityAlreadyInListException : Exception
        {
            public CityAlreadyInListException(City city, Country country) : base(String.Format("City {0} is already listed in {1}", city.Name, country.Name)) { }
        }
        public class CityNotInListException : Exception
        {
            public CityNotInListException(City city, Country country) : base(String.Format("City {0} was not found in {1}", city.Name, country.Name)) { }
        }
        public class InvalidRiverException : Exception
        {
            public InvalidRiverException() : base(String.Format("The river cannot be empty")) { }
        }
        public class RiverAlreadyInListException : Exception
        {
            public RiverAlreadyInListException(River river, Country country) : base(String.Format("River {0} is already listed in {1}", river.Name, country.Name)) { }
        }
        public class RiverNotInListException : Exception
        {
            public RiverNotInListException(River river, Country country) : base(String.Format("River {0} was not found in {1}", river.Name, country.Name)) { }
        }
        public class InvalidCapitalException : Exception
        {
            public InvalidCapitalException() : base(String.Format("The capital cannot be empty")) { }
        }
        public class CapitalAlreadyInListException : Exception
        {
            public CapitalAlreadyInListException(City capital, Country country) : base(String.Format("Capital {0} is already listed in {1}", capital.Name, country.Name)) { }
        }
        public class CapitalNotInListException : Exception
        {
            public CapitalNotInListException(City capital, Country country) : base(String.Format("Capital {0} was not found in {1}", capital.Name, country.Name)) { }
        }
        public class CapitalNotInCitiesException : Exception
        {
            public CapitalNotInCitiesException(City capital, Country country) : base(String.Format("Capital {0} was present in cities of {1}", capital.Name, country.Name)) { }
        }
        #endregion
    }
}
