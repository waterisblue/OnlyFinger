using SqlSugar;

namespace OnlyFingerWeb.Model
{
    [SugarTable("userdetail")]
    public class UserEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public string? username { get; set; }
        public int gender { get; set; }
        public string? desc { get; set; }
        public long createdate { get; set; }
        public bool isDelete { get; set; }
        public UserEntity(string? username, int gender, string? desc, long createdate, bool isDelete)
        {
            this.username = username;
            this.gender = gender;
            this.desc = desc;
            this.createdate = createdate;
            this.isDelete = isDelete;
        }
        public UserEntity()
        {

        }
    }
}
