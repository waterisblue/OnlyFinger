using Furion.DependencyInjection;
using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;
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

            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

            if ((task.startTime < timestamp) || (task.startTime > task.endTime))
            {
                returnCode.code = 415;
                returnCode.message = "用户输入错误，输入的时间不符合要求";
                return returnCode;
            }
            int ret = Db.Insertable(task).IgnoreColumns(it => new { it.id }).ExecuteReturnIdentity();
            returnCode.code = 200;
            returnCode.message = "任务数据插入成功，任务id：" + ret;
            returnCode.data = ret + "";
            return returnCode;
        }

        public ReturnCode<string> deleteTask(int taskId)
        {
            var returnCode = new ReturnCode<string>();
            var taskList = Db.Queryable<TaskEntity>().Where(it => it.id == taskId).ToList();
            if (taskList.Count == 0)
            {
                returnCode.code = 304;
                returnCode.message = "查询失败，未查询到内容";
                return returnCode;
            }
            var task = taskList.First();
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            if(task.startTime <= timestamp)
            {
                returnCode.code = 304;
                returnCode.message = "删除失败，仅能删除未开始的任务";
                returnCode.data = "删除行数为：0";
                return returnCode;
            }
            var ret = Db.Updateable<TaskEntity>()
                .SetColumns(it => it.isDelete == true)
                .Where(it => it.id == taskId)
                .ExecuteCommand();

            if (ret != 1)
            {
                returnCode.code = 500;
                returnCode.message = "删除行数不唯一，删除行数：" + ret;
                return returnCode;
            }
            Db.Updateable<Group2Task>()
                .SetColumns(it => it.isDelete == true)
                .Where(it => it.taskId == taskId)
                .ExecuteCommand();
            returnCode.code = 200;
            returnCode.message = "删除成功";
            returnCode.data = 1 + "";
            return returnCode;
        }


        public ReturnCode<string> updateTask(int taskId, string taskName, string desc)
        {
            var returnCode = new ReturnCode<string>();
            var taskList = Db.Queryable<TaskEntity>().Where(it => it.id == taskId && it.isDelete == false).ToList();
            if (taskList.Count == 0)
            {
                returnCode.code = 304;
                returnCode.message = "查询失败，未查询到内容";
                return returnCode;
            }
            var task = taskList.First();
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            if (task.startTime <= timestamp)
            {
                returnCode.code = 304;
                returnCode.message = "失败，无法更新已经开始的任务";
                returnCode.data = "更新失败";
                return returnCode;
            }
            var taskUpdate = new TaskEntity();
            taskUpdate.id = taskId;
            taskUpdate.taskName = taskName;
            taskUpdate.desc = desc;

            int ret = Db.Updateable(taskUpdate).UpdateColumns(it => new { it.taskName, it.desc }).ExecuteCommand();
            if (ret != 1)
            {
                returnCode.code = 500;
                returnCode.message = "修改任务信息失败，返回行数不为1";
                return returnCode;
            }
            returnCode.code = 200;
            returnCode.message = "修改任务成功，修改行数：" + ret;
            return returnCode;

        }

        public ReturnCode<TaskWithGroup> findTaskById(int taskId)
        {
            var returnCode = new ReturnCode<TaskWithGroup>();
            var taskList = Db.Queryable<TaskEntity>().Where(it => it.id == taskId && it.isDelete == false).ToList();
            if(taskList.Count == 0)
            {
                returnCode.code = 304;
                returnCode.message = "查询失败，未查询到内容";
                return returnCode;
            }
            var task = taskList.First();
            var groupList = Db.Queryable<Group2Task>()
                .Where(it => it.taskId == taskId)
                .LeftJoin<GroupEntity>((tg, g) => tg.groupId == g.id && g.isDelete == false)
                .Select((tg, g) => new GroupEntity { id = g.id, groupName = g.groupName, desc = g.desc })
                .ToList();
            var taskWithGroup = new TaskWithGroup(task, groupList);
            returnCode.code = 200;
            returnCode.message = "查询成功";
            returnCode.data = taskWithGroup;
            return returnCode;
        }
        public ReturnCode<int> addGroupToTask(int groupId, int taskId)
        {
            var returnCode = new ReturnCode<int>();
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            // 检查该用户是否已加入组
            int checkListCount = Db.Queryable<Group2Task>().Where(it => it.groupId == groupId && it.taskId == taskId && it.isDelete == false).Count();
            if (checkListCount != 0)
            {
                returnCode.code = 304;
                returnCode.message = "该组已在任务中";
                returnCode.data = checkListCount;
                return returnCode;
            }

            var group2TaskEntity = new Group2Task(groupId, taskId, timestamp, false);
            try
            {
                int ret = Db.Insertable(group2TaskEntity).IgnoreColumns(it => it.id).ExecuteCommand();

                if (ret != 1)
                {
                    returnCode.code = 500;
                    returnCode.message = "插入失败，插入行数不唯一";
                    returnCode.data = ret;
                    return returnCode;
                }


                returnCode.code = 200;
                returnCode.message = "增加成功";
                returnCode.data = ret;
                return returnCode;
            }
            // 若外键异常
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                returnCode.code = 304;
                returnCode.message = e.Message;
                returnCode.data = -1;
                return returnCode;
            }
        }
        public ReturnCode<List<TaskEntity>> getTasksBySearch(string searchStr)
        {
            var returnCode = new ReturnCode<List<TaskEntity>>();
            var exp = Expressionable.Create<TaskEntity>();

            if (searchStr == null || searchStr.Equals(""))
            {
                returnCode.code = 500;
                returnCode.message = "查询失败，不能输入空查询字符。";
                returnCode.data = null;
                return returnCode;
            }

            var taskList = new List<TaskEntity>();

            exp.Or(it => it.taskName.Contains(searchStr));
            exp.And(it => it.isDelete == false);

            int parseId;

            bool tryParseBool = int.TryParse(searchStr, out parseId);


            if (tryParseBool)
            {
                exp.Or(it => it.id == parseId);
            }
            taskList = Db.Queryable<TaskEntity>().Where(exp.ToExpression()).ToList();


            returnCode.code = 200;
            returnCode.message = "查询成功。";
            returnCode.data = taskList;

            return returnCode;
        }

        public ReturnCode<List<TaskEntity>> getCurrentTask()
        {
            var returnCode = new ReturnCode<List<TaskEntity>>();
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var taskList = Db.Queryable<TaskEntity>()
                .Where(it => it.startTime > timestamp)
                .ToList();
            returnCode.code = 200;
            returnCode.message = "查询成功";
            returnCode.data = taskList;
            return returnCode;
        }
    }
}
