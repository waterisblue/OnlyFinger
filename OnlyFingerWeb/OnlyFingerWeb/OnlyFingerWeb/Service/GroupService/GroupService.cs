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
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

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
            int ret = Db.Updateable(group).UpdateColumns(it => new {it.groupName, it.desc}).ExecuteCommand();

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
            var res = Db.Updateable<GroupEntity>()
                .SetColumns(it => it.isDelete == true)
                .Where(it => it.id == groupId)
                .ExecuteCommand();

            if (res != 1)
            {
                returnCode.code = 500;
                returnCode.message = "删除行数不唯一，删除行数：" + res;
                return returnCode;
            }
            Db.Updateable<Group2Task>()
                .Where(it => it.groupId == groupId)
                .SetColumns(it => it.isDelete == true)
                .ExecuteCommand();
            Db.Updateable<Group2User>()
                .Where(it => it.groupid == groupId)
                .SetColumns(it => it.isDelete == true)
                .ExecuteCommand();
            returnCode.code = 200;
            returnCode.message = "删除成功";
            returnCode.data = 1 + "";
            return returnCode;
        }

        public ReturnCode<List<GroupEntity>> getGroups(int start, int limit)
        {
            ReturnCode<List<GroupEntity>> returnCode = new ReturnCode<List<GroupEntity>>();
            if(start >= limit || start < 0 || limit < 0)
            {
                returnCode.code = 500;
                returnCode.message = "页码输入错误";
                return returnCode;
            }
            int totalCount = 0;
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
            exp.And(it => it.isDelete == false);

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

        public ReturnCode<int> addUser2Group(int userId, int groupId)
        {
            ReturnCode<int> returnCode = new ReturnCode<int> ();
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            // 检查该用户是否已加入组
            int checkListCount = Db.Queryable<Group2User>().Where(it => it.userid == userId && it.groupid == groupId && it.isDelete == false).Count();
            if(checkListCount != 0)
            {
                returnCode.code = 304;
                returnCode.message = "用户已加入该组";
                returnCode.data = checkListCount;
                return returnCode;
            }

            var group2UserEntity = new Group2User(groupId, userId, timestamp, false);
            try
            {
               int ret = Db.Insertable(group2UserEntity).IgnoreColumns(it => it.id).ExecuteCommand();
                // 增加组成员人数
                int incMember = Db.Updateable<GroupEntity>()
                .SetColumns(it => it.memberCount == it.memberCount + 1)
                .Where(it => it.id == groupId)
                .ExecuteCommand();

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
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                returnCode.code = 304;
                returnCode.message = "没有该组或用户";
                returnCode.data = -1;
                return returnCode;
            } 
    
        }

        public ReturnCode<List<Group2UserJoinResult>> getUserByGroupId(int groupId)
        {
            List<Group2UserJoinResult> group2Users = Db.Queryable<Group2User>()
                .LeftJoin<GroupEntity>((gu, g) => gu.groupid == g.id)
                .LeftJoin<UserEntity>((gu, g, u) => gu.userid == u.id)
                .Where((gu, g, u) => gu.groupid == groupId && !gu.isDelete && !g.isDelete && !u.isDelete)
                .Select((gu, g, u) => 
                new Group2UserJoinResult { id = gu.id, groupid = g.id, userid = u.id, 
                    groupname = g.groupName, username = u.username, createtime = gu.createtime, gender = u.gender})
                .ToList();
            var returnCode = new ReturnCode<List<Group2UserJoinResult>>();
            returnCode.code = 200;
            returnCode.message = "查询成功";
            returnCode.data = group2Users;
            return returnCode;
        }

        public ReturnCode<string> deleteGroup2UserById(int groupId, int userId)
        {
            var returnCode = new ReturnCode<string>();
            int ret = Db.Updateable<Group2User>()
                .SetColumns(it => it.isDelete == true)
                .Where(it => it.groupid == groupId && it.userid == userId)
                .ExecuteCommand();
            if(ret != 1)
            {
                returnCode.code = 304;
                returnCode.message = "删除行数不唯一，请检查数据是否正确";
                return returnCode;
            }
            returnCode.code = 200;
            returnCode.message = "删除成功";
            returnCode.data = "修改行数为" + ret;
            return returnCode;
        }
    }
}
