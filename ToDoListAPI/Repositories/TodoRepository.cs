using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Common;
using ToDoListAPI.Data;
using ToDoListAPI.DTOs.Todo;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories.Interfaces;

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
            var page = Math.Max(query.Page, 1);
            var limit = Math.Clamp(query.Limit, 1, 100);

            var q = _context.Todos
                .AsNoTracking()
                .Where(t => t.UserId == userId);

            if (!string.IsNullOrWhiteSpace(query.Search))
                q = q.Where(t => t.Title.Contains(query.Search) ||
                                (t.Description != null && t.Description.Contains(query.Search)));

            if (query.IsCompleted.HasValue)
                q = q.Where(t => t.IsCompleted == query.IsCompleted.Value);

            var totalCount = await q.CountAsync();

            var items = await q
                .OrderByDescending(t => t.CreatedAt)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            return new PagedResult<Todo>(items, totalCount, page, limit);
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
