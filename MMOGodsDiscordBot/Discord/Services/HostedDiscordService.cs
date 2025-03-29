using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Discord.Services;

/// <inheritdoc />
public class HostedDiscordService : BackgroundService
{
    private readonly DiscordBot _bot;

    /// <inheritdoc />
    public HostedDiscordService(DiscordBot discord)
    {
        _bot = discord;
    }

    /// <inheritdoc cref="BackgroundService.ExecuteAsync(CancellationToken)" />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bot.StartAsync(stoppingToken).ConfigureAwait(false);
    }
}
