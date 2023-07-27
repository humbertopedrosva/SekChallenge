using SekChallenge.API.DTOs;
using SekChallenge.API.Entities;
using SekChallenge.API.Infra.Repositories.Interfaces;
using System.Text.Json;

namespace SekChallenge.API.Services.Interfaces
{
    public class FileService : IFileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IScanRepository _scanRepository;

        public FileService(IHttpClientFactory httpClientFactory, IScanRepository scanRepository)
        {
            _httpClientFactory = httpClientFactory;
            _scanRepository = scanRepository;
        }
        public async Task<List<Scan>> SendFilesAsync(SendFilesDTO sendFilesDTO)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var scans = new List<Scan>();

            foreach (var file in sendFilesDTO.Files)
            {
                var scan = await SendFile(file, httpClient);
                scans.Add(scan);
                await _scanRepository.CreateAsync(scan);
            }

            return scans;
        }

        public async Task<bool> FileReportAsync(string resource)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var queryParameters = new Dictionary<string, string>
            {
                { "apikey", "c8a108d7596029407e7df11a333829607d5fed152a5888c843faac32eff25508" },
                { "resource", resource}
            };
            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            var response = await httpClient.GetAsync($"https://www.virustotal.com/vtapi/v2/file/report?{queryString}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var scan = JsonSerializer.Deserialize<FileReportDTO>(responseContent);

            if (scan.Scans.Any())
                return true;

            return false;
        }


        private async Task<Scan> SendFile(IFormFile file, HttpClient httpClient)
        {
            using MemoryStream ms = new MemoryStream();

            await file.CopyToAsync(ms);

            using MultipartFormDataContent multi = new MultipartFormDataContent
        {
            CreateApiPart(),
            CreateFileContent(ms, file.FileName)
            };


            var response = await httpClient.PostAsync("https://www.virustotal.com/vtapi/v2/file/scan", multi);

            var responseContent = await response.Content.ReadAsStringAsync();
            var scan = JsonSerializer.Deserialize<Scan>(responseContent);

            return scan;
        }

        private HttpContent CreateApiPart()
        {
            HttpContent content = new StringContent("c8a108d7596029407e7df11a333829607d5fed152a5888c843faac32eff25508");
            content.Headers.ContentDisposition = new("form-data")
            {
                Name = "\"apikey\""
            };

            return content;
        }

        private HttpContent CreateResource(string resource)
        {
            HttpContent content = new StringContent("resource");
            content.Headers.ContentDisposition = new("form-data")
            {
                Name = "\"apikey\""
            };

            return content;
        }

        private HttpContent CreateFileContent(Stream stream, string fileName, bool includeSize = true)
        {
            StreamContent fileContent = new StreamContent(stream);

            var disposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            disposition.Name = "\"file\"";
            disposition.FileName = "\"" + fileName + "\"";

            if (includeSize)
                disposition.Size = stream.Length;

            fileContent.Headers.ContentDisposition = disposition;
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            return fileContent;
        }
    }
}
