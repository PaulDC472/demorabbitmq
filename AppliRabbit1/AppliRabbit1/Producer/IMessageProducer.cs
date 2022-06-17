namespace AppliRabbit1.Producer
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
