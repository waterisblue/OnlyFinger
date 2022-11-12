using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zk4500.Model;

namespace zk4500
{
    class MyDBDao
    {
        SqlSugarScope db = SqlSugarConnect.getConnect();

        public int registerUser(UserDetail ud)
        {
            return db.Insertable(ud).IgnoreColumns(it => new { it.Id }).ExecuteReturnIdentity();
        }

        public List<TaskEntity> getTask(int taskId)
        {
            double timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            Console.WriteLine(timestamp);
            var taskList = db.Queryable<TaskEntity>().Where(it => it.id == taskId && timestamp >= it.startTime && timestamp <= it.endTime && it.isDelete == false).ToList();
            return taskList;
        }
        public string sign(int taskId, int userId)
        {
            int count = db.Queryable<Group2Task, Group2User, UserDetail>((gt, gu, ud) => new JoinQueryInfos(
                JoinType.Inner, gt.taskId == taskId && gu.groupid == gt.groupId && gu.userid == userId && gu.isDelete == false && gt.isDelete == false && ud.isDelete == false,
                JoinType.Inner, ud.Id == gu.userid && gu.isDelete == false
                )).Count();
            if (count == 0) return "用户未参与本任务";
            int signCount = db.Queryable<Sign>().Where(it => it.taskid == taskId && it.userid == userId).Count();
            if(signCount != 0)
            {
                return "用户已签到";
            }
            var sign = new Sign();
            sign.userid = userId;
            sign.taskid = taskId;
            sign.signtime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); 
            int ret = db.Insertable<Sign>(sign).IgnoreColumns(it => it.id).ExecuteCommand();
            return "签到成功";
        }
        
    }
}


