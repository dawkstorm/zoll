using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CustomsController.Services
{
    public class CustomsService : ICustomsService
    {
        private readonly CustomsContext _customContext; // database itself

        public CustomsService(CustomsContext customsContext){

            _customContext = customsContext;
        }

        public CustomsService(){ }

        public Country AddNewCountry(string A2Code, bool isEUCU)
        {
            var country = new Country(A2Code, isEUCU);
            var code = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if(code == default){
                _customContext.Add(country);
            }
            _customContext.SaveChanges();
            return country;
        }

        public Country RemoveCountry(string A2Code)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if(country != default){
                _customContext.Remove(country);
            }
            _customContext.SaveChanges();
            return country;
        }

        public string ChangeEUCU(string A2Code, bool val)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if(country != default){
                country.isEUCU = val;
                _customContext.SaveChanges();
                return $"{val}";
            }
            else{
                return "country is not in the database";
            }
        }

        public string GetCountryEUCU(string A2Code)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if(country != default){
                return $"{country.isEUCU}";
            }
            else{
                return "country is not in the database";
            }
            
        }

        public List<Country> GetAllCountries()
        {
            return _customContext.Countries.ToList();
        }

        public bool GetCustoms(string country1code, string country2code)
        {
            var country1_isEUCU = bool.Parse(GetCountryEUCU(country1code));
            var country2_isEUCU = bool.Parse(GetCountryEUCU(country2code));

            try
            {
                if(country1_isEUCU && country2_isEUCU) return false;
                else return true;
            }
            catch (System.Exception)
            {
                Console.WriteLine("country is not in the database");
                throw;
            }
            

        }
    }
}