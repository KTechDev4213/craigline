using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    internal class PersistentData
    {
        private string path;
        public string AuthToken { get; set; }
        public PersistentData()
        {
            path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "craigline", "config.dat");
        }
        public void Save()
        {

        }
    }
}
