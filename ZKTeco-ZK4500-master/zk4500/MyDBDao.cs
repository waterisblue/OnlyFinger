using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zk4500
{
    class MyDBDao
    {
        SqlSugarScope db = SqlSugarConnect.getConnect();

        public int registerUser(UserDetail ud)
        {
            return db.Insertable(ud).IgnoreColumns(it => new { it.Id }).ExecuteReturnIdentity();
        }
    }
}
