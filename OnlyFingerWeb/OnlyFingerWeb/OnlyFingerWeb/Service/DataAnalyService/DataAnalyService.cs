using Furion.DependencyInjection;
using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;
using SqlSugar;

namespace OnlyFingerWeb.Service.DataAnalyService
{
    public class DataAnalyService : IDataAnalyService, ITransient
    {
        public ISqlSugarClient Db { get; }

        public DataAnalyService(ISqlSugarClient db)
        {
            Db = db;
        }

        public ReturnCode<List<TaskEntity>> getCurrentTimeTask()
        {
            var returnCode = new ReturnCode<List<TaskEntity>>();
            double timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var taskList = Db.Queryable<TaskEntity>().Where(it => timestamp >= it.startTime && timestamp <= it.endTime && it.isDelete == false).ToList();
            returnCode.code = 200;
            returnCode.message = "查询成功";
            returnCode.data = taskList;
            return returnCode;
        }

        public ReturnCode<Dictionary<string, int>> getGaugeData(int taskId)
        {
            var returnCode = new ReturnCode<Dictionary<string, int>>();

            var allCount = Db.Queryable<Group2Task>()
                .InnerJoin<Group2User>((gt, gu) => gt.taskId == taskId && gt.groupId == gu.groupid && gu.isDelete == false && gt.isDelete == false).Count();
            var signCount = Db.Queryable<Sign>()
                .Where(it => it.taskid == taskId).Count();
            var resData = new Dictionary<string, int>();
            resData.Add("allCount", allCount);
            resData.Add("signCount", signCount);
            returnCode.code = 200;
            returnCode.message = "查询成功";
            returnCode.data = resData;
            return returnCode;

        }

        public ReturnCode<List<UserSign>> getSignUser(int taskId)
        {
            var res = Db.Queryable<Sign>()
                .InnerJoin<UserEntity>((s, ue) => s.taskid == taskId && ue.id == s.userid && ue.isDelete == false)
                .OrderBy(s => s.signtime, OrderByType.Desc)
                .Select((s, ue) => new UserSign { id = ue.id, signtime = s.signtime, username = ue.username})
                .ToList();

            var returnCode = new ReturnCode<List<UserSign>>();
            returnCode.code = 200;
            returnCode.data = res;
            returnCode.message = "查询成功";
            return returnCode;


        }
    }
}
