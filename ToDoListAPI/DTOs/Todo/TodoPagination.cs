namespace ToDoListAPI.DTOs.Todo
{
    public class TodoPagination
    {
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public string? Search { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
