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
        /// <param name="desc">组描述信息</param>
        /// <returns>增加条数</returns> 
        [HttpPost("addGroup")]
        public string addGroup(string groupName, string desc)
        {
            GroupEntity groupEntity = new GroupEntity();
            groupEntity.groupName = groupName;
            groupEntity.desc = desc;
            groupEntity.isDelete = false;
            ReturnCode<string> returnCode = groupService.addGroup(groupEntity);
            return JsonConvert.SerializeObject(returnCode);
        }

        /// <summary>
        /// 更新一个组（暂时只更新组名）
        /// </summary>
        /// <param name="groupId">需要修改的组id</param>
        /// <param name="groupName">要修改的组名</param>
        /// <param name="desc">要修改的组描述信息</param>
        /// <returns>更新条数</returns>
        [HttpPost("updateGroup")]
        public string updateGroup(int groupId, string groupName, string desc)
        {

            GroupEntity groupEntity = new GroupEntity();
            groupEntity.groupName = groupName;
            groupEntity.desc = desc;
            ReturnCode<string> returnCode = groupService.updateGroup(groupId, groupEntity);
            return JsonConvert.SerializeObject(returnCode);
        }

        /// <summary>
        /// 分段获取组
        /// </summary>
        /// <param name="start">起始条数</param>
        /// <param name="limit">获取多少条</param>
        /// <returns>通过页码获取到的组集合</returns>
        [HttpPost("getAllGroupByPage")]
        public string getAllGroupByPage(int start, int limit)
        {
            ReturnCode<List<GroupEntity>> returnCode = groupService.getGroups(start, limit);
            return JsonConvert.SerializeObject(returnCode);
        }

        /// <summary>
        /// 获取组数一共多少条
        /// </summary>
        /// <returns>获取到的组条数</returns>
        [HttpGet("getGroupCount")]
        public string getGroupCount()
        {
            ReturnCode<int> returnCode = groupService.getGroupNum();
            return JsonConvert.SerializeObject(returnCode); 
        }

        /// <summary>
        /// 通过id或组名查找组
        /// </summary>
        /// <param name="searchStr">查找字符串</param>
        /// <returns>查找到的组集合</returns>
        [HttpPost("searchGroup")]
        public string searchGroup(string searchStr)
        {
            ReturnCode<List<GroupEntity>> returnCode = groupService.searchGroup(searchStr);
            return JsonConvert.SerializeObject(returnCode);
        }

        /// <summary>
        /// 删除一个组
        /// </summary>
        /// <param name="groupId">组id</param>
        /// <returns>删除条数</returns>
        [HttpPost("deleteGroup")]
        public string deleteGroup(int groupId)
        {
            var returnCode = groupService.deleteGroup(groupId);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("addUser2Group")]
        public string addUser2Group(int userId, int groupId)
        {
            var returnCode = groupService.addUser2Group(userId, groupId);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("getUserByGroupId")]
        public string getUserByGroupId(int groupId)
        {
            var returnCode = groupService.getUserByGroupId(groupId);
            return JsonConvert.SerializeObject(returnCode);
        }

    }
}
