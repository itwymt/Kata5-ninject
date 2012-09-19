using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata5
{
    public interface IPublisher
    {
        event WriteEventHandler WriteEvent;
    }
}
