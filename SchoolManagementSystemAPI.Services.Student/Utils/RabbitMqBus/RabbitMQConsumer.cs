using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Services;
using SchoolManagementSystemAPI.Services.Student.Services.IServices;
using System.Text;

namespace SchoolManagementSystemAPI.Services.Student.Utils.RabbitMqBus
{
    public class RabbitMQConsumer : BackgroundService, IRabbitMQConsumer
    {
        private readonly IConfiguration _config;
        //private readonly IStudentService _service;
        private readonly StuRegService _service;
        private readonly IConnection _conn;
        private IModel studentRegistrationChannel;

        public RabbitMQConsumer(IConfiguration config, StuRegService service)
        {
            _config = config;
            _service = service;

            var factory = new ConnectionFactory()
            {
                HostName = _config.GetValue<string>("RabbitmqConn:Host"),
                UserName = _config.GetValue<string>("RabbitmqConn:Username"),
                Password = _config.GetValue<string>("RabbitmqConn:Password"), //"pwQAxWoSgrrF30FS2y4nCeRAR52IwiVm",
                VirtualHost = _config.GetValue<string>("RabbitmqConn:VirtualHost"),
                AutomaticRecoveryEnabled = true,
            };

            string queueName = _config.GetValue<string>("ExchnageAndQueueName:StudentRegQueue");
            _conn = factory.CreateConnection();
            studentRegistrationChannel = _conn.CreateModel();
            studentRegistrationChannel.QueueDeclare(queueName, false, false, false, null);
           // studentRegistrationChannel.QueueBind(queueName, queueName, queueName);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var studentRegConsumer = new EventingBasicConsumer(studentRegistrationChannel);
            studentRegConsumer.Received += StudentRegMsgReceived;

            studentRegistrationChannel.BasicConsume(_config.GetValue<string>("ExchnageAndQueueName:StudentRegQueue"), false, studentRegConsumer);
            return Task.CompletedTask;
        }

        private void StudentRegMsgReceived(object? sender, BasicDeliverEventArgs e)
        {
            var content = Encoding.UTF8.GetString(e.Body.ToArray());
            MsgRegStudentDTO st = JsonConvert.DeserializeObject<MsgRegStudentDTO>(content);
            _service.RegStudent(st).GetAwaiter().GetResult();

            studentRegistrationChannel.BasicAck(e.DeliveryTag, false);
        }
    }
}
