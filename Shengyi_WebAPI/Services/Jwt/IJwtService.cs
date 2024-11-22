namespace Shengyi_WebAPI.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateJwtStr(int id,string name,string phone);
        bool VerifyToken(string? token);
    }
}
