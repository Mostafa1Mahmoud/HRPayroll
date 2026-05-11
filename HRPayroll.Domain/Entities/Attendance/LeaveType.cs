using HRPayroll.Domain.Common;

namespace HRPayroll.Domain.Entities.Attendance
{
    public class LeaveType : AuditableEntity
    {
        public string Name { get; private set; }
        public int DefaultDaysPerYear { get; private set; }
        public bool IsPaid { get; private set; }
        public bool AllowCarryForward { get; private set; }
        public int? MaxCarryForwardDays { get; private set; }

        private readonly List<LeavePolicy> _leavePolicies = [];
        public IReadOnlyCollection<LeavePolicy> LeavePolicies => _leavePolicies.AsReadOnly();

        private readonly List<LeaveBalance> _leaveBalances = [];
        public IReadOnlyCollection<LeaveBalance> LeaveBalances => _leaveBalances.AsReadOnly();

        private LeaveType() { }

        public static LeaveType Create(string name, int defaultDaysPerYear, bool isPaid, bool allowCarryForward = false, int? maxCarryForwardDays = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if (defaultDaysPerYear <= 0)
                throw new DomainException("Default days per year must be greater than zero.");

            if (maxCarryForwardDays.HasValue && maxCarryForwardDays.Value <= 0)
                throw new DomainException("Max carry forward days must be greater than zero.");

            if (!allowCarryForward && maxCarryForwardDays.HasValue)
                throw new DomainException("Cannot set max carry forward days when carry forward is disabled.");

            return new LeaveType
            {
                Name = name.Trim(),
                DefaultDaysPerYear = defaultDaysPerYear,
                IsPaid = isPaid,
                AllowCarryForward = allowCarryForward,
                MaxCarryForwardDays = maxCarryForwardDays
            };
        }

        public void Update(string name, int defaultDaysPerYear, bool isPaid, bool allowCarryForward, int? maxCarryForwardDays)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if (defaultDaysPerYear <= 0)
                throw new DomainException("Default days per year must be greater than zero.");

            if (!allowCarryForward && maxCarryForwardDays.HasValue)
                throw new DomainException("Cannot set max carry forward days when carry forward is disabled.");

            Name = name.Trim();
            DefaultDaysPerYear = defaultDaysPerYear;
            IsPaid = isPaid;
            AllowCarryForward = allowCarryForward;
            MaxCarryForwardDays = maxCarryForwardDays;
        }
    }

}
