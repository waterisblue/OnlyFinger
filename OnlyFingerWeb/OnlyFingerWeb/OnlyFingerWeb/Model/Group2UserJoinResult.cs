namespace OnlyFingerWeb.Model
{
    public class Group2UserJoinResult
    {
        public int id { get; set; }
        public int groupid { get; set; }
        public int userid { get; set; }
        public string? username { get; set; }
        public string? groupname { get; set; }
        public long createtime { get; set; }
        public bool isDelete { get; set; }
        public int gender { get; set; }

        public Group2UserJoinResult()
        {

        }
    }
}
