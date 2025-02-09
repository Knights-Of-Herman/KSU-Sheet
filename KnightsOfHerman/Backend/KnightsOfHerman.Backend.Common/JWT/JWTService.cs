using System.Security.Claims;
using System.Security.Principal;

namespace KnightsOfHerman.Backend.Common.JWT
{
    /// <summary>
    /// Helper class for parsing JWT tokens
    /// </summary>
    public class JWTService
    {
        /// <summary>
        /// Parses a userid from a token
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TryParseUserID(IIdentity? identity, out int id)
        {
            if (identity != null)
            {
                var claimsIdentity = identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    var rawID = claimsIdentity.FindFirst("id")?.Value;
                    if (rawID != null && int.TryParse(rawID, out int tempid))
                    {
                        id = tempid;
                        return true;
                    }
                }
            }
            id = -1;
            return false;
        }
    }
}
