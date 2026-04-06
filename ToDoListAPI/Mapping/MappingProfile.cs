using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoListAPI.DTOs.Auth;
using ToDoListAPI.DTOs.Todo;
using ToDoListAPI.Models;
namespace ToDoListAPI.Mapping

{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Todo Mappings
            CreateMap<CreateTodoDto, Todo>();
            CreateMap<UpdateTodoDto, Todo>();
            CreateMap<Todo, TodoDto>();

            
        }
    }
}
