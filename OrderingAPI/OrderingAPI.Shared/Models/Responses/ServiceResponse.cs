namespace OrderingAPI.Shared.Models.Responses
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public T? Data { get; set; }
        public string TraceID { get; set; }
    }

    public class ServiceResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public string TraceID { get; set; }
    }
}
