using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    internal class SearchCommand:Command<SearchCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[term]")]
            public string Term { get; set; }
        }
        public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            AnsiConsole.MarkupLine($"Hello, [blue]{settings.Term}[/]");
            return 0;
        }
    }
}
