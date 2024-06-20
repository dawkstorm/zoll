namespace CustomsController.Services
{
    public interface ICustomsService
    {
        public string Get();

        public bool Post(string shipper, string receiver);

        public Country AddNewCountry(string A2Code, bool isEUCU);

        public Country RemoveCountry(string A2Code);

        public string ChangeEUCU(string A2Code, bool val);

        public string GetCountryEUCU(string A2Code);

        public List<Country> GetAllCountries();
    }
}