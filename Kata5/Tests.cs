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
    public class TestPablisher
    {
        private string readedText;
        [Fact]
        public void test_read_text()
        {
            //readedText = "";
            //var str = new [] {"lhyiuyo","ddddd","dddddd"};
            //var tr = new StringReader(string.Join(",", str));
            //var publisher = new Publisher(tr);
            //publisher.WriteEvent += HandlePublisherEvent;
            //publisher.ReadText();
            //readedText.Should().Be("lhyiuyo,ddddd,dddddd");
        }

        [Fact]
        public void FF()
        {
            var s = "ddd";
            var r = s;
            r += "rr" ;

            s.Should().Be("ddd");


        }

        [Fact]
        public void test_read_text1()
        {
            //readedText = "";
            //var str = new[] { "lhyiuyo", "ddddd", "1234qwere" };
            //var tr = new StringReader(string.Join("\n", str));
            //var publisher = new Publisher(tr);
            //publisher.WriteEvent += HandlePublisherEvent;
            //publisher.ReadText();
            //readedText.Should().Be("lhyiuyo,ddddd,1234qwere");
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
        public void test_add_command()
        {

        }
        public void text_reading_text()
        {

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
    }
}
