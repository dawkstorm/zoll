using Microsoft.EntityFrameworkCore;

namespace CustomsController.Services
{
    public class CustomsService : ICustomsService
    {
        private readonly CustomsContext _customContext; // using var db

        public CustomsService(CustomsContext customsContext){

            _customContext = customsContext;
        }

        public CustomsService(){ }

        public string Get()
        {
            _customContext.SaveChanges();
            return "DE";
        }

        public bool Post(string shipper, string receiver)
        {
            var _shipper = _customContext.Countries.Find(shipper);
            var _receiver = _customContext.Countries.Find(receiver);


            if(_shipper.isEUCU && _shipper.isEUCU){
                return false;
            }
            if(_shipper.isEUCU && !_receiver.isEUCU){
                return true;
            }
            if(!_shipper.isEUCU && _receiver.isEUCU){
                return true;
            }
            return false;
        }

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
    }
}