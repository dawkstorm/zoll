namespace CustomsController.Services
{
    public interface ICustomsService
    {
        public string Get();

        public bool Post(string country);
    }
}