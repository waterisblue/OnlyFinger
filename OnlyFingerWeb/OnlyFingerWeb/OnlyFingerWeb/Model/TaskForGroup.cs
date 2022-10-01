namespace OnlyFingerWeb.Model
{
    public class TaskForGroup
    {
        public int id { get; set; }
        public int groupId { get; set; }
        public int taskId { get; set; }
        public long createTime { get; set; }
    }
}
