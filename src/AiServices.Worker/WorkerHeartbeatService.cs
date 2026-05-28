namespace AiServices.Worker;

/// <summary>
/// Worker nền cho document sync, embedding sync, agent jobs sau này.
/// Giai đoạn 1 chưa chạy job thật, chỉ giữ project đúng kiến trúc để không phải tách nhánh sau.
/// </summary>
public sealed class WorkerHeartbeatService : BackgroundService
{
    private readonly ILogger<WorkerHeartbeatService> _logger;

    public WorkerHeartbeatService(ILogger<WorkerHeartbeatService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("AiServices.Worker started. No background job is enabled in phase 1.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}
