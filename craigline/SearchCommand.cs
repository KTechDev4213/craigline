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
        private CraigClient _client;
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "[term]")]
            public string Term { get; set; }
            [CommandArgument(1, "[sortby]")]
            public string SortBy { get; set; }
        }
        public SearchCommand(CraigClient client)
        {
            _client = client;
        }
        public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            _client.Search(settings.Term);
            AnsiConsole.MarkupLine($"Hello, [blue]{settings.Term}[/]");
            return 0;
        }
    }
}
