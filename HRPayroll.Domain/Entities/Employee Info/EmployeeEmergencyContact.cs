using HRPayroll.Domain.Common;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.EmployeeInfo
{
    public class EmployeeEmergencyContact: AuditableEntity
    {
        public Guid EmployeeId { get; private set; }
        public string FullName { get; private set; }
        public _RelationshipType Relationship { get; private set; }
        public string Phone { get; private set; }
        public string? Email { get; private set; }
        public bool IsPrimary { get; private set; }

        public Employee Employee { get; private set; }

        private EmployeeEmergencyContact() { }

        public static EmployeeEmergencyContact Create(Guid employeeId, string fullName, _RelationshipType relationship, string phone, string? email = null, bool isPrimary = false)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(phone);

            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            return new EmployeeEmergencyContact
            {
                EmployeeId = employeeId,
                FullName = fullName.Trim(),
                Relationship = relationship,
                Phone = phone.Trim(),
                Email = email?.Trim().ToLower(),
                IsPrimary = isPrimary
            };
        }

        public void SetAsPrimary() => IsPrimary = true;
        public void UnsetPrimary() => IsPrimary = false;

        public void Update(string fullName, _RelationshipType relationship, string phone, string? email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
            ArgumentException.ThrowIfNullOrWhiteSpace(phone);

            FullName = fullName.Trim();
            Relationship = relationship;
            Phone = phone.Trim();
            Email = email?.Trim().ToLower();
        }
    }
}
