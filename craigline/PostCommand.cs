using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    internal class PostCommand:Command<PostCommand.Settings>
    {
        public class Settings:CommandSettings
        {

        }
        public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            AnsiConsole.MarkupLine($"Posting not implemented yet");
            return 0;
        }
    }
}
