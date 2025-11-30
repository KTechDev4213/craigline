using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
        { }
        ITokenStore tokenStore;
        public AuthCommand(ITokenStore tokenStore)
        {
            this.tokenStore = tokenStore;
        }
        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://dev-8att7jypkdqyxipd.us.auth0.com/oauth/device/code", new StringContent("client_id=qJbpzs7386MGCH4kdWEvvtdQL2H0DECk&scope=offline_access+openid+profile", Encoding.UTF8, "application/x-www-form-urlencoded"));
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error initiating device authorization flow.");
                return 1;
            }
            AuthObject authObject = System.Text.Json.JsonSerializer.Deserialize<AuthObject>(await response.Content.ReadAsStringAsync())!;
            if(authObject == null)
            {
                return 5;
            }
            Console.WriteLine($"Please go to the following URL and enter the code to authenticate: {authObject.verification_uri} and enter in: {authObject.user_code}");
            HttpResponseMessage tokenResponse;
            Console.WriteLine("Press Any Key After You Authenticate");
            Console.Read();
            do
            {
                Thread.Sleep(authObject.interval);
                tokenResponse = await client.PostAsync("https://dev-8att7jypkdqyxipd.us.auth0.com/oauth/token", new StringContent($"grant_type=urn:ietf:params:oauth:grant-type:device_code&device_code={authObject.device_code}&client_id=qJbpzs7386MGCH4kdWEvvtdQL2H0DECk", Encoding.UTF8, "application/x-www-form-urlencoded")); await client.PostAsync("https://dev-8att7jypkdqyxipd.us.auth0.com/oauth/token", new StringContent($"grant_type=urn:ietf:params:oauth:grant-type:device_code&device_code={authObject.device_code}&client_id=qJbpzs7386MGCH4kdWEvvtdQL2H0DECk", Encoding.UTF8, "application/x-www-form-urlencoded"));
            }
            while (!tokenResponse.IsSuccessStatusCode);
            AccessToken? token = await tokenResponse.Content.ReadFromJsonAsync<AccessToken>();
            if(token == null)
            {
                Console.WriteLine("Received null access token somehow.");
                return 5;
            }
            if(!tokenStore.SaveToken(token))
            {
                Console.WriteLine("Error saving access token.");
                return 6;
            }
            Console.WriteLine("Authentication successful! Access token saved.");
            return 0;
        }
    }
}