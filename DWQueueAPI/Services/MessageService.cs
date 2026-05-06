using System.Reflection.Metadata.Ecma335;
using DWQueueAPI.Interfaces;

namespace DWQueueAPI.Services
{
    public class MessageService : IMessageService
    {
        public string GetHello() => "Hello from MessageService!";
    }
}
