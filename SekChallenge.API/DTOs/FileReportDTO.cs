using SekChallenge.API.Entities.Enums;
using System.Text.Json.Serialization;

namespace SekChallenge.API.DTOs
{
    public class FileReportDTO
    {
        [JsonPropertyName("scan_date")]
        public DateTime ScanDate { get; set; }

        [JsonPropertyName("scan_id")]
        public string ScanId { get; set; }

        [JsonPropertyName("scans")]
        public Dictionary<string, object> Scans { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("response_code")]
        public ScanFileResponseCode ResponseCode { get; set; }

        [JsonPropertyName("verbose_msg")]
        public string VerboseMsg { get; set; }
    }
}
