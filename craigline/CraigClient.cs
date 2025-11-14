using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    internal class CraigClient
    {
        public int Post(string Title, string Description)
        {
            Console.WriteLine($"Posting to Craigslist: {Title} - {Description}");
            return 0;
        }
        public string[] Search(string Term)
        {
            Console.WriteLine($"Searching Craigslist for: {Term}");
            return new string[] { "Post1", "Post2", "Post3" };
        }
    }
    internal class CraigsListPost
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
