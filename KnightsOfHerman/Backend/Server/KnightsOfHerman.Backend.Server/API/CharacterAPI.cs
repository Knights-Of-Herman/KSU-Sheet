using KnightsOfHerman.Backend.Common.JWT;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Common.Sanctum.Abstract.Character;
using KnightsOfHerman.Common.Sanctum.Communication.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KnightsOfHerman.Backend.Server.API
{
    /// <summary>
    /// API in charge of HTTP Post and Get for Characters
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class CharacterAPIController : ControllerBase
    {
        
        ICharacterService _character;

        JWTService _jwt;

        public CharacterAPIController(ICharacterService characterService, JWTService jwt)
        {
            _character = characterService;
            _jwt = jwt;
        }

        /// <summary>
        /// Gets a list of characters a user has
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [Authorize]
        [HttpPost("getprofiles")]
        public async Task<IActionResult> GetProfiles()
        {
            if (_jwt.TryParseUserID(HttpContext.User.Identity, out int userID))
            {
                var result = await _character.GetProfiles(userID);
                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                else
                {
                    return StatusCode(500, result.ErrorMessage);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Gets a character object
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("getcharacter")]
        public async Task<IActionResult> GetCharacter([FromBody] int characterID)
        {
            if (_jwt.TryParseUserID(HttpContext.User.Identity, out int userID))
            {
                var result = await _character.GetCharacterDTO(userID, characterID);
                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                } else
                {
                    return StatusCode(500, result.ErrorMessage);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Deletes the character
        /// </summary>
        /// <param name="characterID"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("deletecharacter")]
        public async Task<IActionResult> DeleteCharacter([FromBody] int characterID)
        {
            if (_jwt.TryParseUserID(HttpContext.User.Identity, out int userID))
            {
                var result = await _character.DeleteCharacter(userID, characterID);
                if (result.IsSuccess)
                {
                    return Ok();
                }
            }
            return Unauthorized();

        }

        /// <summary>
        /// Creates a blank character
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("createblankcharacter")]
        public async Task<IActionResult> CreateBlankCharacter()
        {
            if (_jwt.TryParseUserID(HttpContext.User.Identity, out int userID))
            {
                var result = await _character.CreateCharacter(userID);
                if (result.IsSuccess)
                {
                    return Ok(result.Value);
                }
                else
                {
                    //Some type of error, will handle more later
                    return StatusCode(500, result.ErrorMessage);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Shares a character
        /// </summary>
        /// <param name="sharedata"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("sharecharacter")]
        public async Task<IActionResult> ShareCharacter([FromBody] CharacterShareDTO sharedata)
        {
            if (_jwt.TryParseUserID(HttpContext.User.Identity, out int userID))
            {

                var result = await _character.ShareCharacter(userID, sharedata.ShareUser, sharedata.CharacterID, sharedata.Access);

                if(result == CharacterShareResult.Success)
                {
                    return Ok();
                }
                if(result == CharacterShareResult.UserNotFound || result == CharacterShareResult.CharacterNotFound)
                {
                    return NotFound();
                }
                if(result == CharacterShareResult.NotAuthorized)
                {
                    return Unauthorized();
                } else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Gets the user's a character is shared with
        /// </summary>
        /// <param name="characterid"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("getsharepermissions")]
        public async Task<IActionResult> GetSharePermissions([FromBody] int characterid)
        {
            if (_jwt.TryParseUserID(HttpContext.User.Identity, out int userID))
            {
                var result = await _character.GetCharacterSharePermissions(userID, characterid);   
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Unshares a character form a user
        /// </summary>
        /// <param name="characterid"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("unsharecharacter")]
        public async Task<IActionResult> UnshareCharacter([FromBody] int characterid)
        {
            if (_jwt.TryParseUserID(HttpContext.User.Identity, out int userID))
            {
                var result = await _character.UnshareCharacter(userID, characterid);
                if (result.IsSuccess)
                {
                    return Ok();
                } else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

