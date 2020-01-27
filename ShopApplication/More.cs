using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication
{
    class More
    {
        public static void Backup(String path)
        {
            Ezzat.ExecutedNoneQuery("Backup_db", new System.Data.SqlClient.SqlParameter("@path", path));
        }
        public static void Restore(String path)
        {
            string query = string.Format("alter database  [ShopDB] set offline with ROLLBACK IMMEDIATE;RESTORE DATABASE[ShopDB] FROM  DISK = '{0}';", path);
            Ezzat.ExecutedNoneQuery(query);
        }
    }
}
