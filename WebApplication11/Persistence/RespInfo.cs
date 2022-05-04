namespace WebApplication11.Persistence
{
    public class RespInfo
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public RespInfo(bool success, string message)
        {
            Message = message;
            Success = success;
        }
    }
}
