#region Using

using System;

#endregion


namespace Kata5
{
    public sealed class Publisher : IPublisher
    {

        public event EventHandler<WriteEventArgs> WriteEvent;

        private void RaiseWriteEvent(WriteEventArgs eventArgs)
        {
            if (WriteEvent != null)
            {
                WriteEvent(this, eventArgs);
            }
        }

        public void Publish(String text)
        {
            if (!String.IsNullOrEmpty(text))
                {
                    RaiseWriteEvent(new WriteEventArgs(text));
                }
        }
    }
}
