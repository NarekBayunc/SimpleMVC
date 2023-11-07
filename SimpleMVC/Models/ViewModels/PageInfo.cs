namespace SimpleMVC.Models.ViewModels
{
    public class PageInfo
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int TotalItems { get; set; } 
        public int TotalPages   
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
    public class IndexViewModel<T>
    {
        public IEnumerable<T>? Entities { get; set; }
        public PageInfo? PageInfo { get; set; }
    }
}
