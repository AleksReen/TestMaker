using Newtonsoft.Json;

namespace TestMaker.Models.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel
    {
        public TokenResponseViewModel()
        {

        }

        public string token { get; set; }    
        public int expiration { get; set; }
        public string refresh_token { get; set; }
        public string userId { get; set; }

    }
}
