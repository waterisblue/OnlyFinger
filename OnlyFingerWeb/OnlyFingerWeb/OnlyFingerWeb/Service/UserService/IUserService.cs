using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;

namespace OnlyFingerWeb.Service.UserService
{
    public interface IUserService
    {
        ReturnCode<List<UserEntity>> getUserByPage(int start, int end);
        ReturnCode<List<UserEntity>> getUserBySearch(string searchStr);
        // ReturnCode<Dictionary<string, string>> getUserDetailById(int id);
        ReturnCode<int> getUserCount();
        ReturnCode<UserEntity> getUserById(int userId);
    }
}
