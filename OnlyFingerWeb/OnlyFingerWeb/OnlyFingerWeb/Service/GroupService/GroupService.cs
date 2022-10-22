using Furion.DependencyInjection;
using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;
using SqlSugar;

namespace OnlyFingerWeb.Service.GroupService
{
    public class GroupService : IGroupService, ITransient
    {
        public ISqlSugarClient Db { get; }

        public GroupService(ISqlSugarClient db)
        {
            Db = db;
        }

        public ReturnCode<string> addGroup(GroupEntity group)
        {
            ReturnCode<string> returnCode = new ReturnCode<string>();

            // 获取当前时间戳
            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            group.createTime = timestamp;
            group.memberCount = 0;
            int ret = Db.Insertable(group).IgnoreColumns(it => new { it.id }).ExecuteCommand();

            if(ret != 1)
            {
                returnCode.code = 500;
                returnCode.message = "组插入到数据库失败，返回行数不为1";
                return returnCode;
            }

            returnCode.code = 200;
            returnCode.message = "插入组成功，插入行数：" + ret;
            return returnCode;
        }

        public ReturnCode<string> updateGroup(int groupId, GroupEntity group)
        {
            ReturnCode<string> returnCode = new ReturnCode<string>();

            group.id = groupId;
            int ret = Db.Updateable(group).UpdateColumns(it => new {it.groupName}).ExecuteCommand();

            if(ret != 1)
            {
                returnCode.code = 500;
                returnCode.message = "修改组信息失败，返回行数不为1";
                return returnCode;
            }
            returnCode.code = 200;
            returnCode.message = "修改组成功，修改行数：" + ret;
            return returnCode;
        }

        public ReturnCode<string> deleteGroup(int groupId)
        {
            ReturnCode<string> returnCode = new ReturnCode<string>();
            Db.Deleteable<GroupEntity>().Where(new GroupEntity() { id = groupId }).ExecuteCommand();
            //TODO: 暂未实现，涉及到联表问题

            return returnCode;
        }

        public ReturnCode<List<GroupEntity>> getGroups(int start, int limit)
        {
            int totalCount = 0;
            ReturnCode<List<GroupEntity>> returnCode = new ReturnCode<List<GroupEntity>>();
            List<GroupEntity> groupList = Db.Queryable<GroupEntity>().Where(it => it.isDelete == false).ToPageList(start, limit, ref totalCount);

            returnCode.code = 200;
            returnCode.message = "查询成功。";
            returnCode.data = groupList;
            return returnCode;
        }

        public ReturnCode<int> getGroupNum()
        {
            ReturnCode<int> returnCode = new ReturnCode<int>();
            int count = Db.Queryable<GroupEntity>().Where(it => it.isDelete == false).Count();

            returnCode.code = 200;
            returnCode.message = "查询成功。";
            returnCode.data = count;
            return returnCode;
        }

        public ReturnCode<List<GroupEntity>> searchGroup(string searchStr)
        {
            var returnCode = new ReturnCode<List<GroupEntity>>();
            var exp = Expressionable.Create<GroupEntity>();

            if (searchStr == null || searchStr.Equals(""))
            {
                returnCode.code = 500;
                returnCode.message = "查询失败，不能输入空查询字符。";
                returnCode.data = null;
                return returnCode;
            }

            var groupList = new List<GroupEntity> ();

            exp.Or(it => it.groupName.Contains(searchStr));
            exp.Or(it => it.isDelete == false);

            int parseId;

            bool tryParseBool = int.TryParse(searchStr, out parseId);


            if (tryParseBool)
            {
                exp.Or(it => it.id == parseId);
            }
            groupList = Db.Queryable<GroupEntity>().Where(exp.ToExpression()).ToList();

            
            returnCode.code = 200;
            returnCode.message = "查询成功。";
            returnCode.data = groupList;

            return returnCode;
        }
    }
}
