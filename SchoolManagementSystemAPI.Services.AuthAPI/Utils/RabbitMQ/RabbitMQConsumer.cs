
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Utils.RabbitMQ
{
    public class RabbitMQConsumer : BackgroundService, IRabbitMQConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _conn;
        private readonly IModel teacherDelChannel;
        private readonly IModel parentDelChannel;
        private readonly IModel studentDelChannel;

        private readonly string teacherQueueName;
        private readonly string parentQueueName;
        private readonly string studentQueueName;

        public RabbitMQConsumer(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };
            _conn = factory.CreateConnection();
           
            teacherDelChannel = _conn.CreateModel();
            parentDelChannel = _conn.CreateModel();
            studentDelChannel = _conn.CreateModel();

            teacherQueueName = _configuration.GetValue<string>("ExchnageAndQueueName:TeacherDelQueue");
            parentQueueName = _configuration.GetValue<string>("ExchnageAndQueueName:ParentDelQueue");
            studentQueueName = _configuration.GetValue<string>("ExchnageAndQueueName:StudentDelQueue");

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
            throw new NotImplementedException();
        }

        private void ParentConSumerReceived(object? sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TeacherConsumerReceived(object? sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
