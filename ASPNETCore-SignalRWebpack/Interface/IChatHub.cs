namespace ASPNETCore_SignalRWebpack.Interface
{
    public interface IChatHub
    {
        Task SendAsync(string username, string message);
        Task BroadcastMessage(string username, string message);
    }
}
