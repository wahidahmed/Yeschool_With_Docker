using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Auth.Api.Data.RawSql
{
    public class RawSql_Helper: IRawSql_Helper
    {
        private readonly AppDbContext _db;
        public RawSql_Helper(AppDbContext db)
        {
            this._db = db;
        }

        private List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            //https://entityframeworkcore.com/knowledge-base/35631903/raw-sql-query-without-dbset---entity-framework-core
            using (var command = _db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 10000;

                _db.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }
        }

        public void ExecuteSqlRaw(string sql, params object[] param)
        {
            try
            {
                _db.Database.ExecuteSqlRaw(sql, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DbBackup()
        {
            var sql = string.Format(@"exec prcDatabase_Backup");
            ExecuteSqlRaw(sql);
        }

        public IEnumerable<Sql_UserInfo> GetAppContentByRole(string roleName)
        {

            var sql = RawSqlQuery(string.Format(@"select c.ID AppContentId,IsParent,ParentID,ISNULL(DisplayName,'')DisplayName,ISNULL(Area,'')Area,ISNULL(Controller,'')Controller,ISNULL(Action,'') Action,ISNULL(DisplayOrder,0)DisplayOrder,IsDisplay_As_Menu,ISNULL(Icon,''),ISNULL(RoleName,'')RoleName from AppContents c left join AspRoleRights r on r.AppContentId=c.ID and RoleName='{0}'", roleName), x => new Sql_UserInfo
            {
                AppContentId = (int)x[0],
                IsParent = (bool)x[1],
                ParentID = (int)x[2],
                DisplayName = (string)x[3],
                Area = (string)x[4],
                Controller = (string)x[5],
                Action = (string)x[6],
                DisplayOrder = (int)x[7],
                IsDisplay_As_Menu = (bool)x[8],
                Icon = (string)x[9],
                RoleName = (string)x[10],
            });

            return sql;
        }

        public IEnumerable<Sql_UserInfo> GetMenuItem(string userName)
        {
            var rawSql = string.Format(@"select a.ID,a.IsParent,a.ParentID,ISNULL(a.DisplayName,'') as DisplayName,ISNULL(a.Area,'')Area,ISNULL(a.Controller,'')Controller,ISNULL(a.Action,'')Action,ISNULL(a.DisplayOrder,0)DisplayOrder,a.IsDisplay_As_Menu,ISNULL(a.Icon,'')Icon,ISNULL(c.RoleName,'')RoleName from AppContents a
				join AspRoleRights b on b.AppContentId=a.ID
				join Roles c on c.RoleName=b.RoleName
				join Users d on d.Role=c.RoleName
				where d.Username='{0}'", userName);
            var sql = RawSqlQuery(rawSql, x => new Sql_UserInfo
            {
                ID = (int)x[0],
                IsParent = (bool)x[1],
                ParentID = (int)x[2],
                DisplayName = (string)x[3],
                Area = (string)x[4],
                Controller = (string)x[5],
                Action = (string)x[6],
                DisplayOrder = (int)x[7],
                IsDisplay_As_Menu = (bool)x[8],
                Icon = (string)x[9],
                RoleName = (string)x[10]
            });

            return sql;
        }
    }
}
