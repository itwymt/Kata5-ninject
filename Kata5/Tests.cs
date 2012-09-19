using System;
using System.IO;
using System.Text;
using Xunit;
using FluentAssertions;

namespace Kata5
{
    public class TestPablisher
    {
        private string readedText;
        [Fact]
        public void test_read_text()
        {
            readedText = "";
            var str = new [] {"lhyiuyo","ddddd","dddddd"};
            var tr = new StringReader(string.Join(",", str));
            var publisher = new Publisher(tr);
            publisher.WriteEvent += HandlePublisherEvent;
            publisher.ReadText();
            readedText.Should().Be("lhyiuyo,ddddd,dddddd");
        }

        [Fact]
        public void test_read_text1()
        {
            readedText = "";
            var str = new[] { "lhyiuyo", "ddddd", "1234qwere" };
            var tr = new StringReader(string.Join("\n", str));
            var publisher = new Publisher(tr);
            publisher.WriteEvent += HandlePublisherEvent;
            publisher.ReadText();
            readedText.Should().Be("lhyiuyo,ddddd,1234qwere");
        }

        private void HandlePublisherEvent(Object s, WriteEventArgs eventArgs)
        {
            if (readedText != "")
            {
                readedText += ",";
            }
            readedText += eventArgs.GetText();
        }
    }

    public class TestSubscriber
    {
        [Fact]
        public void test_subsriber()
        {
            var tp = new TestPublisher();
            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            var subscriber = new Subscriber(tp, tw);
            tp.Raise("abcd");
            sb.ToString().Should().Be("abcd\r\n");
        }

        [Fact]
        public void test_subsriber1()
        {
            var tp = new TestPublisher();
            var sb = new StringBuilder();
            var tw = new StringWriter(sb);
            var subscriber = new Subscriber(tp, tw);
            tp.Raise("");
            sb.ToString().Should().Be("\r\n");
        }
    }

    public class TestPublisher : IPublisher
    {
        public event WriteEventHandler WriteEvent;
        public void Raise(string s)
        {
            var eventArgs = new WriteEventArgs(s);
            WriteEvent(this, eventArgs);
        }
    }
}
