using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Services;
using System.Text;

namespace SchoolManagementSystemAPI.Services.Teacher.Utils.RabbitMQBus
{
    public class RabbitMQBusConsumer : BackgroundService, IRabbitMQBusConsumer
    {
        private readonly TeacherRegService _service;
        private readonly IConfiguration _config;
        private readonly IConnection _conn;
        private readonly IModel teacherRegChannel;

        public RabbitMQBusConsumer(TeacherRegService service, IConfiguration config)
        {
            _service = service;
            _config = config;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            
            string queueName = _config.GetValue<string>("ExchnageAndQueueName:TeacherRegQueue");
            _conn = factory.CreateConnection();
            teacherRegChannel = _conn.CreateModel();
            teacherRegChannel.QueueDeclare(queueName, false, false, false, null);   
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var teacherRegConsumer = new EventingBasicConsumer(teacherRegChannel);
            teacherRegConsumer.Received += TeacherMsgReceived;

            teacherRegChannel.BasicConsume(_config.GetValue<string>("ExchnageAndQueueName:TeacherRegQueue"), false, teacherRegConsumer);
            return Task.CompletedTask;
        }

        private void TeacherMsgReceived(object? sender, BasicDeliverEventArgs e)
        {
            var context = Encoding.UTF8.GetString(e.Body.ToArray());
            MsgRegTeacherDTO msg = JsonConvert.DeserializeObject<MsgRegTeacherDTO>(context);
            _service.RegTeacher(msg);

            teacherRegChannel.BasicAck(e.DeliveryTag, false);
        }
    }
}
