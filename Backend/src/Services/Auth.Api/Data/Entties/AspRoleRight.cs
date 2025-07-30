using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Api.Data.Entties
{
    public class AspRoleRight
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        [ForeignKey("AppContent")]
        public int AppContentId { get; set; }
        public AppContent AppContent { get; set; }
    }
}
