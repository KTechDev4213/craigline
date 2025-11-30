using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    public class CraigClient
    {
        private bool authenticated;
        public CraigClient(ITokenStore tokenStore) 
        { 
            var token = tokenStore.GetToken();
            if(token is null)
            {
                authenticated = false;
                return;
            }
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.access_token}");
            authenticated = true;
        }
        HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://craigline/api/")
        };
        public bool CheckAuthStatus()
        {
            return authenticated;
        }
        public async Task<int> Post(Post product)
        {
            Console.WriteLine("Preparing to post product to Craigslist...");
            var response = await client.PostAsync("/product", JsonContent.Create(product));
            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //get new access token from refresh token
                //if that fails, return Utils.NotLoggedIn ...
            }
            else if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to post product: {response.StatusCode}");
            }
            Console.WriteLine($"Posting to Craigslist: {product.Name} - {product.Description}");
            return 0;
        }
        public async Task<Post[]> Search(string Term)
        {
            var response = await client.GetAsync($"/search?term={Term}");
            if (!response.IsSuccessStatusCode)
            {

            }
            return new Post[] { new Post("42", "69", "Jon Snow", "THE GOAT", 67)};
        }
    }
}
public record Post(string Id, string UserId, string Name, string Description, decimal Price);
record User(string Id, string UserName, string Email);
