using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public record RawIntegrationEvent : IntegrationEvent
    {
        public string RawData { get; set; }
    }
}
