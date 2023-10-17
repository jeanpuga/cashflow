namespace APPLICATION.Features.Auth.Models
{
    public class FilterAccount
    {
        public FilterAccount(string user, string password)
        {
            Email = user;
            Password = password;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}