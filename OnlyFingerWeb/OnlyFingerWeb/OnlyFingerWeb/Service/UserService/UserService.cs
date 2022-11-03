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
    }
}
