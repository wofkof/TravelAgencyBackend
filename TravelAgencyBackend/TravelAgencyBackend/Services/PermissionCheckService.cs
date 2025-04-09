using Microsoft.EntityFrameworkCore;
using TravelAgencyBackend.Models;

namespace TravelAgencyBackend.Services
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

        private int? CurrentEmployeeId
        {
            get => _httpContext.HttpContext?.Session.GetInt32("EmployeeId");
            set
            {
                if (_httpContext.HttpContext != null)
                {
                    _httpContext.HttpContext.Session.SetInt32("EmployeeId", value ?? 0);
                }
            }
        }

        public bool HasPermission(string permissionName)
        {
            var employee = _context.Employees
                .Include(e => e.Role)
                    .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .FirstOrDefault(e => e.EmployeeId == CurrentEmployeeId);

            return employee?.Role.RolePermissions
                .Any(rp => rp.Permission.PermissionName == permissionName) ?? false;
        }

        // 權限屬性
        public bool CanViewMembers => HasPermission("查看會員");
        public bool CanManageMembers => HasPermission("管理會員");
        public bool CanEditMemberPassword => HasPermission("修改會員密碼");
        public bool CanViewParticipants => HasPermission("查看參與人");
        public bool CanManageParticipants => HasPermission("管理參與人");
        public bool CanViewEmployees => HasPermission("查看員工");
        public bool CanManageEmployees => HasPermission("管理員工");
        public bool CanManageRoles => HasPermission("管理角色");
        public bool CanSetRoles => HasPermission("設定角色權限");
        public bool CanManageChatRooms => HasPermission("管理聊天室");
        public bool CanViewAnnouncements => HasPermission("查看公告");
        public bool CanPushAnnouncements => HasPermission("發布公告");
        public bool CanManagePermissions => HasPermission("管理權限");
    }
}
