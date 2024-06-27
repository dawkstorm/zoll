using CustomsController.Model;
using Microsoft.EntityFrameworkCore;

namespace CustomsController.Services;

/// <inheritdoc/>
public class CustomsService : ICustomsService
{
    private readonly CustomsContext _customContext;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="customsContext">for logic</param>
    public CustomsService(CustomsContext customsContext)
    {
        _customContext = customsContext;
    }

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
    public CustomsResponse GetCustoms(string country1code, string country2code)
    {
        var country1_isEUCU = _customContext.Countries.FirstOrDefault(c => c.A2Code == country1code).IsEUCU;
        var country2_isEUCU = _customContext.Countries.FirstOrDefault(c => c.A2Code == country2code).IsEUCU;

        var doesCountryExist = DoesCountryExist(country1code, country2code);
        if (doesCountryExist != null)
            return doesCountryExist;

        var result2 = new CustomsResponse()
        {
            IfCustomsInEUCU = !(country1_isEUCU && country2_isEUCU),
            Success = true
        };
        return result2;
    }

    private CustomsResponse DoesCountryExist(string country1code, string country2code)
    {
        if (GetCountry(country1code) == default || GetCountry(country2code) == default)
        {
            var result = new CustomsResponse()
            {
                Message = "Country wasn't found in the list",
                Success = false
            };
            return result;
        }
        else return null;
    }

    //Check if in one region
    private bool CheckIfTheSameRegion(string c1, string p1, string c2, string p2)
    {
        if (c1 == c2)
        {
            var exceptionsForCountry = _customContext.PostalCodes.Where(c => c.Country.A2Code == c1);
            var exceptionCodes = exceptionsForCountry.Select(b => b.Code).ToList();

            foreach (var code in exceptionCodes)
            {
                if (p1.StartsWith(code) && p2.StartsWith(code))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Check does specific postal code belong to EUCU
    private bool CheckIfEUCU(string country, string pCode, string city)
    {
        var countryCheck = CountryCheck(country);
        if (!countryCheck)
            return countryCheck;

        var exceptionsForCountry = _customContext.PostalCodes.Where(c => c.Country.A2Code == country);

        var zipcodeCheck = ZipcodeCheck(pCode, exceptionsForCountry);
        if (!zipcodeCheck)
            return zipcodeCheck;

        var regionCheck = RegionCheck(pCode, exceptionsForCountry);
        if (!regionCheck)
            return regionCheck;

        var cityCheck = CityCheck(pCode, city, exceptionsForCountry);
        if (!cityCheck)
            return cityCheck;

        return true;
    }

    private bool CityCheck(string pCode, string city, IQueryable<PostalCode> exceptionsForCountry)
    {
        foreach (var exceptionForCountry in exceptionsForCountry)
        {
            if (exceptionForCountry.Type == PostalCodeType.City
            && exceptionForCountry.Code == pCode
            && exceptionForCountry.City == city)
            {
                return false;
            }
        }
        return true;
    }

    private bool RegionCheck(string pCode, IQueryable<PostalCode> exceptionsForCountry)
    {
        foreach (var exceptionForCountry in exceptionsForCountry)
        {
            if (exceptionForCountry.Type == PostalCodeType.Region && pCode.StartsWith(exceptionForCountry.Code))
                return false;
        }
        return true;
    }

    private bool ZipcodeCheck(string pCode, IQueryable<PostalCode> exceptionsForCountry)
    {
        // postleitzahl check
        // if (exceptionCodes.Contains(pCode) )
        //    return false; // customs ist true
        foreach (var exceptionForCountry in exceptionsForCountry)
            if (exceptionForCountry.Code == pCode && exceptionForCountry.Type == PostalCodeType.Zipcode)
                return false;
        return true;
    }

    private bool CountryCheck(string country)
    {
        if (!bool.Parse(GetCountryEUCU(country)))
            return false;
        return true;
    }

    /// <inheritdoc/>
    public CustomsResponse GetCustomsBetweenDistricts(
    string shipperC1,
    string shipperP1,
    string receiverC2,
    string receiverP2,
    string shipperCity1,
    string receiverCity2)
    {
        var doesCountryExist = DoesCountryExist(shipperC1, receiverC2);
        if (doesCountryExist != null)
            return doesCountryExist;

        var result = CheckIfSameCountryAndSameZipcode(shipperC1, shipperP1, receiverC2, receiverP2);
        if (!result)
            return new CustomsResponse()
            {
                IfCustomsInEUCU = result,
                Success = true,
            };

        var resultRegion = CheckIfTheSameRegion(shipperC1, shipperP1, receiverC2, receiverP2);
        if (resultRegion)
            return new CustomsResponse()
            {
                IfCustomsInEUCU = !resultRegion,
                Success = true,
            };

        var resultShipper = CheckIfEUCU(shipperC1, shipperP1, shipperCity1);
        if (!resultShipper)
            return new CustomsResponse()
            {
                IfCustomsInEUCU = !resultShipper,
                Success = true,
            };

        var resultReceiver = CheckIfEUCU(receiverC2, receiverP2, receiverCity2);
        if (!resultReceiver)
            return new CustomsResponse()
            {
                IfCustomsInEUCU = !resultReceiver,
                Success = true,
            };

        return new CustomsResponse()
        {
            IfCustomsInEUCU = false,
            Success = true,
        };
    }

    private bool CheckIfSameCountryAndSameZipcode(string c1, string p1, string c2, string p2)
    {
        if (c1 == c2 && p1 == p2) return false;  // EUCU customs is false
        return true;
    }

    /// <inheritdoc/>
    public Country? GetCountry(string A2Code)
    {
        var country = _customContext.Countries.FirstOrDefault(c => c.A2Code == A2Code);
        return country;
    }
}