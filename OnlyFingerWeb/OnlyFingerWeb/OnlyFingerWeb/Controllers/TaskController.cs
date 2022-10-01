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
        /// <param name="startTime">任务开始时间</param>
        /// <param name="endTime">任务结束时间</param>
        /// <param name="group">任务的组</param>
        /// <returns></returns>
        [HttpPost("addTask")]
        public string addTask(String taskName, long startTime, long endTime, int group)
        {
            ReturnCode<string> returnCode = new ReturnCode<string>();
            if(taskName == null)
            {
                returnCode.code = 403;
                returnCode.message = "任务名不可为空";
                return JsonConvert.SerializeObject(returnCode);
            }
            
            TaskEntity taskEntity = new TaskEntity(taskName, startTime, endTime, group);
            
            returnCode = taskService.addTask(taskEntity);
            return JsonConvert.SerializeObject(returnCode);
        }
    }
}
