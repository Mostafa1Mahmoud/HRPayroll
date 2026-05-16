using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.EmployeeInfo;
using HRPayroll.Domain.Entities.Identity;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.Attendance
{
    public class LeaveRequest
    {
        public Guid EmployeeId { get; private set; }
        public Guid LeaveTypeId { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public int TotalDays { get; private set; }
        public string? Reason { get; private set; }
        public _LeaveStatus Status { get; private set; }
        public Guid? ReviewedBy { get; private set; }
        public DateTime? ReviewedAt { get; private set; }

        public Employee Employee { get; private set; }
        public LeaveType LeaveType { get; private set; }
        public User? Reviewer { get; private set; }

        private LeaveRequest() { }

        public static LeaveRequest Create(Guid employeeId, Guid leaveTypeId, DateOnly startDate, DateOnly endDate, int totalDays, string? reason = null)
        {
            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            if (leaveTypeId == Guid.Empty)
                throw new DomainException("LeaveTypeId must be a valid Guid.");

            if (endDate < startDate)
                throw new DomainException("End date cannot be before start date.");

            if (totalDays <= 0)
                throw new DomainException("Total days must be greater than zero.");

            return new LeaveRequest
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveTypeId,
                StartDate = startDate,
                EndDate = endDate,
                TotalDays = totalDays,
                Reason = reason?.Trim(),
                Status = _LeaveStatus.Pending
            };
        }

        public void Approve(Guid reviewedBy)
        {
            if (Status != _LeaveStatus.Pending)
                throw new DomainException("Only pending requests can be approved.");

            if (reviewedBy == Guid.Empty)
                throw new DomainException("ReviewedBy must be a valid Guid.");

            Status = _LeaveStatus.Approved;
            ReviewedBy = reviewedBy;
            ReviewedAt = DateTime.UtcNow;
        }

        public void Reject(Guid reviewedBy)
        {
            if (Status != _LeaveStatus.Pending)
                throw new DomainException("Only pending requests can be rejected.");

            if (reviewedBy == Guid.Empty)
                throw new DomainException("ReviewedBy must be a valid Guid.");

            Status = _LeaveStatus.Rejected;
            ReviewedBy = reviewedBy;
            ReviewedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status != _LeaveStatus.Pending)
                throw new DomainException("Only pending requests can be cancelled.");

            Status = _LeaveStatus.Rejected;
        }
    }
}