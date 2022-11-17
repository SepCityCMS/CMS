namespace SepCityCMS.Models.API
{
    public class SearchResponse<T>
    {
        public long? RecordCount { get; set; }
        public long? TotalPages { get; set; }
        public List<T> Results { get; set; }
    }
}
