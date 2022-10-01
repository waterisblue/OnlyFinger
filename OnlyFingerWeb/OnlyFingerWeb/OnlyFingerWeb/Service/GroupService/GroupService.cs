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

        public ReturnCode<List<GroupEntity>> getGroups()
        {
            ReturnCode<List<GroupEntity>> returnCode = new ReturnCode<List<GroupEntity>>();
            List<GroupEntity> groupList = Db.Queryable<GroupEntity>().ToList();

            returnCode.code = 200;
            returnCode.message = groupList;
            return returnCode;
        }
    }
}
