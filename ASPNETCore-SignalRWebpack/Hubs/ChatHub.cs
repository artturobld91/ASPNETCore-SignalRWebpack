using ASPNETCore_SignalRWebpack.Interface;
using Microsoft.AspNetCore.SignalR;

namespace ASPNETCore_SignalRWebpack.Hubs
{
    /// <summary>
    /// This code code broadcasts received messages to all connected users once the server receives them. 
    /// It's unnecessary to have a generic on method to receive all the messages. 
    /// A method named after the message name is enough.
    /// In this example, the TypeScript client sends a message identified as newMessage. 
    /// The C# NewMessage method expects the data sent by the client. 
    /// A call is made to SendAsync on Clients.All. 
    /// The received messages are sent to all clients connected to the hub.
    /// </summary>
    public class ChatHub : Hub<IChatHub>
    {
        public async Task BroadcastMessage(string username, string message) =>
            await Clients.All.SendAsync("messageReceived", message);

    }
}
