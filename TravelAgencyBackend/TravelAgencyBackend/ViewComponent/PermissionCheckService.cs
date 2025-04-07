using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.ViewComponent
{
    public class PermissionCheckService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public PermissionCheckService(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public bool HasPermission(string permissionName) 
        {
            var employeeIdClaim = _httpContext.HttpContext?.User.FindFirst("EmployeeId")?.Value;

            int employeeId = string.IsNullOrEmpty(employeeIdClaim)
                ? 1
                : int.Parse(employeeIdClaim);

            var employee = _context.Employees
                .Include(e => e.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            return employee?.Role.RolePermissions
                .Any(rp => rp.Permission.PermissionName == permissionName) ?? false;
        }
    }
}
