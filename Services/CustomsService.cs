namespace CustomsController.Services
{
    public class CustomsService : ICustomsService
    {
        public CustomsService()
        {

        }

        public string Get()
        {
            return "DE";
        }

        public bool Post(string country)
        {
            return country == "DE" || country == "UK";
        }
    }
}