using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;
using SchoolManagementSystemAPI.Services.Parent.Services;
using System.Text;

namespace SchoolManagementSystemAPI.Services.Parent.Utils.RabbitMQ
{
    public class RabbitMQBusConsumer : BackgroundService, IRabbitMQBusConsumer
    {
        private readonly IConfiguration _config;
        private readonly ParentRegService _service;
        private readonly IConnection _conn;
        private readonly IModel regParentChannel;

        public RabbitMQBusConsumer(ParentRegService service, IConfiguration config)
        {
            _config= config;
            _service= service;  

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };

            _conn = factory.CreateConnection();
            regParentChannel = _conn.CreateModel();

            string queue = _config.GetValue<string>("ExchnageAndQueueName:ParentRegQueue");
            regParentChannel.QueueDeclare(queue, false, false, false, null);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var parentMsgConsumer = new EventingBasicConsumer(regParentChannel);
            parentMsgConsumer.Received += ParentRerReceived;

            regParentChannel.BasicConsume(_config.GetValue<string>("ExchnageAndQueueName:ParentRegQueue"), false, parentMsgConsumer);
            return Task.CompletedTask;
        }

        private void ParentRerReceived(object? sender, BasicDeliverEventArgs e)
        {
            var context = Encoding.UTF8.GetString(e.Body.ToArray());
            MsgRegParentDTO msgRegParentDTO = JsonConvert.DeserializeObject<MsgRegParentDTO>(context);
            _service.RegParent(msgRegParentDTO);

            regParentChannel.BasicAck(e.DeliveryTag, false);
        }
    }
}
