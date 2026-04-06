//using System.Linq.Dynamic.Core;
using ToDoListAPI.Common;
using ToDoListAPI.DTOs.Todo;

namespace ToDoListAPI.Services.Interface
{
    public interface ITodoService
    {
        Task<Result<TodoDto>> CreateAsync(CreateTodoDto dto, string userId);
        Task<PagedResult<TodoDto>> GetAllAsync(string userId, TodoPagination query);
        Task<Result<TodoDto>> UpdateAsync(int id, UpdateTodoDto dto, string userId);
        Task<Result> DeleteAsync(int id, string userId);



    }
}
