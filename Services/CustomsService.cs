using System.Runtime.Intrinsics.X86;
using CustomsController.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CustomsController.Services;

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

        var countrytest = _customContext.Countries
            .FirstOrDefault(c => c.A2Code == A2Code);
        var countrytest2 = _customContext.Countries
            .Include(c => c.PostalCodes)
            .FirstOrDefault(c => c.A2Code == A2Code);





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
    public bool? GetCustoms(string country1code, string country2code)
    {
        var country1_isEUCU = bool.Parse(GetCountryEUCU(country1code));
        var country2_isEUCU = bool.Parse(GetCountryEUCU(country2code));

        if (GetCountry(country1code) == default || GetCountry(country2code) == default)
        {
            return null;
        }

        if (country1_isEUCU && country2_isEUCU) return false;
        else return true;
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
    private bool CheckIfEUCU(string country, string pCode)
    {
        var countryCheck = CountryCheck(country);
        if (!countryCheck)
            return countryCheck;


        var exceptionsForCountry = _customContext.PostalCodes.Where(c => c.Country.A2Code == country);
        var exceptionCodes = exceptionsForCountry.Select(b => b.Code).ToList();

        var zipcodeCheck = ZipcodeCheck(pCode, exceptionCodes);
        if (!zipcodeCheck)
            return zipcodeCheck;

        //region check
        var regionCheck = RegionCheck(pCode, exceptionsForCountry);
        if (!regionCheck)
            return regionCheck;

        //city check
        // var cityCheck = CityCheck();

        return true;

        // region //use foreach
        // there's no customs within one postleitzahl

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

    private bool ZipcodeCheck(string pCode, List<string> exceptionCodes)
    {
        // postleitzahl check
        if (exceptionCodes.Contains(pCode))
            return false; // customs ist true
        return true;
    }

    private bool CountryCheck(string country)
    {
        if (!bool.Parse(GetCountryEUCU(country)))
            return false;
        return true;
    }

    /// <inheritdoc/>
    public bool GetCustomsBetweenDistricts(string shipperC1, string shipperP1, string receiverC2, string receiverP2)
    {
        var result = CheckIfSameCountryAndSameZipcode(shipperC1, shipperP1, receiverC2, receiverP2);
        if (!result)
            return result;

        var resultRegion = CheckIfTheSameRegion(shipperC1, shipperP1, receiverC2, receiverP2);
        if (resultRegion)
            return !resultRegion;

        var resultShipper = CheckIfEUCU(shipperC1, shipperP1);
        if (!resultShipper)
            return !resultShipper;

        var resultReceiver = CheckIfEUCU(receiverC2, receiverP2);
        if (!resultReceiver)
            return !resultReceiver;

        return false;
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