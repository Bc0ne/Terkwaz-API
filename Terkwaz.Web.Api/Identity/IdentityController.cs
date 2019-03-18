namespace Terkwaz.Web.Api.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using Pharmatolia.API.Models;
    using Terkwaz.Api.Models.Identity;
    using Terkwaz.Domain.User;

    [ApiController]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IdentityConfig _identityConfig;

        public IdentityController(IUserRepository userRepository, IdentityConfig identityConfig)
        {
            _userRepository = userRepository;
            _identityConfig = identityConfig;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterationInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Your Data isn't valid."));
            }

            var userByEmail = await _userRepository.GetUserByEmailAsync(model.Email);

            if (userByEmail != null)
            {
                return BadRequest(new ApiError(400, HttpStatusCode.BadRequest.ToString(), "Email is already exist."));
            }

            var user = Domain.User.User.New(model.FullName, model.Email, model.PhotoUrl);

            await _userRepository.RegisterAsync(user, model.password);

            var userOutputModel = Mapper.Map<UserRegisterationOutputModel>(user);

            userOutputModel.Token = GenerateToken(user);

            return Ok(userOutputModel);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfig.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _identityConfig.Issuer,
                audience: _identityConfig.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_identityConfig.TokenExpiryInMinutes)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}