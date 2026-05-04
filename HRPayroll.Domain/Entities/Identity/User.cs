using HRPayroll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPayroll.Domain.Entities.Identity
{
    public class User: AuditableEntity
    {
        public Guid CompanyId { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PasswordHash { get; private set; }
        public string FullName => $"{FirstName} {LastName}";
        public Company Company { get; private set; }

        private readonly List<UserRole> _userRoles = new List<UserRole>();
        public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

        private User() { }

        public static User Create(Guid companyId, string email, string firstName, string lastName, string passwordHash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

            return new User
            {
                CompanyId = companyId,
                Email = email.ToLower().Trim(),
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                PasswordHash = passwordHash
            };
        }

        public void AssignRole(Guid roleId)
        {
            if (_userRoles.Exists(ur => ur.RoleId == roleId))
                return;

            _userRoles.Add(UserRole.Create(Id, roleId));
        }

        public void RemoveRole(Guid roleId)
        {
            var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == roleId);
            if (userRole != null)
            {
                _userRoles.Remove(userRole);
            }
        }

        public void UpdatePassword(string newPasswordHash)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(newPasswordHash);
            PasswordHash = newPasswordHash;
        }

        public void UpdateProfile(string firstName, string lastName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
        }
    }
}
