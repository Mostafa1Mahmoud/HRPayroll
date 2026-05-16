using HRPayroll.Domain.Common;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.EmployeeInfo
{
    public class EmployeePersonalInfo: AuditableEntity
    {
        public Guid EmployeeId { get; private set; }
        public DateOnly? DateOfBirth { get; private set; }
        public _Gender? Gender { get; private set; }
        public string? NationalId { get; private set; }
        public string? Phone { get; private set; }
        public string? Address { get; private set; }
        public string? Nationality { get; private set; }
        public Employee Employee { get; private set; }

        private EmployeePersonalInfo() { }

        public static EmployeePersonalInfo Create(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            return new EmployeePersonalInfo
            {
                EmployeeId = employeeId
            };
        }

        public void Update(DateOnly? dateOfBirth, _Gender? gender, string? nationalId, string? phone, string? address, string? nationality)
        {
            if (dateOfBirth.HasValue && dateOfBirth.Value > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new DomainException("Date of birth cannot be in the future.");

            DateOfBirth = dateOfBirth;
            Gender = gender;
            NationalId = nationalId?.Trim();
            Phone = phone?.Trim();
            Address = address?.Trim();
            Nationality = nationality?.Trim();
        }

    }
}
