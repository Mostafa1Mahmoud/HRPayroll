using HRPayroll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPayroll.Domain.Entities.Identity
{
    public class UserRole
    {
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        public User User { get; private set; }
        public Role Role { get; private set; }

        private UserRole() { }

        public static UserRole Create(Guid userId, Guid roleId)
        {
            return new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
        }
    }
}
