using SekChallenge.API.DTOs;
using SekChallenge.API.Entities;

namespace SekChallenge.API.Services.Interfaces
{
    public interface IFileService
    {
        Task<List<Scan>> SendFilesAsync(SendFilesDTO sendFilesDTO);
        Task<bool> FileReportAsync(string resource);
    }
}
