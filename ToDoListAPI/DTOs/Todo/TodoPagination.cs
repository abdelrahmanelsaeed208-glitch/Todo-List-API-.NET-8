using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI.DTOs.Todo
{
    public class TodoPagination
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int Limit { get; set; } = 10;

        [MaxLength(100)]
        public string? Search { get; set; }

        public bool? IsCompleted { get; set; }
    }
}
