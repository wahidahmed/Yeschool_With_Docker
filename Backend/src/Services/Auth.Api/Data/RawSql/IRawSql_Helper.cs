namespace Auth.Api.Data.RawSql
{
    public interface IRawSql_Helper
    {
        IEnumerable<Sql_UserInfo> GetAppContentByRole(string roleName);
        IEnumerable<Sql_UserInfo> GetMenuItem(string userName);
    }
}
