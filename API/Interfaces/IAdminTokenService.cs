using API.Entities;

namespace API.Interfaces
{
    public interface IAdminTokenService
    {
        Task<string> CreateToken(Admin user);
    }
}