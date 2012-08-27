using System.Collections.Generic;
using System.Linq;

namespace HeynsLibrary.Attributes.Messages
{
    public class MessageRetriever
    {
        public static IEnumerable<KeyValuePair<string, string>>
            GetMessages(BaseMessageAttribute typeWithMessage)
        {
            var classLevelAttributes = typeWithMessage
                .GetType()
                .GetCustomAttributes(true)
                .Where(x => x is BaseMessageAttribute);

            if (classLevelAttributes.Any())
                foreach (BaseMessageAttribute attribute in classLevelAttributes)
                {
                    yield return 
                        new KeyValuePair<string, string>
                            (typeWithMessage.GetType().Name, attribute.GetMessage());
                }

            var properties = typeWithMessage.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property
                    .GetCustomAttributes(true)
                    .Where(x => x is BaseMessageAttribute);

                if (!attributes.Any())
                    continue;

                foreach (BaseMessageAttribute attribute in attributes)
                {
                    yield return 
                        new KeyValuePair<string, string>
                            (property.Name, attribute.GetMessage());
                }
            }
        }
    }
}