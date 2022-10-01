using SqlSugar;

namespace OnlyFingerWeb.Entity
{
    [SugarTable("task")]
    public class TaskEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public String? taskName { get; set; }
        public long startTime { get; set; }
        public long endTime { get; set; }
        public int group { get; set; }
        public TaskEntity(string? taskName, long startTime, long endTime, int group)
        {
            this.taskName = taskName;
            this.startTime = startTime;
            this.endTime = endTime;
            this.group = group;
        }  
        public TaskEntity()
        {

        }
    }
}
