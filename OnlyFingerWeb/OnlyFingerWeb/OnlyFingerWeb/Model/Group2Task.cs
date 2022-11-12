using SqlSugar;

namespace OnlyFingerWeb.Model
{
    [SugarTable("group2task")]
    public class Group2Task
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public int groupId { get; set; }
        public int taskId { get; set; }
        public long createTime { get; set; } 
        public bool isDelete { get; set; }
        public Group2Task(int groupId, int taskId, long createTime, bool isDelete)
        {
            this.groupId = groupId;
            this.taskId = taskId;
            this.createTime = createTime;
            this.isDelete = isDelete;
        }
        public Group2Task()
        {

        }
    }
}
