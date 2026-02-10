namespace OrderingAPI.Interfaces
{
    public interface IErrorLogRepository
    {
        Task LogError(string traceID, string message, string stackTrace);
    }
}
