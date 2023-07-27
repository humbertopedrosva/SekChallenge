using Microsoft.AspNetCore.Mvc;
using SekChallenge.API.DTOs;
using SekChallenge.API.Entities;
using SekChallenge.API.Infra.Repositories.Interfaces;
using SekChallenge.API.Services.Interfaces;

namespace SekChallenge.API.Controllers
{
    [ApiController]
    [Route("api/v1/file")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IScanRepository _scanRepository;

        public FileController(IFileService fileService, IScanRepository scanRepository)
        {
            _fileService = fileService;
            _scanRepository = scanRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<Scan>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendFile([FromQuery] SendFilesDTO sendFilesDTO)
        {
            var scans = await _fileService.SendFilesAsync(sendFilesDTO);

            return Created(nameof(FileController), scans);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Scan>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScans()
        {
            var result = await _scanRepository.GetAsync();
            return Ok(result);
        }
    }
}
