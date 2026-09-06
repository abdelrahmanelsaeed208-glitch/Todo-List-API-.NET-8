using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.DTOs.Todo
{
    public class UpdateTodoDto
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
