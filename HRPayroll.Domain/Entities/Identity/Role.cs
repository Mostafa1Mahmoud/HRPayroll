using HRPayroll.Domain.Common;

namespace HRPayroll.Domain.Entities.Identity
{
    public class Role: AuditableEntity
    {
        public Guid? CompanyId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsSystemRole { get; private set; }

        private readonly List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        private readonly List<UserRole> _userRoles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

        private Role() { }

        public static Role Create(string name, string description, Guid? companyId = null, bool isSystemRole = false)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return new Role
            {
                Name = name.Trim(),
                Description = description,
                CompanyId = companyId,
                IsSystemRole = isSystemRole
            };

        }

        public void AddPermission(Permission permission)
        {
            if (_rolePermissions.Exists(rp => rp.PermissionId == permission.Id))
                return;

            _rolePermissions.Add(RolePermission.Create(Id, permission.Id));
        }

        public void RemovePermission(Guid permissionId)
        {
            var rolePermission = _rolePermissions.FirstOrDefault(rp => rp.PermissionId == permissionId);
            if (rolePermission is not null)
                _rolePermissions.Remove(rolePermission);
        }
    }
}
