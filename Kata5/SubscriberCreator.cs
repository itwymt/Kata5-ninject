#region Using 

using System;
using System.IO;

#endregion


namespace Kata5
{
    public interface ISubscriberCreator
    {
        Subscriber CreateSubscriber(TextWriter tw);
    }

    public class SubscriberCreator : ISubscriberCreator
    {
        private readonly IPublisher _publisher;
        public SubscriberCreator(IPublisher publisher)
        {
            if (publisher==null)
            {
                throw new NullReferenceException("Publisher should not be null");
            }
            _publisher = publisher;
        }

        public Subscriber CreateSubscriber(TextWriter tw)
        {
            return new Subscriber(_publisher, tw);
        }
    }
}
