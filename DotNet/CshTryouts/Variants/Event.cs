using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Variants
{
    internal interface IEvent
    {
        public string Id { get; set; }
    }

    internal interface IEvent<TPayload> : IEvent
    {
        TPayload Payload { get; }
    }

    internal class TextEvent : IEvent<string>
    {
        public string Payload { get; set; }

        public string Id { get; set; }

        public TextEvent(string t)
        {
            Payload = t;
        }
    }
}
