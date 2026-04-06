using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using ToDoListAPI.Data;
using ToDoListAPI.DTOs.Todo;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories.Interfaces;
//using ToDoListAPI.Common;


namespace ToDoListAPI.Repositories
{
    public class TodoRepository: ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Todo?> GetByIdAsync(int id, string userId)
        {
            return await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<PagedResult<Todo>> GetAllAsync(string userId, TodoPagination query)
        {
            var q = _context.Todos.Where(t => t.UserId == userId);

            if (!string.IsNullOrEmpty(query.Search))
                q = q.Where(t => t.Title.Contains(query.Search) ||
                                (t.Description != null && t.Description.Contains(query.Search)));

            if (query.IsCompleted.HasValue)
                q = q.Where(t => t.IsCompleted == query.IsCompleted.Value);

            var totalCount = await q.CountAsync();

            var items = await q
                .OrderByDescending(t => t.CreatedAt)
                .Skip((query.Page - 1) * query.Limit)
                .Take(query.Limit)
                .ToListAsync();

            return new PagedResult<Todo>();
            
        }

        public async Task AddAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Todos.Update(todo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Todo todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

    }
}
