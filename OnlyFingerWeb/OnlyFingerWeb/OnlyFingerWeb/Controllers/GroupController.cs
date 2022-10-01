using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;
using OnlyFingerWeb.Service.GroupService;

namespace OnlyFingerWeb.Controllers
{
    /// <summary>
    /// 组处理接口
    /// </summary>
    [Route("group/[controller]")]
    [ApiController]
    public class GroupController: ControllerBase
    {
        IGroupService groupService;
        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        /// <summary>
        /// 增加一个组
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns> 
        [HttpPost("addGroup")]
        public string addGroup(string groupName)
        {
            GroupEntity groupEntity = new GroupEntity();
            groupEntity.groupName = groupName;
            ReturnCode<string> returnCode = groupService.addGroup(groupEntity);
            return JsonConvert.SerializeObject(returnCode);
        }

        /// <summary>
        /// 更新一个组（暂时只更新组名）
        /// </summary>
        /// <param name="groupId">需要修改的组id</param>
        /// <param name="groupName">要修改的组名</param>
        /// <returns></returns>
        [HttpPost("updateGroup")]
        public string updateGroup(int groupId, string groupName)
        {

            GroupEntity groupEntity = new GroupEntity();
            groupEntity.groupName = groupName;
            ReturnCode<string> returnCode = groupService.updateGroup(groupId, groupEntity);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("getAllGroup")]
        public string getAllGroup()
        {
            ReturnCode<List<GroupEntity>> returnCode = groupService.getGroups();
            return JsonConvert.SerializeObject(returnCode);
        }

        
    }
}
