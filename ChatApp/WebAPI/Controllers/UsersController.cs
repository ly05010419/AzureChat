using ChatApp.Managers.Interfaces;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ChatApp.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager UsersManager;
        private readonly IConversationsManager ConversationsManager;
        
        public UsersController(IUsersManager usersManager, IConversationsManager conversationsManager)
        {
            UsersManager = usersManager;
            ConversationsManager = conversationsManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult List()
        {
            return Ok(UsersManager.GetAllUsers());
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var userModel = UsersManager.Login(loginModel, HttpContext);
            return Ok(userModel);
        }

        [HttpGet("getMyFriends/{userId}")]
        public IActionResult GetMyFriends(long userId)
        {
            try
            {
                return Ok(UsersManager.GetMyFriends(userId));
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
        
        [HttpGet("GetMyMessages/{userId}/{friendId}")]
        public IActionResult GetMyMessages(long userId ,long friendId)
        {
            try
            {
                return Ok(ConversationsManager.GetReplies(userId,friendId));
            }
            catch (Exception exp)
            {
                return BadRequest();
            }
        }
    }
}
