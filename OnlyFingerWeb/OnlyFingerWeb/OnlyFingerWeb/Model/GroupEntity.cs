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
        public string? desc { get; set; }
        public bool isDelete { get; set; }
        public GroupEntity(string? groupName, int memberCount, long createTime, string? desc, bool isDelete)
        {
            this.groupName = groupName;
            this.memberCount = memberCount;
            this.createTime = createTime;
            this.desc = desc;
            this.isDelete = isDelete;
        }
        public GroupEntity()
        {

        }
    }
}
