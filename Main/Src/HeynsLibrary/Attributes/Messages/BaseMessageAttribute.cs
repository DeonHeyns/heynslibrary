
namespace HeynsLibrary.Attributes.Messages
{
    public abstract class BaseMessageAttribute : BaseAttribute, IMessageAttribute
    {
        public abstract string GetMessage();
    }
}
