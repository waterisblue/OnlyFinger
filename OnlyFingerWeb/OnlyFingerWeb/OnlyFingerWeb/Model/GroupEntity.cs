using SqlSugar;

namespace OnlyFingerWeb.Model
{
    [SugarTable("group")]
    public class GroupEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public string? groupName { get; set; }
        public int memberCount { get; set; }
        public long createTime { get; set; }
        public GroupEntity(string? groupName, int memberCount, long createTime)
        {
            this.groupName = groupName;
            this.memberCount = memberCount;
            this.createTime = createTime;
        }
        public GroupEntity()
        {

        }
    }
}
