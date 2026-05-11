using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.Identity;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.Payroll
{
    public class PayrollRun
    {
        public Guid CompanyId { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }
        public _PayrollStatus Status { get; private set; }
        public Guid ApprovedById { get; private set; }
        public DateTime ApprovedAt { get; private set; }

        public Company Company { get; private set; }
        public User ApprovedBy { get; private set; }

        private PayrollRun() { }

        public static PayrollRun Create(Guid companyId, int month, int year)
        {
            if (companyId == Guid.Empty)
                throw new DomainException("CompanyId must be a valid Guid.");

            if (month < 1 || month > 12)
                throw new DomainException("Month must be between 1 and 12.");

            if (year < 1900 || year > DateTime.Now.Year)
                throw new DomainException("Year must be between 1900 and the current year.");

            return new PayrollRun
            {
                CompanyId = companyId,
                Month = month,
                Year = year,
                Status = _PayrollStatus.Draft,
            };
        }

        public void Approve(Guid approvedById)
        {
            if (approvedById == Guid.Empty)
                throw new DomainException("ApprovedById must be a valid Guid.");

            if (Status != _PayrollStatus.Draft)
                throw new DomainException("Only payroll runs in Draft status can be approved.");

            Status = _PayrollStatus.Approved;
            ApprovedById = approvedById;
            ApprovedAt = DateTime.UtcNow;
        }

        public void Finalize()
        {
            if (Status != _PayrollStatus.Approved)
                throw new DomainException("Only payroll runs in Approved status can be finalized.");
            Status = _PayrollStatus.Finalized;
        }
    }
}
