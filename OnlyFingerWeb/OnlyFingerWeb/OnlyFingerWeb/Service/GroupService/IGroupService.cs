using OnlyFingerWeb.Entity;
using OnlyFingerWeb.Model;

namespace OnlyFingerWeb.Service.GroupService
{
    public interface IGroupService
    {
        public ReturnCode<string> addGroup(GroupEntity group);
        public ReturnCode<string> updateGroup(int groupId, GroupEntity group);
        public ReturnCode<string> deleteGroup(int groupId);
        public ReturnCode<List<GroupEntity>> getGroups();
    }
}
