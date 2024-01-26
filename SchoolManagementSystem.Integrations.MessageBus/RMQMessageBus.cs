using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Integrations.MessageBus
{
    public class RMQMessageBus : IMessageBus
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _conn;

        public RMQMessageBus()
        {
            _hostname = "localhost";
            _username = "guest";
            _password = "guest";
        } 

        public void SendMessage(object msg, string? exchangeName, List<string> queue)
        {
            if (ConnectionExist())
            {
                using var channel = _conn.CreateModel();
                var json = JsonConvert.SerializeObject(msg);
                var body = Encoding.UTF8.GetBytes(json);
              
                if(queue != null)
                {
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false);
                    queue.ForEach(queue =>
                    {
                        channel.QueueDeclare(queue, false, false, false, null);
                        channel.QueueBind(queue, exchangeName, queue);
                        channel.BasicPublish(exchangeName, queue, null, body);
                    });
                }
                else
                {
                    channel.ExchangeDeclare(exchangeName,ExchangeType.Direct, false);
                    channel.QueueDeclare(exchangeName, false, false, false, null);
                    channel.QueueBind(exchangeName, exchangeName, exchangeName);
                    channel.BasicPublish(exchangeName, exchangeName, null, body);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _conn = factory.CreateConnection();
            }catch (Exception ex) { }
        }

        private bool ConnectionExist()
        {
            if(_conn == null )
            {
                CreateConnection();
                return true;
            }
            return true;
        }
    }
}
