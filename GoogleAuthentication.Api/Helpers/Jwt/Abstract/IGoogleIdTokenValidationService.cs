using GoogleAuthentication.Api.Dto;

namespace GoogleAuthentication.Api.Helpers.Jwt.Abstract
{
    public interface IGoogleIdTokenValidationService
    {
        public Task<Token> ValidateIdTokenAsync(Login model);
    }
}
