#region Using

using System;

#endregion



namespace Kata5
{
    public sealed class Publisher : IPublisher
    {
        //private readonly TextReader _textReader;

        public event EventHandler<WriteEventArgs> WriteEvent;



        //public Publisher(TextReader textReader)
        //{
        //    _textReader = textReader;
        //}

        private void RaiseWriteEvent(WriteEventArgs eventArgs)
        {
            if (WriteEvent != null)
            {
                WriteEvent(this, eventArgs);
            }
        }

        //public void ReadText()
        //{
        //    string s;
        //    while ((s = _textReader.ReadLine()) != null)
        //    {
        //        if (!String.IsNullOrEmpty(s))
        //        {
        //            RaiseWriteEvent(new WriteEventArgs(s));
        //        }
        //    }
        //}

        public void Publish(String text)
        {
            if (!String.IsNullOrEmpty(text))
                {
                    RaiseWriteEvent(new WriteEventArgs(text));
                }
        }
    }
}
