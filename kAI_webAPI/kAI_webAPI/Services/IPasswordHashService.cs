namespace kAI_WebAPI.Services
{
    public interface IPasswordHasherService
    {
        (string Hash, string Salt) HashPassword(string plain);
        bool Verify(string plain, string hash, string salt);
    }
}