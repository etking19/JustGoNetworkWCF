using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfService.Model;

namespace WcfService.Dao
{
    public class CountryDao : BaseDao
    {
        public string AddCountry(Country country)
        {
            return "1";
        }

        public bool UpdateCountry(string countryId, Country country)
        {
            return true;
        }

        public Country GetCountry(string countryId)
        {
            return new Country()
            {
                countryId = "1",
                name = "Malaysia"
            };
        }

        public List<Country> GetCountris()
        {
            List<Country> countryList = new List<Country>();
            countryList.Add(new Country() { countryId ="1", name="Malaysia" });
            countryList.Add(new Country() { countryId = "2", name = "Thailand" });

            return countryList;
        }

        public bool DeleteCountry(string countryId)
        {
            return true;
        }
    }
}