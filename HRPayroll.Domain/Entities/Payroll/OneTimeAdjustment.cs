using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.EmployeeInfo;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.Payroll
{
    public class OneTimeAdjustment
    {
        public Guid EmployeeId { get; private set; }
        public Guid PayrollrunId { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }
        public _ComponentType ComponentType { get; private set; }
        public decimal Amount { get; private set; }
        public string Reason { get; private set; }

        public Employee Employee { get; private set; }
        public PayrollRun PayrollRun { get; private set; }

        private OneTimeAdjustment() { }

        public static OneTimeAdjustment Create(Guid employeeId, Guid payrollrunId, int month, int year, _ComponentType componentType, decimal amount, string reason)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(reason);
            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            if (payrollrunId == Guid.Empty)
                throw new DomainException("PayrollrunId must be a valid Guid.");

            if (month < 1 || month > 12)
                throw new DomainException("Month must be between 1 and 12.");

            if (year < 2000 || year > DateTime.UtcNow.Year + 1)
                throw new DomainException("Year must be between 2000 and next year.");

            if (amount <= 0)
                throw new DomainException("Amount must be greater than zero.");

            return new OneTimeAdjustment
            {
                EmployeeId = employeeId,
                PayrollrunId = payrollrunId,
                Month = month,
                Year = year,
                ComponentType = componentType,
                Amount = amount,
                Reason = reason.Trim()
            };
        }
    }
}
