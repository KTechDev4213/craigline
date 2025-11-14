using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    internal class ViewCommand : Command<ViewCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[id]")]
            public string Id { get; set; }
        }
        public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            AnsiConsole.MarkupLine($"Viewing post with ID: [green]{settings.Id}[/]");
            return 0;
        }
    }
}
