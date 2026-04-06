using AutoMapper;
using Org.BouncyCastle.Crypto;
//using System.Linq.Dynamic.Core;
using ToDoListAPI.Common;
using ToDoListAPI.DTOs.Todo;
using ToDoListAPI.Models;
using ToDoListAPI.Repositories.Interfaces;
using ToDoListAPI.Services.Interface;

namespace ToDoListAPI.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<Result<TodoDto>> CreateAsync(CreateTodoDto dto, string userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return Result<TodoDto>.Failure("Title is required");

            var todo = _mapper.Map<Todo>(dto);
            todo.UserId = userId;

            await _todoRepository.AddAsync(todo);

            return Result<TodoDto>.Success(_mapper.Map<TodoDto>(todo));
        }

        public async Task<PagedResult<TodoDto>> GetAllAsync(string userId, TodoPagination query)
        {
            var todos = await _todoRepository.GetAllAsync(userId, query);
            var dtos = _mapper.Map<List<TodoDto>>(todos.CurrentPage);

            return new PagedResult<TodoDto>(dtos, query.Page,query.Limit );
        }

        public async Task<Result<TodoDto>> UpdateAsync(int id, UpdateTodoDto dto, string userId)
        {
            var todo = await _todoRepository.GetByIdAsync(id, userId);
            if (todo == null)
                return Result<TodoDto>.Failure("Todo not found or you don't have permission");

            _mapper.Map(dto, todo);
            todo.UpdatedAt = DateTime.UtcNow;

            await _todoRepository.UpdateAsync(todo);

            return Result<TodoDto>.Success(_mapper.Map<TodoDto>(todo));
        }

        public async Task<Result> DeleteAsync(int id, string userId)
        {
            var todo = await _todoRepository.GetByIdAsync(id, userId);
            if (todo == null)
                return Result.Failure("Todo not found or you don't have permission");

            await _todoRepository.DeleteAsync(todo);
            return Result.Success();
        }
    }
    }


