using HRPayroll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPayroll.Domain.Entities.Identity
{
    public class RolePermission
    {
        public Guid RoleId { get; private set; }
        public Guid PermissionId { get; private set; }
        public Role Role { get; private set; }
        public Permission Permission { get; private set; }

        private RolePermission() { }

        public static RolePermission Create(Guid roleId, Guid permissionId)
        {
            return new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };
        }

    }
}
