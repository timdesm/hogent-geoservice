using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class River
    {
        #region Attributes
        public int Id { get; private set; }
        public String Name { get; private set; }
        public int Length { get; private set; }
        public virtual ICollection<Country> Countries { get; private set; } = new List<Country>();
        #endregion

        #region Constructor
        public River() { }
        public River(String name, int length, Country country)
        {
            SetName(name);
            SetLength(length);
            
        }
        #endregion

        #region Methods
        public void SetName(String name)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new InvalidNameException();
            this.Name = name;
        }
        public void SetLength(int length)
        {
            if (length < 0) throw new InvalidLengthException();
            this.Length = length;
        }

        public void AddCountries(List<Country> countries)
        {
            if (countries == null) throw new InvalidCountryException();
            foreach (Country country in countries)
                AddCountry(country);
        }
        public void AddCountry(Country country)
        {
            if (country == null) throw new InvalidCountryException();
            if (this.Countries.Contains(country)) throw new CountryAlreadyInListException(country, this);
            this.Countries.Add(country);
        }
        public void RemoveCountry(Country country)
        {
            if (country == null) throw new InvalidCountryException();
            if (!this.Countries.Contains(country)) throw new CountryNotInListException(country, this);
            this.Countries.Remove(country);
        }
        #endregion

        #region Override
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType())) return false;
            River y = (River) obj;
            if (this.Id == y.Id && this.Name == y.Name) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Name);
        }
        public override string ToString()
        {
            return string.Format("River: {0}, {1}", this.Name, this.Length);
        }
        #endregion

        #region Exceptions
        public class InvalidNameException : Exception
        {
            public InvalidNameException() : base(String.Format("The name cannot be empty")) { }
        }
        public class InvalidLengthException : Exception
        {
            public InvalidLengthException() : base(String.Format("The length cannot be empty or lower than 0")) { }
        }
        public class InvalidCountryException : Exception
        {
            public InvalidCountryException() : base(String.Format("The country cannot be empty")) { }
        }
        public class CountryAlreadyInListException : Exception
        {
            public CountryAlreadyInListException(Country country, River river) : base(String.Format("Country {0} is already listed in {1}", country.Name, river.Name)) { }
        }
        public class CountryNotInListException : Exception
        {
            public CountryNotInListException(Country country, River river) : base(String.Format("Country {0} was not found in {1}", country.Name, river.Name)) { }
        }
        #endregion
    }
}
