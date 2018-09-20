using Newtonsoft.Json;

namespace Core.BlockIO
{
    public class Address
    {
        [JsonProperty("address")]
        public string WalletAddress { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        public static Address ToAddress(string json)
        {
            return JsonConvert.DeserializeObject<Address>(json);
        }
    }
}