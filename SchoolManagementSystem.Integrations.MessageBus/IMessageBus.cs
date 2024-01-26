using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Integrations.MessageBus
{
    public interface IMessageBus
    {
        void SendMessage(Object msg, string exchangeName, List<string>? queue);
    }
}
