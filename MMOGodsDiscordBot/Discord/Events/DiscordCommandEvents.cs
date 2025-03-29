using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using Microsoft.Extensions.Logging;

namespace Discord.Events
{
    public static class DiscordCommandEvents
    {
        /// <summary>
        /// Logs succesfully executed commands.
        /// </summary>
        /// <param name="sender">Service that executed the command.</param>
        /// <param name="e">Execution arguments.</param>
        public async static Task OnCommandExecutedAsync(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            sender.Client.Logger.LogInformation($"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}'", DateTime.Now);
        }

        /// <summary>
        /// Logs errored commands.
        /// </summary>
        /// <param name="sender">Service that executed the command.</param>
        /// <param name="e">Execution error arguments.</param>
        /// <returns></returns>
        public async static Task OnCommandErroredAsync(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            sender.Client.Logger.LogError($"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);
        }
    }
}