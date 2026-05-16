using HRPayroll.Domain.Common;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.EmployeeInfo
{
    public class EmploymentHistory: AuditableEntity
    {
        public Guid EmployeeId { get; private set; }
        public _ChangeType ChangeType { get; private set; }
        public string? OldValue { get; private set; }
        public string? NewValue { get; private set; }
        public DateOnly EffectiveDate { get; private set; }
        public Employee Employee { get; private set; }

        private EmploymentHistory() { }

        internal static EmploymentHistory Create(Guid employeeId, _ChangeType changeType, string? oldValue, string? newValue, DateOnly effectiveDate)
        {
            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            return new EmploymentHistory
            {
                EmployeeId = employeeId,
                ChangeType = changeType,
                OldValue = oldValue,
                NewValue = newValue,
                EffectiveDate = effectiveDate
            };
        }

    }
}
