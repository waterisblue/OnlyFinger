using OnlyFingerWeb.Entity;

namespace OnlyFingerWeb.Model
{
    public class TaskWithGroup
    {
        public int id { get; set; }
        public string? taskName { get; set; }
        public long startTime { get; set; }
        public long endTime { get; set; }
        public string? desc { get; set; }
        public bool isDelete { get; set; }
        public List<GroupEntity>? groupEntities { get; set; }
        public TaskWithGroup(TaskEntity task, List<GroupEntity> groupEntities)
        {
            this.id = task.id;
            this.taskName = task.taskName;
            this.startTime = task.startTime;
            this.endTime = task.endTime;
            this.desc = task.desc;
            this.isDelete = task.isDelete;
            this.groupEntities = groupEntities;
        }
    }
}
