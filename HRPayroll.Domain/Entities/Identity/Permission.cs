using HRPayroll.Domain.Common;

namespace HRPayroll.Domain.Entities.Identity
{
    public class Permission: AuditableEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string GroupName { get; private set; }

        private readonly List<RolePermission> _rolePermissions = new List<RolePermission>();
        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

        private Permission() { }

        public static Permission Create(string name, string description, string groupName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(groupName);

            return new Permission
            {
                Name = name.ToLower().Trim(),
                Description = description,
                GroupName = groupName
            };
        }

    }
}
