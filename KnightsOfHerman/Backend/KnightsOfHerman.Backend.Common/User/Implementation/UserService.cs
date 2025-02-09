using KnightsOfHerman.Backend.Common.Config;
using KnightsOfHerman.Backend.Common.Database.Interfaces.User;
using KnightsOfHerman.Backend.Common.User.Abstract;
using KnightsOfHerman.Common.Types;
using KnightsOfHerman.Common.User;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using ProfanityGuard.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Backend.Common.User.Implementation
{
    /// <summary>
    /// Implementation of IUserService
    /// </summary>
    public class UserService : IUserService
    {
        JWTConfig _config;
        IUserDB _userDB;

        public UserService(JWTConfig config, IUserDB userdb)
        {
            _config = config;
            _userDB = userdb;
        }

        public async Task<TryResult<string>> TryCreateUserAsync(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return TryResult<string>.Fail();
            }

            if (ProfanityChecker.Check(model.Username))
            {
                return TryResult<string>.Fail("Username contains profanity");
            }

            if(!new EmailAddressAttribute().IsValid(model.Email))
            {
                return TryResult<string>.Fail("Invalid Email");
            }

            var salt = GenerateSalt();
            var e_pwd = EncryptPassword(model.Password, salt);
            var result = await _userDB.TryCreateUserAsync(model.Username, model.Email, e_pwd, salt);

            if (result.IsSuccess)
            {
                var user = new UserModel()
                {
                    Username = model.Username,
                    Email = model.Email,
                    ID = result.Value
                };

                var token = CreateToken(user);

                return TryResult<string>.Success(token);
            }
            else
            {
                return TryResult<string>.Fail();
            }
        }

        public async Task<TryResult<string>> TryGetJWTTokenAsync(LoginModel credentials)
        {
            //Add authenication here

            var result = await _userDB.TryGetUserAuthInfo(credentials.Email);
            if (result.IsSuccess)
            {
                var auth = result.Value;
                if (auth != null)
                {
                    var temp_pwd = EncryptPassword(credentials.Password, auth.Salt);

                    if (temp_pwd == auth.EncryptedPassword)
                    {
                        var user = new UserModel()
                        {
                            Username = auth.Username,
                            ID = auth.UserID,
                            Email = credentials.Email
                        };

                        var token = CreateToken(user);

                        return TryResult<string>.Success(token);
                    }

                }
            }
            return TryResult<string>.Fail();
        }

        /// <summary>
        /// Password password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string EncryptPassword(string password, string salt)
        {
            var pwd = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA256, 10000, 128));
            return pwd;
        }

        /// <summary>
        /// rng bitch
        /// </summary>
        /// <returns></returns>
        string GenerateSalt()
        {
            return RandomNumberGenerator.GetHexString(128);
        }

        /// <summary>
        /// Vtuber model
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] {
                        new Claim("id", user.ID.ToString()),
                        new Claim("email", user.Email),
                        new Claim("username", user.Username)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<List<string>> CheckUsername(string username)
        {
            var list = new List<string>();
            //First Check if username exists
            var exists = await _userDB.CheckUsernameExists(username);
            if(exists)
            {
                list.Add("Username already exists");
                return list;
            }

            if (ProfanityChecker.Check(username))
            {
                list.Add("Username contains profanity");
            }

            //Other checks here.

            return list;
        }

        public async Task<List<string>> CheckEmail(string email)
        {
            var list = new List<string>();

            //First check if format is correct
            var correctFormat = new EmailAddressAttribute().IsValid(email);
            if (!correctFormat)
            {
                list.Add("Invalid email format");
            }

            //First Check if username exists
            var exists = await _userDB.CheckEmailExists(email);
            if (exists)
            {
                list.Add("Email already exists");
                return list;
            }

            //Other checks here.

            return list;
        }

        public async Task<bool> LookForUsername(string username)
        {
            return await _userDB.CheckUsernameExists(username);
        }
    }
}
