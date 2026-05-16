using HRPayroll.Domain.Common;

namespace HRPayroll.Domain.Entities.Identity
{
    public class Company: AuditableEntity
    {
        public string Name { get; private set; }
        public string? LogoUrl { get; private set; }

        private readonly List<User> _users = new List<User>();
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

        private readonly List<Role> _roles = new List<Role>();
        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

        private Company() { }

        public static Company Create(string name, string? logoUrl = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            return new Company
            {
                Name = name.Trim(),
                LogoUrl = logoUrl?.Trim()
            };
        }

        public void UpdateLogo(string logoUrl)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(logoUrl);
            LogoUrl = logoUrl;
        }

        public void UpdateName(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            Name = name.Trim();
        }

    }
}
