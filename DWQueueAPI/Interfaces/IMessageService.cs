namespace DWQueueAPI.Interfaces
{
    public interface IMessageService
    {
        
            void PublishMessage<T>(string queueName, T message);
        
    }
}
