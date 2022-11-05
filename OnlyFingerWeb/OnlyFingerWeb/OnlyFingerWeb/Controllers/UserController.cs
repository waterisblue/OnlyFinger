using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlyFingerWeb.Service.UserService;

namespace OnlyFingerWeb.Controllers
{
    /// <summary>
    /// 用户处理接口
    /// </summary>
    [Route("user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// 分页获取用户
        /// </summary>
        /// <param name="start">起始页</param>
        /// <param name="end">结束页</param>
        /// <returns></returns>
        [HttpPost("getUserByPage")]
        public string getUserByPage(int start, int end)
        {

            var returnCode = userService.getUserByPage(start, end);
            return JsonConvert.SerializeObject(returnCode);
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUserCount")]
        public string getUserCount()
        {
            var returnCode = userService.getUserCount();
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("getUserById")]
        public string getUserById(int userId)
        {
            var returnCode = userService.getUserById(userId);
            return JsonConvert.SerializeObject(returnCode);
        }
    }
}
