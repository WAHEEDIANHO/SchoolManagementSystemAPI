namespace SchoolManagementSystemAPI.Services.AuthAPI.Services.HangFireServiceManagement
{
    public interface IHangFireServiceManagement
    {
        void SendEmail(string email);
        void CheckSentMessageUpdate();
        void SendNewsLetter(List<String> emails);
    }
}
