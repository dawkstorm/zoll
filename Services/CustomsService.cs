using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CustomsController.Services
{
    public class CustomsService : ICustomsService
    {
        private readonly CustomsContext _customContext; // database itself

        public CustomsService(CustomsContext customsContext)
        {

            _customContext = customsContext;
        }

        public CustomsService() { }

        public Country AddNewCountry(string A2Code, bool isEUCU)
        {
            var country = new Country(A2Code, isEUCU);
            var code = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (code == default)
            {
                _customContext.Add(country);
            }
            _customContext.SaveChanges();
            return country;
        }

        public Country RemoveCountry(string A2Code)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (country != default)
            {
                _customContext.Remove(country);
            }
            _customContext.SaveChanges();
            return country;
        }

        public string ChangeEUCU(string A2Code, bool val)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (country != default)
            {
                country.isEUCU = val;
                _customContext.SaveChanges();
                return $"{val}";
            }
            else
            {
                return "country is not in the database";
            }
        }

        public string GetCountryEUCU(string A2Code)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (country != default)
            {
                return $"{country.isEUCU}";
            }
            else
            {
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
                if (country1_isEUCU && country2_isEUCU) return false;
                else return true;
            }
            catch (System.Exception)
            {
                Console.WriteLine("country is not in the database");
                throw;
            }


        }

        //Check does specific postal code belong to EUCU
        bool CheckForEUCU(string country, string pCode)
        {
            var exceptionsForCountry = _customContext.Postleizahlen.Where(c => c.Country == country);
            var exceptionCodes = exceptionsForCountry.Select(b => b.Code);
            
            
            // postleitzahl check
            if (exceptionCodes.Contains(pCode))
                return false; // customs ist true
            else return true;


            var exceptionCodeFrance = "976";
            var LengthFrance = exceptionCodeFrance.Length;





            




            // region //use foreach
            /*
            if (exceptionCodes.Contains(pCode.Substring(0, 2)))
            {
                if (exceptionsForCountry.FirstOrDefault(e => e.Type == "region") != default)
                { //if type is region
                    if (country == "FR")
                    {
                        if (exceptionCodes.Contains(    () return false;
                        else return true;
                    }
                    else return false;
                }
                else return true;
            }
            else return true;
            */   
        }

        public bool GetCustomsBetweenDistricts(string c1, string p1, string c2, string p2)
        {
            return !(CheckForEUCU(c1, p1) && CheckForEUCU(c2, p2));
        }
    }
}