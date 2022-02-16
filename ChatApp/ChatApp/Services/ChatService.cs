using ChatApp.Mobile.Services.Interfaces;
using ChatApp.Services.Core;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Core
{
    public class ChatService: IChatService
    {
        private readonly HubConnection hubConnection;
        public ChatService()
        {
            hubConnection =new HubConnectionBuilder()
                .WithUrl($"{ServiceSettings.BaseAddress}/chathub", (opts) =>
                {
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (reqest, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
                .Build();
        }

        public async Task Connect(string userEmail)
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync("OnConnect", userEmail);
        }

        public async Task Disconnect()
        {
            await hubConnection.InvokeAsync("OnDisconnect");
            await hubConnection.StopAsync();
        }

        public async Task SendMessage(string userId, string message)
        {
            await hubConnection.InvokeAsync("SendPrivateMessage", userId, message);
        }

        public void ReceiveMessage(Action<string, string> GetMessageAndUser)
        {
            hubConnection.On("ReceivePrivateMessage", GetMessageAndUser);
        }
    }
}
