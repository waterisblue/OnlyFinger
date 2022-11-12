using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zk4500
{
    class UserDetail
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public string username { get; set; }
        public string fingertemp { get; set; }
        public string desc { get; set; }
        public long createdate { get; set; }
        public bool isDelete { get; set; }
        public int gender { get; set; }

        public UserDetail(string userName, string fingertemp, string desc, long createtime, bool isDelete, int gender)
        {
            this.username = userName;
            this.fingertemp = fingertemp;
            this.desc = desc;
            this.createdate = createtime;
            this.isDelete = isDelete;
            this.gender = gender;
        }
        public UserDetail()
        {

        }
    }
}
