using Core.Domain.Entities;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces.UserProfile
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
    }
}
