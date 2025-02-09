using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.User
{
    public static class Password
    {
        /// <summary>
        /// Checks a password's strength
        /// From Mudblazor EditForm 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static IEnumerable<string> CheckStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                yield return "Password is required!";
                yield break;
            }
            if (password.Length < 8)
                yield return "Password must be at least of length 8";
            if (!Regex.IsMatch(password, @"[A-Z]"))
                yield return "Password must contain at least one capital letter";
            if (!Regex.IsMatch(password, @"[a-z]"))
                yield return "Password must contain at least one lowercase letter";
            if (!Regex.IsMatch(password, @"[0-9]"))
                yield return "Password must contain at least one digit";
        }
    }
}
