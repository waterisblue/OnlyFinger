using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zk4500
{
    class SqlSugarConnect
    {
        static SqlSugarScope db = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = "database='fingerproject';Data Source = '127.0.0.1'; User Id = 'root'; pwd='zhang';charset='utf8';pooling=true",//连接符字串
            DbType = DbType.MySql,//数据库类型
            IsAutoCloseConnection = true
        });

        public static SqlSugarScope getConnect()
        {
            return db;
        }
    }
}
