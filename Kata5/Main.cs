#region Using

using System;
using System.IO;
using Ninject;
using Ninject.Modules;

#endregion

namespace Kata5
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var module = new CustomModule();
            IKernel result = new StandardKernel(module);
            var commandProcessor = result.Get<CommandProcessor>(); //= new CommandProcessor(publisher, subsriberCreator, Console.In);
            commandProcessor.TextReader = Console.In;
            commandProcessor.SubscribeAndReadText();
            Console.ReadLine();
        }
    }

    public class CustomModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IPublisher>().To<Publisher>();
            Bind<ISubscriberCreator>().To<SubscriberCreator>();
            Bind<Subscriber>().ToSelf();
            Bind<CommandProcessor>().ToSelf();
         }
    }
}
