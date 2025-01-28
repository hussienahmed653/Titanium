namespace Titanium2.Application.Interfaces.JwtInterfaces
{
    public interface IJwtServices
    {
        public Task<string> GenerateToken(UserDTO userDTO);
    }
}
