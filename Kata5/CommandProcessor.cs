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
        private readonly TextReader _textReader;
        public CommandProcessor(IPublisher publisher, ISubscriberCreator subscriberCreator, TextReader textReader)
        {
            _publisher = publisher;
            _subscriberCreator = subscriberCreator;
            _textReader = textReader;
        }
        public void SubscribeAndReadText()
        {
            string s;
            while ((s = _textReader.ReadLine()) != null)
            {
                if (s == "add")
                {
                    _subscriberCreator.CreateSubscriber(Console.Out);
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
