using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChatApp.Mobile.Services.Core
{
    public class SessionService : ISessionService
    {
        public async Task<UserModel> GetConnectedUser()
        {
            string content = string.Empty;
            try
            {
                content = await SecureStorage.GetAsync("ConnectedUser");
            }
            catch (Exception exp)
            {

            }
            return string.IsNullOrEmpty(content) ? null : JsonConvert.DeserializeObject<UserModel>(content);
        }

        public async Task SetConnectedUser(UserModel userModel)
        {
            string content = JsonConvert.SerializeObject(userModel);
            await SecureStorage.SetAsync("ConnectedUser", content);
        }

        public async Task LogOut()
        {
            await SecureStorage.SetAsync("ConnectedUser", string.Empty);
            await SecureStorage.SetAsync("Token", string.Empty);
        }

        public T GetValue<T>(string key) where T : class
        {
            var content = Preferences.Get(key, null);
            return string.IsNullOrEmpty(content) ? null : JsonConvert.DeserializeObject<T>(content);
        }

        public T GetStructValue<T>(string key) where T : struct
        {
            var content = Preferences.Get(key, null);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void SetValue<T>(string key, T value)
        {
            string content = JsonConvert.SerializeObject(value);
            Preferences.Set(key, content);
        }
    }
}
