namespace Auth.Api.Data.RawSql
{
    public interface IRawSql_Helper
    {
        IEnumerable<Sql_UserInfo> GetAppContentByRole(string roleName);
    }
}
