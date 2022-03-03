namespace MailSender.Interfaces
{
    public class MailSendResult
    {
        public bool IsSuccess { get; }

        public string ReasonMessage { get; }

        public MailSendResult(bool success, string message = null)
        {
            IsSuccess = success;
            ReasonMessage = message;
        }

        public static MailSendResult Ok() => new MailSendResult(true, string.Empty);

        public static MailSendResult Fail(string message) => new MailSendResult(false, message);
    }
}
