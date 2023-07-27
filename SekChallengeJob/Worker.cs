using SekChallenge.API.Infra.Repositories.Interfaces;
using SekChallenge.API.Services.Interfaces;

namespace SekChallengeJob
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IFileService _fileService;
        private readonly IScanRepository _scanRepository;

        public Worker(ILogger<Worker> logger, IFileService fileService, IScanRepository scanRepository)
        {
            _logger = logger;
            _fileService = fileService;
            _scanRepository = scanRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("O serviço está iniciando.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Rodando: {time}", DateTimeOffset.Now);

                var scans = await _scanRepository.GetScanUnfishedAsync();

                if(scans != null) 
                {
                    foreach(var scan in scans) 
                    {
                        var finished = await _fileService.FileReportAsync(scan.Resource);

                        if (finished)
                        {
                            scan.Finished = finished;
                            await _scanRepository.UpdateAsync(scan);
                        }
                    }
                    
                }
                await Task.Delay(1000, stoppingToken);//1segundo
            }
            _logger.LogInformation("O serviço está parando.");
        }
    }
}
