namespace TravelAgencyBackend.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;
        
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
