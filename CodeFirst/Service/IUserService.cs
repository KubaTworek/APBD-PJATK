using CodeFirst.Models;

namespace CodeFirst.Service
{
    public interface IUserService
    {
        object GenerateJwt(LoginRequest request);
        object RefreshToken(string refreshToken);
        void RegisterUser(NewUserRequest request);
    }
}
