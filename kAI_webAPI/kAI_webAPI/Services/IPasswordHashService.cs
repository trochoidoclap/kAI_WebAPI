namespace kAI_WebAPI.Services
{
    public interface IPasswordHasherService
    {
        (string Hash, string Salt) HashPassword(string plain);
        //(string hash, string salt) HashPassword(object password);
        bool Verify(string plain, string hash, string salt);
    }
}