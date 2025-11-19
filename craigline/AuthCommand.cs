using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace craigline
{
    internal class AuthCommand: AsyncCommand<AuthCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandArgument(0, "<username>")]
            public string Username { get; set; }
            [CommandArgument(1, "<password>")]
            public string Password { get; set; }
        }

        public override Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            
            return Task.FromResult(0);
        }
    }
}
