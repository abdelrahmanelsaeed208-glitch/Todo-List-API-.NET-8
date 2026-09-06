using ToDoListAPI.Common;
using ToDoListAPI.DTOs.Todo;
using ToDoListAPI.Models;

namespace ToDoListAPI.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo?> GetByIdAsync(int id, string userId);
        Task<PagedResult<Todo>> GetAllAsync(string userId, TodoPagination query);
        Task AddAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task DeleteAsync(Todo todo);
    }
}
