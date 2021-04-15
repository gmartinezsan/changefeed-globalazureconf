using Newtonsoft.Json;

namespace Webstore.Functions.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("CustomerId")]
        public string CustomerId { get; set; }


        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")]
        public Address Address { get; set; }

        [JsonProperty("Total")]
        public decimal Total { get; set; }

    }
}



