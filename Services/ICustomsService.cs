namespace CustomsController.Services
{
    /// <summary>
    /// Service for Customs
    /// </summary>
    public interface ICustomsService
    {
        /// <summary>
        /// Add a new country to the database
        /// </summary>
        /// <param name="A2Code">Country's A2 isocode</param>
        /// <param name="isEUCU">is counntry a part of EUCU</param>
        public Country AddNewCountry(string A2Code, bool isEUCU);

        /// <summary>
        /// Remove country from the database
        /// </summary>
        /// <param name="A2Code">Country's A2 isocode</param>
        public Country RemoveCountry(string A2Code);
        /// <summary>
        /// Change boolean isEUCu
        /// </summary>
        /// <param name="A2Code">Country's A2 isocode</param>
        public string ChangeEUCU(string A2Code, bool val);
        /// <summary>
        /// Check whether country is in EUCU or not
        /// </summary>
        /// <param name="A2Code">Country's A2 isocode</param>
        /// <returns></returns>
        public string GetCountryEUCU(string A2Code);
        /// <summary>
        /// Return all countries
        /// </summary>
        public List<Country> GetAllCountries();
        /// <summary>
        /// Check whether there are customs between 2 countries
        /// </summary>
        /// <param name="country1code">Shipper country's isocode</param>
        /// <param name="country2code">Receiver country's isocode</param>
        /// <returns></returns>
        public bool GetCustoms(string country1code, string country2code);
        /// <summary>
        /// Check whether there are customs between 2 postalCode
        /// </summary>
        /// <param name="c1">Shipper country</param>
        /// <param name="p1">Shipper postalCode</param>
        /// <param name="c2">Receiver country</param>
        /// <param name="p2">Receiver postalCode</param>
        public bool GetCustomsBetweenDistricts(string c1, string p1, string c2, string p2);
    }
}