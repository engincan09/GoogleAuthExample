using Google.Apis.Auth;
using GoogleAuthentication.Api.Dto;
using GoogleAuthentication.Api.Entities.Users;
using GoogleAuthentication.Api.Helpers.Jwt.Abstract;
using Microsoft.AspNetCore.Identity;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace GoogleAuthentication.Api.Helpers.Jwt.Concrete
{
    public class GoogleIdTokenValidationService : IGoogleIdTokenValidationService
    {
        readonly IConfiguration _configuration;
        readonly UserManager<User> _userManager;
        readonly ITokenHandler _tokenHandler;
        public GoogleIdTokenValidationService(
            IConfiguration configuration,
            UserManager<User> userManager,
            ITokenHandler tokenHandler)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> ValidateIdTokenAsync(Login model)
        {
            ValidationSettings? settings = new ValidationSettings()
            {
                Audience = new List<string>()
                    { _configuration["ExternalLogin:Google-Client-Id"] }
            };
            Payload payload = await ValidateAsync(model.IdToken, settings);

            UserLoginInfo userLoginInfo = new(model.Provider, payload.Subject, model.Provider);
            User user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new() { Id = Guid.NewGuid().ToString(), Email = payload.Email, UserName = payload.Email, Provider = model.Provider };
                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, userLoginInfo);
            else
                throw new Exception("Invalid external authentication.");

            Token token = _tokenHandler.CreateAccessToken(5);
            return token;
        }
    }
}
