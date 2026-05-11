using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.Identity;
using HRPayroll.Domain.Entities.Organization;

namespace HRPayroll.Domain.Entities.Attendance
{
    public class LeavePolicy : AuditableEntity
    {
        public Guid CompanyId { get; private set; }
        public Guid LeaveTypeId { get; private set; }
        public Guid? GradeId { get; private set; }
        public int AllowedDays { get; private set; }
        public DateOnly EffectiveFrom { get; private set; }

        public Company Company { get; private set; }
        public LeaveType LeaveType { get; private set; }
        public Grade? Grade { get; private set; }

        private LeavePolicy() { }

        public static LeavePolicy Create(Guid companyId, Guid leaveTypeId, int allowedDays, DateOnly effectiveFrom, Guid? gradeId = null)
        {
            if (companyId == Guid.Empty)
                throw new DomainException("CompanyId must be a valid Guid.");

            if (leaveTypeId == Guid.Empty)
                throw new DomainException("LeaveTypeId must be a valid Guid.");

            if (allowedDays <= 0)
                throw new DomainException("Allowed days must be greater than zero.");

            return new LeavePolicy
            {
                CompanyId = companyId,
                LeaveTypeId = leaveTypeId,
                GradeId = gradeId,
                AllowedDays = allowedDays,
                EffectiveFrom = effectiveFrom
            };
        }

        public void Update(int allowedDays, DateOnly effectiveFrom)
        {
            if (allowedDays <= 0)
                throw new DomainException("Allowed days must be greater than zero.");

            AllowedDays = allowedDays;
            EffectiveFrom = effectiveFrom;
        }
    }

}
