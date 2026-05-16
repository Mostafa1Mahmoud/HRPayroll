using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.Identity;

namespace HRPayroll.Domain.Entities.Attendance
{
    public class PublicHoliday : AuditableEntity
    {
        public Guid CompanyId { get; private set; }
        public string Name { get; private set; }
        public DateOnly Date { get; private set; }
        public bool IsRecurring { get; private set; }

        public Company Company { get; private set; }

        private PublicHoliday() { }

        public static PublicHoliday Create(Guid companyId, string name, DateOnly date, bool isRecurring = false)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if (companyId == Guid.Empty)
                throw new DomainException("CompanyId must be a valid Guid.");

            return new PublicHoliday
            {
                CompanyId = companyId,
                Name = name.Trim(),
                Date = date,
                IsRecurring = isRecurring
            };
        }

        public void Update(string name, DateOnly date, bool isRecurring)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            Name = name.Trim();
            Date = date;
            IsRecurring = isRecurring;
        }
    }
}