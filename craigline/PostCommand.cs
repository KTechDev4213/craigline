using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    internal class PostCommand:AsyncCommand<PostCommand.Settings>
    {
        CraigClient client = new CraigClient();
        public class Settings:CommandSettings
        {
            [CommandArgument(0, "<title>")]
            public string Title { get; init; }
            [CommandArgument(1, "<description>")]
            public string Description { get; init; }
            [CommandArgument(2, "<price>")]
            public decimal Price { get; init; }
        }
        public PostCommand()
        {
            client = new CraigClient();
        }
        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            if (!client.CheckAuthStatus())
            {
                Utils.NotLoggedInMessage();
                return 1;
            }
            var response = await client.Post(new Post("1", "3", settings.Title, settings.Description, settings.Price));
            return response;
        }
    }
}
