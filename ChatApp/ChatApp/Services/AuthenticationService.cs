using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Prism.Navigation;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Core
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public AuthenticationService(ISessionService sessionService, INavigationService navigationService)
            : base(sessionService, navigationService)
        {
        }

        public async Task<UserModel> Login(LoginModel loginDto)
        {
            return await Post<UserModel, LoginModel>("Users/login", loginDto);
        }
    }
}
