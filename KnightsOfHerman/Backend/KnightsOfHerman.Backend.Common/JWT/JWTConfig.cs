using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.Config
{
    public class JWTConfig
    {
        /// <summary>
        /// Will move the real key to a secure place in full production
        /// </summary>
        public string JWTSecret => "ZcTYErxwEva9MegMMa_dK0YChZwYQ7bL7geOs-He48Y=";
    }
}
