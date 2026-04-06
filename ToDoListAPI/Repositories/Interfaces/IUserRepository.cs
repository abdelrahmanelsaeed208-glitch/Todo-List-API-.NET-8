using Microsoft.AspNetCore.Identity;

namespace ToDoListAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
