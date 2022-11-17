namespace SepCityCMS.Models.API
{
    public class RecordResponse<T>
    {
        public int? Status { get; set; }
        public string? Message { get; set; }
        public T Record { get; set; }
    }
}
