using Furion.DependencyInjection;
using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;
using SqlSugar;

namespace OnlyFingerWeb.Service.UserService
{
    public class UserService : IUserService, ITransient
    {
        public ISqlSugarClient Db { get; }

        public UserService(ISqlSugarClient db)
        {
            Db = db;
        }
        public ReturnCode<List<UserEntity>> getUserByPage(int start, int end)
        {
            ReturnCode<List<UserEntity>> returnCode = new ReturnCode<List<UserEntity>>();
            if (start >= end || start < 0 || end < 0)
            {
                returnCode.code = 500;
                returnCode.message = "页码输入错误";
                return returnCode;
            }

            int totalCount = 0;
            List<UserEntity> userList = Db.Queryable<UserEntity>().Where(it => it.isDelete == false).ToPageList(start, end, ref totalCount);
            
            returnCode.code = 200;
            returnCode.message = "查询成功。";
            returnCode.data = userList;
            return returnCode;
        }

        public ReturnCode<List<UserEntity>> getUserBySearch(string searchStr)
        {
            throw new NotImplementedException();
        }

        public ReturnCode<int> getUserCount()
        {
            ReturnCode<int> returnCode = new ReturnCode<int>();
            int count = Db.Queryable<UserEntity>().Where(it => it.isDelete == false).Count();
            
            returnCode.code = 200;
            returnCode.message = "查询成功。";
            returnCode.data = count;
            return returnCode;
        }

        public ReturnCode<UserEntity> getUserById(int userId)
        {
            ReturnCode<UserEntity> returnCode = new ReturnCode<UserEntity>();
            List<UserEntity> userEntities = Db.Queryable<UserEntity>()
                .Where(it => it.id == userId && !it.isDelete)
                .ToList();
            if(userEntities.Count != 1)
            {
                returnCode.code = 500;
                returnCode.message = "未查询到该用户";
                return returnCode;
            }
            returnCode.code = 200;
            returnCode.message = "查询成功";
            returnCode.data = userEntities.First(); 
            return returnCode;
        }

        public ReturnCode<List<UserEntity>> searchUser(string searchStr)
        {
            var returnCode = new ReturnCode<List<UserEntity>>();
            var exp = Expressionable.Create<UserEntity>();

            if (searchStr == null || searchStr.Equals(""))
            {
                returnCode.code = 500;
                returnCode.message = "查询失败，不能输入空查询字符。";
                returnCode.data = null;
                return returnCode;
            }

            var userList = new List<UserEntity>();

            exp.Or(it => it.username.Contains(searchStr));
            exp.And(it => it.isDelete == false);

            int parseId;

            bool tryParseBool = int.TryParse(searchStr, out parseId);


            if (tryParseBool)
            {
                exp.Or(it => it.id == parseId);
            }
            userList = Db.Queryable<UserEntity>().Where(exp.ToExpression()).ToList();


            returnCode.code = 200;
            returnCode.message = "查询成功。";
            returnCode.data = userList;

            return returnCode;
        }

        public ReturnCode<string> updateUserById(int userId, string userName, string desc, int gender)
        {
            throw new NotImplementedException();
        }

        public ReturnCode<string> deleteUserById(int userId)
        {
            var returnCode = new ReturnCode<string>();
            int ret = Db.Updateable<UserEntity>()
                .SetColumns(it => it.isDelete == true)
                .Where(it => it.id == userId)
                .ExecuteCommand();
            if (ret != 1)
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
