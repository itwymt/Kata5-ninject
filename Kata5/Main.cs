#region Using

using System;

#endregion

namespace Kata5
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var publisher = new Publisher();
            var subsriberCreator = new SubscriberCreator(publisher);
            var commandProcessor = new CommandProcessor(publisher, subsriberCreator, Console.In);
            commandProcessor.SubscribeAndReadText();
            Console.ReadLine();
        }
    }
}
