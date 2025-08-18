namespace Auth.Api.Data.Entties
{
    public class AppContent
    {
        public int ID { get; set; }
        public bool IsParent { get; set; }
        public int ParentID { get; set; }
        public bool IsDisplay_As_Menu { get; set; }
        public int DisplayOrder { get; set; }
        public string DisplayName { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Icon { get; set; }
        public string RoutePath {  get; set; }
    }
}
