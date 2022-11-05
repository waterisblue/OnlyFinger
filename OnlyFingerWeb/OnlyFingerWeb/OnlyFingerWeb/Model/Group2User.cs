using SqlSugar;

namespace OnlyFingerWeb.Model
{
    [SugarTable("user2group")]
    public class Group2User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public int groupid { get; set; }
        public int userid { get; set; }
        public long createtime { get; set; }
        public bool isDelete { get; set; }

        public Group2User(int groupid, int userid, long createtime, bool isDelete)
        {
            this.groupid = groupid;
            this.userid = userid;
            this.createtime = createtime;
            this.isDelete = isDelete; 
        }

        public Group2User()
        {

        }
    }
}
