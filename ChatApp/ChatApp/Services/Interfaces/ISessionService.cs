using ChatApp.Mobile.Models;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Interfaces
{
    public interface ISessionService
    {
        Task<UserModel> GetConnectedUser();
        Task SetConnectedUser(UserModel userModel);
    }
}
