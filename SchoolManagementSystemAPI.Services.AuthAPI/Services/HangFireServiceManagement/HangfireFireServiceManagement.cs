
namespace SchoolManagementSystemAPI.Services.AuthAPI.Services.HangFireServiceManagement
{
    public class HangfireFireServiceManagement : IHangFireServiceManagement
    {
        private readonly ILogger<HangfireFireServiceManagement> _logger;

        public HangfireFireServiceManagement(ILogger<HangfireFireServiceManagement> logger)
        {
            _logger = logger;
        }
        public void CheckSentMessageUpdate()
        {
            Console.WriteLine("I will be cheking snet message after every 2s");
            _logger.LogInformation("I will be cheking snet message after every 2s");
        }

        public void SendEmail(string email)
        {
            _logger.LogInformation("Sent email some minute after registration");
        }

        public void SendNewsLetter(List<string> emails)
        {
            _logger.LogInformation("There is a particular time for me to take my action");
        }
    }
}
