#region 

using System.IO;

#endregion


namespace Kata5
{
    public class Subscriber
    {
        private readonly TextWriter _textWriter;

        public Subscriber(IPublisher publisher, TextWriter textWriter)
        {
            _textWriter = textWriter;

            if (publisher != null)
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
