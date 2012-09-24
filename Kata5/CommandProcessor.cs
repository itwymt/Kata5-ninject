#region Using
using System;
using System.IO;
#endregion

namespace Kata5
{
    public class CommandProcessor
    {
        private readonly IPublisher _publisher;
        private readonly ISubscriberCreator _subscriberCreator;

        public TextReader TextReader { private get; set; }

        public CommandProcessor(IPublisher publisher, ISubscriberCreator subscriberCreator)
        {
            if (publisher == null)
            {
                throw new NullReferenceException("Publisher should not be null");
            }
            if (subscriberCreator == null)
            {
                throw new NullReferenceException("Subscriber creator should not be null");
            }
            _publisher = publisher;
            _subscriberCreator = subscriberCreator;
        }

        public void SubscribeAndReadText()
        {
            string s;
            while ((s = TextReader.ReadLine()) != null)
            {
                if (s.ToLower() == "add")
                {
                    _subscriberCreator.CreateSubscriber(Console.Out, _publisher);
                }
                else
                {
                    if (!String.IsNullOrEmpty(s))
                    {
                        _publisher.Publish(s);
                    }
                }
            }
        }
    }
}
