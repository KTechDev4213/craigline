using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    public class MessageSellerCommand:AsyncCommand<MessageSellerCommand.Settings>
    {
        CraigClient client = new CraigClient();
        public class Settings:CommandSettings
        {
            
        }
        public override Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            if (client.CheckAuthStatus())
            {
                Console.WriteLine("MessageSeller command executed.");
                return Task.FromResult(0);
            }
                
            else
            {
                Utils.NotLoggedInMessage();
                return Task.FromResult(1);
            }
        }
    }
}
