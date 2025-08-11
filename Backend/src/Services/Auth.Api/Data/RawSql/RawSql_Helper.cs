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

            var sql = RawSqlQuery(string.Format(@"select c.ID AppContentId,IsParent,ParentID,DisplayName,ISNULL(Area,'')Area,ISNULL(Controller,'')Controller,ISNULL(Action,'') Action,ISNULL(DisplayOrder,0)DisplayOrder,IsDisplay_As_Menu,ISNULL(Icon,''),ISNULL(RoleName,'')RoleName from AppContents c left join AspRoleRights r on r.AppContentId=c.ID and RoleName='{0}'", roleName), x => new Sql_UserInfo
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
    }
}
