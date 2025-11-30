using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craigline
{
    public interface ITokenStore
    {
        public bool SaveToken(AccessToken token);
        public AccessToken? GetToken();
    }
}
