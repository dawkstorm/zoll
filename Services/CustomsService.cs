namespace CustomsController.Services
{
    public class CustomsService : ICustomsService
    {
        private readonly CustomsContext _customContext;

        public CustomsService(CustomsContext customsContext){
            _customContext = customsContext;
        }

        public CustomsService(){ }

        public string Get()
        {
            return "DE";
        }

        public bool Post(string shipper, string receiver)
        {
            var _shipper = _customContext.Countries.Find(shipper);
            var _receiver = _customContext.Countries.Find(receiver);


            if(_shipper.isEU && _shipper.isEU){
                return false;
            }
            if(_shipper.isEU && !_receiver.isEU){
                return true;
            }
            if(!_shipper.isEU && _receiver.isEU){
                return true;
            }
            return false;
        }
    }
}