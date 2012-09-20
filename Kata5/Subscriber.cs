#region

using System;
using System.IO;

#endregion


namespace Kata5
{
    public class Subscriber
    {
        private readonly TextWriter _textWriter;

        public Subscriber(IPublisher publisher, TextWriter textWriter)
        {
            if (publisher == null)
            {
                throw new NullReferenceException("Publisher should not be null");
            }

            if (textWriter == null)
            {
                throw new NullReferenceException("Text writer should not be null");
            }

            _textWriter = textWriter;

             publisher.WriteEvent += HandleWriteEvent;
        }

        private void HandleWriteEvent(object sender, WriteEventArgs eventArgs)
        {
            WriteText(eventArgs.Text);
        }

        private void WriteText(string s)
        {
            _textWriter.WriteLine(s);
        }
    }

}
