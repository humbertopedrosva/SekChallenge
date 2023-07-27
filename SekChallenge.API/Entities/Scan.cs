using SekChallenge.API.Entities.Enums;
using System.Text.Json.Serialization;

namespace SekChallenge.API.Entities
{
    public class Scan : EntityBase
    {
        public Scan()
        {
                
        }

        [JsonPropertyName("permalink")]
        public string Permalink { get; set; }

        [JsonPropertyName("resource")]
        public string Resource { get; set; }

        [JsonPropertyName("scan_id")]
        public string ScanId { get; set; }

        [JsonPropertyName("sha256")]
        public string SHA256 { get; set; }

        [JsonPropertyName("response_code")]
        public ScanFileResponseCode ResponseCode { get; set; }

        [JsonPropertyName("verbose_msg")]
        public string VerboseMsg { get; set; }

        public bool Finished { get; set; }
    }
}
