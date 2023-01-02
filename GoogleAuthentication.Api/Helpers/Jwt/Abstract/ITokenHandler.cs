using GoogleAuthentication.Api.Dto;
using Newtonsoft.Json.Linq;

namespace GoogleAuthentication.Api.Helpers.Jwt.Abstract
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int minute);
    }
}
