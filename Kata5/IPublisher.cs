#region Using
using System;
#endregion

namespace Kata5
{
    public interface IPublisher
    {
        event EventHandler<WriteEventArgs> WriteEvent;
        void Publish(String text);
    }
}
