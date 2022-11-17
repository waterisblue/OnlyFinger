using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Service.TaskService;

namespace OnlyFingerWeb.Controllers
{
    /// <summary>
    /// 任务处理接口
    /// </summary>
    [Route("task/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskService taskService;

        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        /// <summary>
        /// 增加一个任务
        /// </summary>
        /// <param name="taskName">任务名</param>
        /// <param name="desc"></param>
        /// <param name="startTime">任务开始时间</param>
        /// <param name="endTime">任务结束时间</param>
        /// <returns></returns>
        [HttpPost("addTask")]
        public string addTask(string taskName, string desc, long startTime, long endTime)
        {
            ReturnCode<string> returnCode = new ReturnCode<string>();
            if(taskName == null)
            {
                returnCode.code = 403;
                returnCode.message = "任务名不可为空";
                return JsonConvert.SerializeObject(returnCode);
            }
            
            TaskEntity taskEntity = new TaskEntity(taskName, startTime, endTime, false, desc);
            
            returnCode = taskService.addTask(taskEntity);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("deleteTask")]

        public string deleteTask(int taskId)
        {
            var returnCode = taskService.deleteTask(taskId);
            return JsonConvert.SerializeObject(returnCode);
        }
        [HttpPost("findTaskWithGroup")]
        public string findTaskWithGroup(int taskId)
        {
            var returnCode = taskService.findTaskById(taskId);
            return JsonConvert.SerializeObject(returnCode);
        }
        [HttpPost("updateTask")]
        public string updateTask(int taskId, string taskName, string desc)
        {
            var returnCode = taskService.updateTask(taskId, taskName, desc);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("addGroupToTask")]
        public string addGroupToTask(int groupId, int taskId)
        {
            var returnCode = taskService.addGroupToTask(groupId, taskId);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("getTasksBySearch")]
        public string getTasksBySearch(string searchStr)
        {
            var returnCode = taskService.getTasksBySearch(searchStr);
            return JsonConvert.SerializeObject(returnCode);
        }

        [HttpPost("getCurrentTask")]
        public string getCurrentTask()
        {
            var returnCode = taskService.getCurrentTask();
            return JsonConvert.SerializeObject(returnCode);
        }
    }

}
