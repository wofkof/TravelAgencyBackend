namespace TravelAgencyBackend.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }
}
