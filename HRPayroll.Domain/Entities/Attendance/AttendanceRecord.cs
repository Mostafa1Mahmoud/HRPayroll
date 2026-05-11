using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.EmployeeInfo;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.Attendance
{
    public class AttendanceRecord
    {
        public Guid EmployeeId { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly? CheckIn { get; private set; }
        public TimeOnly? CheckOut { get; private set; }
        public decimal? TotalHours { get; private set; }
        public _AttendanceStatus Status { get; private set; }

        public Employee Employee { get; private set; }

        private AttendanceRecord() { }

        public static AttendanceRecord Create(Guid employeeId, DateOnly date, _AttendanceStatus status, TimeOnly? checkIn = null, TimeOnly? checkOut = null)
        {
            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            if (date > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new DomainException("Attendance date cannot be in the future.");

            if (checkOut.HasValue && checkIn.HasValue && checkOut.Value <= checkIn.Value)
                throw new DomainException("Check-out time must be after check-in time.");

            var totalHours = CalculateTotalHours(checkIn, checkOut);

            return new AttendanceRecord
            {
                EmployeeId = employeeId,
                Date = date,
                Status = status,
                CheckIn = checkIn,
                CheckOut = checkOut,
                TotalHours = totalHours
            };
        }

        public void RecordCheckOut(TimeOnly checkOut)
        {
            if (!CheckIn.HasValue)
                throw new DomainException("Cannot record check-out without a check-in.");

            if (checkOut <= CheckIn.Value)
                throw new DomainException("Check-out time must be after check-in time.");

            CheckOut = checkOut;
            TotalHours = CalculateTotalHours(CheckIn, CheckOut);
        }

        public void UpdateStatus(_AttendanceStatus status)
        {
            Status = status;
        }

        private static decimal? CalculateTotalHours(TimeOnly? checkIn, TimeOnly? checkOut)
        {
            if (!checkIn.HasValue || !checkOut.HasValue)
                return null;

            return (decimal)(checkOut.Value - checkIn.Value).TotalHours;
        }
    }
}
