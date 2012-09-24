#region Using 

using System;
using System.IO;

#endregion


namespace Kata5
{
    public interface ISubscriberCreator
    {
        Subscriber CreateSubscriber(TextWriter tw, IPublisher publisher);
    }

    public class SubscriberCreator : ISubscriberCreator
    {
        public SubscriberCreator(IPublisher publisher)
        {
            if (publisher==null)
            {
                throw new NullReferenceException("Publisher should not be null");
            }
        }

        public Subscriber CreateSubscriber(TextWriter tw, IPublisher publisher)
        {
            return new Subscriber(publisher, tw);
        }
    }
}
