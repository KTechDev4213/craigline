using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    internal class CraigClient
    {
        FileStream settingsFile = File.OpenWrite(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "appsettings.json"));
        HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://craigline/api/")
        };
        public bool CheckAuthStatus()
        {
            //check if there's a stored auth token
            //if not:
            return false;
        }
        public string GetAccessToken()
        {
            //read access token from persistent storage
            return "";
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
record Post(string Id, string UserId, string Name, string Description, decimal Price);
record User(string Id, string UserName, string Email);
