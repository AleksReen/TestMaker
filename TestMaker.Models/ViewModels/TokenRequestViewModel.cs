using Newtonsoft.Json;

namespace TestMaker.Models.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    class TokenRequestViewModel {

        public TokenRequestViewModel(){}

        public string grand_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
}
