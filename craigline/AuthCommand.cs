using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace craigline
{
    internal class AuthCommand : AsyncCommand<AuthCommand.Settings>
    {

        public class AuthObject
        {
            public string device_code { get; set; }
            public string user_code { get; set; }
            public string verification_uri { get; set; }
            public int expires_in { get; set; }
            public int interval { get; set; }
            public string verification_uri_complete { get; set; }
        }

        public class Settings : CommandSettings
        {
            /*
            [CommandArgument(0, "<username>")]
            public string Username { get; set; }
            [CommandArgument(1, "<password>")]
            public string Password { get; set; }
            */
        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://dev-8att7jypkdqyxipd.us.auth0.com/oauth/device/code", new StringContent("client_id=qJbpzs7386MGCH4kdWEvvtdQL2H0DECk&scope=offline_access+openid+profile", Encoding.UTF8, "application/x-www-form-urlencoded"));
            if(!response.IsSuccessStatusCode)
            {
                return 1;
            }
            AuthObject authObject = System.Text.Json.JsonSerializer.Deserialize<AuthObject>(await response.Content.ReadAsStringAsync())!;
            Console.WriteLine($"Please go to the following URL and enter the code to authenticate: {authObject.verification_uri_complete} and enter in: {authObject.device_code}");
            //Todo: Poll for token
            return 0;
        }
    }
}