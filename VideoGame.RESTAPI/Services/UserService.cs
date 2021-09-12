using Microsoft.Extensions.Logging;

namespace VideoGame.RESTAPI.Services
{
    public interface IUserService
    {
        bool IsValidUser(string userName, string password);

    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        // inject database for user validation
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public bool IsValidUser(string userName, string password)
        {
            _logger.LogInformation($"Validating user [{userName}]");
            return userName.Equals("admin") && password.Equals("password");
        }
    }
}
