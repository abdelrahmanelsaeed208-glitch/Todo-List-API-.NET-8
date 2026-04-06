namespace ToDoListAPI.Common
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }

        public PagedResult(List<T> data, int totalCount, int page)
        {
            Data = data;
            Total = totalCount;
            Page = page;
            
        }
    }
}
