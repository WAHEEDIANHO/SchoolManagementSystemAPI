
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Services;
using System.Text;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Utils.RabbitMQ
{
    public class RabbitMQConsumer : BackgroundService, IRabbitMQConsumer
    {
        private readonly IConfiguration _config;
        private readonly IConnection _conn;
        private readonly IModel teacherDelChannel;
        private readonly IModel parentDelChannel;
        private readonly IModel studentDelChannel;
        private readonly UserDeleteService _service;

        private readonly string teacherQueueName;
        private readonly string parentQueueName;
        private readonly string studentQueueName;

        public RabbitMQConsumer(IConfiguration configuration, UserDeleteService service)
        {
            _config = configuration;
            _service = service;

            var factory = new ConnectionFactory()
            {
                /*HostName = "localhost",
                UserName = "guest",
                Password = "guest",*/

                HostName = _config.GetValue<string>("RabbitmqConn:Host"),
                UserName = _config.GetValue<string>("RabbitmqConn:Username"),
                Password = _config.GetValue<string>("RabbitmqConn:Password"), //"pwQAxWoSgrrF30FS2y4nCeRAR52IwiVm",
                VirtualHost = _config.GetValue<string>("RabbitmqConn:VirtualHost"),
                AutomaticRecoveryEnabled = true,
            };
            _conn = factory.CreateConnection();
           
            teacherDelChannel = _conn.CreateModel();
            parentDelChannel = _conn.CreateModel();
            studentDelChannel = _conn.CreateModel();

            teacherQueueName = _config.GetValue<string>("ExchnageAndQueueName:TeacherDelQueue");
            parentQueueName = _config.GetValue<string>("ExchnageAndQueueName:ParentDelQueue");
            studentQueueName = _config.GetValue<string>("ExchnageAndQueueName:StudentDelQueue");

            teacherDelChannel.QueueDeclare(teacherQueueName, false, false, false, null);
            parentDelChannel.QueueDeclare(parentQueueName, false, false, false, null);
            studentDelChannel.QueueDeclare(studentQueueName, false, false, false, null);


        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var teacherDelConsumer = new EventingBasicConsumer(teacherDelChannel);
            teacherDelConsumer.Received += TeacherConsumerReceived;

            var parentDelConsumer = new EventingBasicConsumer(parentDelChannel);
            parentDelConsumer.Received += ParentConSumerReceived;

            var studentConsumer = new EventingBasicConsumer(studentDelChannel);
            studentConsumer.Received += StudentConsumerReceived;

            teacherDelChannel.BasicConsume(teacherQueueName, false, teacherDelConsumer);
            parentDelChannel.BasicConsume(parentQueueName, false, parentDelConsumer);
            studentDelChannel.BasicConsume(studentQueueName, false, studentConsumer);





            return Task.CompletedTask;
        }

        private void StudentConsumerReceived(object? sender, BasicDeliverEventArgs e)
        {
            var context = Encoding.UTF8.GetString(e.Body.ToArray());
            MsgRegStudentDTO stuReg = JsonConvert .DeserializeObject<MsgRegStudentDTO>(context);
            _service.DelUser(stuReg);

            studentDelChannel.BasicAck(e.DeliveryTag, false);
        }

        private void ParentConSumerReceived(object? sender, BasicDeliverEventArgs e)
        {
            var context = Encoding.UTF8.GetString(e.Body.ToArray());
            MsgRegParentDTO parentReg = JsonConvert.DeserializeObject<MsgRegParentDTO>(context);
             _service.DelUser(parentReg);

           parentDelChannel.BasicAck(e.DeliveryTag, false);
        }

        private void TeacherConsumerReceived(object? sender, BasicDeliverEventArgs e)
        {
            var context = Encoding.UTF8.GetString(e.Body.ToArray());
            MsgRegTeacherDTO teacherMsg = JsonConvert.DeserializeObject<MsgRegTeacherDTO>(context);
            _service.DelUser(teacherMsg);

            teacherDelChannel.BasicAck(e.DeliveryTag, false);
        }
    }
}
