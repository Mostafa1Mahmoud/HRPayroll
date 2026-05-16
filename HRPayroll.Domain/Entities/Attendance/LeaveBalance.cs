
using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.EmployeeInfo;

namespace HRPayroll.Domain.Entities.Attendance
{
    public class LeaveBalance
    {
        public Guid EmployeeId { get; private set; }
        public Guid LeaveTypeId { get; private set; }
        public int Year { get; private set; }
        public int TotalDays { get; private set; }
        public int CarriedForwardDays { get; private set; }
        public int UsedDays { get; private set; }

        public int RemainingDays => TotalDays + CarriedForwardDays - UsedDays;

        public Employee Employee { get; private set; }
        public LeaveType LeaveType { get; private set; }

        private LeaveBalance() { }

        public static LeaveBalance Create(Guid employeeId, Guid leaveTypeId, int year, int totalDays, int carriedForwardDays = 0)
        {
            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            if (leaveTypeId == Guid.Empty)
                throw new DomainException("LeaveTypeId must be a valid Guid.");

            if (year < 2000 || year > 2100)
                throw new DomainException("Year must be a valid calendar year.");

            if (totalDays <= 0)
                throw new DomainException("Total days must be greater than zero.");

            if (carriedForwardDays < 0)
                throw new DomainException("Carried forward days cannot be negative.");

            return new LeaveBalance
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveTypeId,
                Year = year,
                TotalDays = totalDays,
                CarriedForwardDays = carriedForwardDays,
                UsedDays = 0
            };
        }

        public void Consume(int days)
        {
            if (days <= 0)
                throw new DomainException("Days to consume must be greater than zero.");

            if (days > RemainingDays)
                throw new DomainException($"Insufficient leave balance. Remaining: {RemainingDays} days.");

            UsedDays += days;
        }

        public void Restore(int days)
        {
            if (days <= 0)
                throw new DomainException("Days to restore must be greater than zero.");

            if (days > UsedDays)
                throw new DomainException("Cannot restore more days than have been used.");

            UsedDays -= days;
        }
    }
}
