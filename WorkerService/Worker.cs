using WorkerService.Application.Interface;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IEmail _email;
        private readonly IHttpService _httpservice;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public Worker(ILogger<Worker> logger, IEmail email, IHttpService httpservice, IHostApplicationLifetime applicationLifetime)
        {
            _logger = logger;
            _email = email;
            _httpservice = httpservice;
            _applicationLifetime = applicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var statusSite = await _httpservice.CheckStatusSite("https://github.com/MatheusSanches02");

                if(statusSite != System.Net.HttpStatusCode.OK) 
                {
                    _email.SendEmail("ma12sanches@gmail.com", "Worker Service Teste", $"Site is not running at: {DateTimeOffset.Now}");
                    _applicationLifetime.StopApplication();
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}