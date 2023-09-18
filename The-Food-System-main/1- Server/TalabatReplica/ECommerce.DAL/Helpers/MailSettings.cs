namespace ECommerce.DAL.Helpers
{
    public class MailSettings
    {
        public string Email { get; set; } = string.Empty;
        public string DisplayedName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
