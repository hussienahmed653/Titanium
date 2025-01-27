namespace Titanium2.Application
{
    public interface IJwtServices
    {
        public Task<string> GenerateToken(UserDTO userDTO);
    }
}
