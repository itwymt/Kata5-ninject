using System;
using System.IO;

namespace Kata5
{
    public class WriteEventArgs : EventArgs
    {
        public WriteEventArgs(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }

    public sealed class Publisher: IPublisher
    {
        private readonly TextReader _textReader;

        public event EventHandler<WriteEventArgs> WriteEvent;

        

        public Publisher(TextReader textReader)
        {
            _textReader = textReader;
        }

        private void RaiseWriteEvent(WriteEventArgs eventArgs)
        {
            if (WriteEvent!=null)
            {
                WriteEvent(this, eventArgs);
            }
        }

        public void ReadText()
        {
            string s;
            while ((s = _textReader.ReadLine()) != null)
            {
                if (!String.IsNullOrEmpty(s))
                {
                    RaiseWriteEvent(new WriteEventArgs(s));
                }
            }
        }
    }

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

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var publisher = new Publisher(Console.In);
            var subsriber = new Subscriber(publisher, Console.Out);

            publisher.ReadText();
            Console.ReadLine();
        }
    }
}
