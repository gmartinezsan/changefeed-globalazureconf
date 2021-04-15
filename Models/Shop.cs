using Newtonsoft.Json;

namespace Webstore.Functions.Models
{
    public class Shop
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }

        [JsonProperty("ShopId")]
        public string ShopId { get; set; }

        [JsonProperty("Total")]
        public decimal Total { get; set; }

    }
}



