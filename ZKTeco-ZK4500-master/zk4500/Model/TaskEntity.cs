using SqlSugar;

namespace OnlyFingerWeb.Entity
{
    [SugarTable("task")]
    public class TaskEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public string taskName { get; set; }
        public long startTime { get; set; }
        public long endTime { get; set; }
        public string desc { get; set; }
        public bool isDelete { get; set; }
        public TaskEntity(string taskName, long startTime, long endTime, bool isDelete, string desc)
        {
            this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.isDelete = isDelete;
            this.desc = desc;
        }
        public TaskEntity()
        {

        }
    }
}
