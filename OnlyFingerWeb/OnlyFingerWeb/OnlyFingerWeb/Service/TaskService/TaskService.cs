using Furion.DependencyInjection;
using OnlyFingerWeb.Entity;
using SqlSugar;

namespace OnlyFingerWeb.Service.TaskService
{
    public class TaskService : ITaskService, ITransient
    {
        public ISqlSugarClient Db { get; }

        public TaskService(ISqlSugarClient db)
        {
            Db = db;
        }

        public ReturnCode<string> addTask(TaskEntity task)
        {
            ReturnCode<string> returnCode = new ReturnCode<string>();

            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            if((task.startTime < timestamp) || (task.startTime > task.endTime))
            {
                returnCode.code = 415;
                returnCode.message = "用户输入错误，输入的时间不符合要求";
                return returnCode;
            }
            int ret = Db.Insertable(task).IgnoreColumns(it => new { it.id }).ExecuteCommand();
            if (ret != 1)
            {
                returnCode.code = 500;
                returnCode.message = "服务器错误，变动的行数不唯一";
                return returnCode;
            }
            returnCode.code = 200;
            returnCode.message = "任务数据插入成功";
            return returnCode;
        }
    }
}
