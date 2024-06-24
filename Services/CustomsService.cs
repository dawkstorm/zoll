using Microsoft.AspNetCore.Http.HttpResults;

namespace CustomsController.Services
{
    /// <inheritdoc/>
    public class CustomsService : ICustomsService
    {
        private readonly CustomsContext _customContext; // database itself
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customsContext">for logic</param>
        public CustomsService(CustomsContext customsContext)
        {
            _customContext = customsContext;
        }

        public CustomsService() { }

        /// <inheritdoc/>
        public Country AddNewCountry(string A2Code, bool isEUCU)
        {
            var country = new Country(A2Code, isEUCU);
            var code = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (code != default)
            {
                return default;
            }
            _customContext.Add(country);
            _customContext.SaveChanges();
            return country;
        }

        /// <inheritdoc/>
        public Country RemoveCountry(string A2Code)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (country == default)
            {
                return null;
            }
            _customContext.Remove(country);
            _customContext.SaveChanges();
            return country;
        }

        /// <inheritdoc/>
        public string ChangeEUCU(string A2Code, bool val)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (country != default)
            {
                country.IsEUCU = val;
                _customContext.SaveChanges();
                return $"{val}";
            }
            else
            {
                return "country is not in the database";
            }
        }

        /// <inheritdoc/>
        public string GetCountryEUCU(string A2Code)
        {
            var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
            if (country != default)
            {
                return $"{country.IsEUCU}";
            }
            else
            {
                return "country is not in the database";
            }

        }

        /// <inheritdoc/>
        public List<Country> GetAllCountries()
        {
            return _customContext.Countries.ToList();
        }

        /// <inheritdoc/>
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
        private bool CheckForEUCU(string country, string pCode)
        {
            var exceptionsForCountry = _customContext.Postleizahlen.Where(c => c.Country == country);
            var exceptionCodes = exceptionsForCountry.Select(b => b.Code);


            // postleitzahl check
            if (exceptionCodes.Contains(pCode))
                return false; // customs ist true
            else
            {
                //region check
                return true;
            }

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

        /// <inheritdoc/>
        public bool GetCustomsBetweenDistricts(string c1, string p1, string c2, string p2)
        {
            return !(CheckForEUCU(c1, p1) && CheckForEUCU(c2, p2));
        }
    }
}