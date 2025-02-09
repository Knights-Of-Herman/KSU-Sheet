using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.User
{
    /// <summary>
    /// Class that represents a JWT Token
    /// </summary>
    public class JwtToken
    {
        public string Token { get; set; } = "";
        public JwtToken(string token)
        {
            Token = token;
        }

        public JwtToken() { }

        /// <summary>
        /// Parses a JWT Token's permissions from its string representation
        /// </summary>
        /// <param name="tokenstr"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static bool TryParseToken(string tokenstr, out IEnumerable<Claim>? claims)
        {
            var handler = new JsonWebTokenHandler();
            if (handler.CanReadToken(tokenstr))
            {
                var token = handler.ReadJsonWebToken(tokenstr);


                if (token.TryGetClaim("id", out _) &&
                        token.TryGetClaim("username", out _) &&
                        token.TryGetClaim("email", out _))
                {
                    claims = token.Claims;
                    return true;
                }
            }
            claims = default;
            return false;
        }

        /// <summary>
        /// Parses a JWT Token's permissions from its Object representation
        /// </summary>
        /// <param name="jwt"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static bool TryParseToken(JwtToken jwt, out IEnumerable<Claim>? claims) => TryParseToken(jwt.Token, out claims);
    }


}
