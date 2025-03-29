using Discord;
using Discord.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<HostedDiscordService>();

var host = builder.Build();
host.Run();