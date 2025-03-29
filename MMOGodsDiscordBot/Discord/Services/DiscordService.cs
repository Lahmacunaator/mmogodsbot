using DSharpPlus;

namespace Discord.Services;

public class DiscordService(DiscordBot discordBot)
{
    public DiscordBot GetDiscordBot() => discordBot;

    public DiscordClient GetClient()
    {
        return discordBot.Client;
    }

    public async Task SendMessage(string message)
    {
        var client = GetClient();
    }
}