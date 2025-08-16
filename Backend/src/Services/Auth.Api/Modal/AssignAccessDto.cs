namespace Auth.Api.Modal
{
    public class AssignAccessDto
    {
        public string RoleName {  get; set; }
        public IList<int> MenuIds { get; set; }
    }
}
