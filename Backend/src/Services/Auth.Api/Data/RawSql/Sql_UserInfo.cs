namespace Auth.Api.Data.RawSql
{
    public class Sql_UserInfo
    {
        public string UserId { get; set; }
        public string RoutePath { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int AppContentId { get; set; }
        public int ID { get; set; }
        public int ParentID { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsParent { get; set; }
        public bool IsDisplay_As_Menu { get; set; }
        public bool IsStudent { get; set; }
        public string Icon { get; set; }
        public string RoleName { get; set; }
    }
}
