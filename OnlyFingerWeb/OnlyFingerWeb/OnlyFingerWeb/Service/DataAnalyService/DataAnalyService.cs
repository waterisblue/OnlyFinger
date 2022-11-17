using Furion.DependencyInjection;
using Newtonsoft.Json;
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

        public ReturnCode<Dictionary<string, string>> getTimeAnaly(long starttime, long endtime)
        {
            var returnCode = new ReturnCode<Dictionary<string, string>>();
            var dictionary = new Dictionary<string, string>();

            var piedata = Db.Queryable<Sign>()
                .Where(it => it.signtime >= starttime && it.signtime <= endtime)
                .LeftJoin<UserEntity>((s, ue) => s.userid == ue.id)
                .GroupBy((s, ue) => new { ue.id, ue.username})
                .Select((s, ue) => new
                {
                    id = ue.id,
                    username = ue.username,
                    sum = SqlFunc.AggregateCount(s.id)
                })
                .MergeTable()
                .OrderBy(it => it.sum, OrderByType.Desc)
                .ToList();

            dictionary.Add("piedata", JsonConvert.SerializeObject(piedata));

            var pie2data = Db.Queryable<Sign>()
                .Where(it => it.signtime >= starttime && it.signtime <= endtime)
                .LeftJoin<TaskEntity>((s, te) => s.taskid == te.id)
                .GroupBy((s, te) => new { te.id, te.taskName })
                .Select((s, te) => new
                {
                    id = te.id,
                    taskname = te.taskName,
                    sum = SqlFunc.AggregateCount(s.id)
                })
                .MergeTable()
                .OrderBy(it => it.sum, OrderByType.Desc)
                .ToList();

            dictionary.Add("pie2data", JsonConvert.SerializeObject(pie2data));

            var taskId = Db.Queryable<Sign>()
                .Where(it => it.signtime >= starttime && it.signtime <= endtime)
                .Select(it => it.taskid)
                .Distinct()
                .ToList();

            var allCount = Db.Queryable<Group2Task>()
                .Where(it => taskId.Contains(it.taskId))
                .LeftJoin<GroupEntity>((gt, ge) => gt.groupId == ge.id)
                .Sum((gt, ge) => ge.memberCount);

            var signCount = Db.Queryable<Sign>()
                .Where(it => it.signtime >= starttime && it.signtime <= endtime)
                .Count();

            dictionary.Add("allCount", allCount + "");
            dictionary.Add("signCount", signCount + "");



            returnCode.code = 200;
            returnCode.message = "查询成功";
            returnCode.data = dictionary;
            return returnCode;
        }
    }
}
