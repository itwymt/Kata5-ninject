#region Using

using System;
using System.IO;
using System.Text;
using NSubstitute;
using Xunit;
using FluentAssertions;

#endregion


namespace Kata5
{
    public class TestPublisher
    {
        private string readedText;

        [Fact]
        public void test_read_text()
        {
            readedText = "";
            var publisher = new Publisher();
            publisher.WriteEvent += HandlePublisherEvent;
            publisher.Publish("lhyiuyo,ddddd,dddddd");
            readedText.Should().Be("lhyiuyo,ddddd,dddddd");
        }

        [Fact]
        public void test_read_text1()
        {
            readedText = "";
            var publisher = new Publisher();
            publisher.WriteEvent += HandlePublisherEvent;
            publisher.Publish(null);
            readedText.Should().Be("");
        }

        [Fact]
        public void test_no_subscribers()
        {
            readedText = "";
            var publisher = new Publisher();
            publisher.Publish("test");
            readedText.Should().Be("");
        }

        private void HandlePublisherEvent(Object s, WriteEventArgs eventArgs)
        {
            if (readedText != "")
            {
                readedText += ",";
            }
            readedText += eventArgs.Text;
        }
    }

    public class TestCommandProcessor
    {
        [Fact]
        public void test_add_subscriber_command()
        {
            var publisher = Substitute.For<IPublisher>();
            var sc = Substitute.For<ISubscriberCreator>();
            var st = new StringReader("add");
            var cp = new CommandProcessor(publisher,sc,st);
            cp.SubscribeAndReadText();
            sc.Received().CreateSubscriber(Arg.Any<TextWriter>());
        }

        [Fact]
        public void test_reading_text()
        {
            var publisher = Substitute.For<IPublisher>();
            var sc = Substitute.For<ISubscriberCreator>();
            var sr = new StringReader("some text");
            var cp = new CommandProcessor(publisher, sc, sr);
            cp.SubscribeAndReadText();
            publisher.Received().Publish("some text");
            publisher.DidNotReceive().Publish("some other text");
        }

        [Fact]
        public void test_null_text_reader()
        {
            var publisher = Substitute.For<IPublisher>();
            var sc = Substitute.For<ISubscriberCreator>();
            Action act = () => new CommandProcessor(publisher, sc, null);
            act.ShouldThrow<NullReferenceException>().WithMessage("Text reader should not be null");
        }

        [Fact]
        public void test_null_publisher()
        {
            var sc = Substitute.For<ISubscriberCreator>();
            var sr = new StringReader("some text");
            Action act = () => new CommandProcessor(null, sc, sr);
            act.ShouldThrow<NullReferenceException>().WithMessage("Publisher should not be null");
        }

        [Fact]
        public void test_null_subscriber_creator()
        {
            var publisher = Substitute.For<IPublisher>();
            var sc = Substitute.For<ISubscriberCreator>();
            var sr = new StringReader("add");
            Action act = () => new CommandProcessor(publisher, null, sr);
            act.ShouldThrow<NullReferenceException>().WithMessage("Subscriber creator should not be null");
        }
    }

    public class TestSubscriber
    {
        [Fact]
        public void test_subsriber()
        {
            var tp = Substitute.For<IPublisher>();
            var sb = new StringBuilder();
            new Subscriber(tp, new StringWriter(sb));
            tp.WriteEvent += Raise.Event<EventHandler<WriteEventArgs>>(this, new WriteEventArgs("abcd"));
            sb.ToString().Should().Be("abcd\r\n");
        }

        [Fact]
        public void test_subsriber1()
        {
            var tp = Substitute.For<IPublisher>();
            var sb = new StringBuilder();
            var subscriber = new Subscriber(tp, new StringWriter(sb));
            tp.WriteEvent += Raise.Event<EventHandler<WriteEventArgs>>(this, new WriteEventArgs (""));
            sb.ToString().Should().Be("\r\n");
        }

        [Fact]
        public void test_null_publisher()
        {
            var sb = new StringBuilder();
            Action act = () => new Subscriber(null, new StringWriter(sb));
            act.ShouldThrow<NullReferenceException>().WithMessage("Publisher should not be null");
        }

        [Fact]
        public void test_null_text_writer()
        {
            var tp = Substitute.For<IPublisher>();
            Action act = () => new Subscriber(tp, null);
            act.ShouldThrow<NullReferenceException>().WithMessage("Text writer should not be null");
        }
    }

    public class test_subscriber_creator
    {
        [Fact]
        public void test_create_subscriber()
        {
            var publisher = Substitute.For<IPublisher>();
            var sc = new SubscriberCreator(publisher);
            var sw = new StringWriter(new StringBuilder());
            var subscriber = sc.CreateSubscriber(sw);
            subscriber.Should().BeOfType<Subscriber>();
        }
        [Fact]
        public void test_subscriber_creator_with_null_publisher()
        {
            Action act = () => new SubscriberCreator(null);
            act.ShouldThrow<NullReferenceException>().WithMessage("Publisher should not be null");
        }
    }
}
