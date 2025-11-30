using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace craigline
{
    internal class TokenStore:ITokenStore
    {
        private string path;
        public TokenStore()
        {
            path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "craigline", "config.dat");
        }
        public bool SaveToken(AccessToken token)
        {
            try
            {
                string json = JsonSerializer.Serialize(token);
                File.WriteAllText(path, json);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
        public AccessToken? GetToken()
        {
            if(!File.Exists(path))
            {
                return null;
            }
            try
            {
                string json = File.ReadAllText(path);
                var token = JsonSerializer.Deserialize<AccessToken>(json);
                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}
