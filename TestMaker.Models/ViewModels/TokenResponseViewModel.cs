using Newtonsoft.Json;

namespace TestMaker.Models.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    class TokenResponseViewModel
    {
        public TokenResponseViewModel()
        {

        }

        public string token { get; set; }
        public int expiration { get; set; }
    }
}
