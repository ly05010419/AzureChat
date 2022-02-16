using ChatApp.Mobile.Models;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserModel> Login(LoginModel loginModel);
    }
}
