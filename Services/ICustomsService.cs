namespace CustomsController.Services
{
    public interface ICustomsService
    {
        public Country AddNewCountry(string A2Code, bool isEUCU);

        public Country RemoveCountry(string A2Code);

        public string ChangeEUCU(string A2Code, bool val);

        public string GetCountryEUCU(string A2Code);

        public List<Country> GetAllCountries();

        public bool GetCustoms(string country1code, string country2code);
    }
}