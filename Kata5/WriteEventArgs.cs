#region Using

using System;

#endregion


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
}
