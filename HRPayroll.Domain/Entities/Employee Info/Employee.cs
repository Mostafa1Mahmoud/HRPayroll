using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.Identity;
using HRPayroll.Domain.Entities.Organization;
using HRPayroll.Domain.Entities.Payroll;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.EmployeeInfo
{
    public class Employee: AuditableEntity
    {
        public Guid UserId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public Guid? BranchId { get; private set; }
        public Guid? JobTitleId { get; private set; }
        public Guid? SalaryStructureId { get; private set; }
        public string EmployeeCode { get; private set; }
        public DateOnly HireDate { get; private set; }
        public _EmployeeStatus Status { get; private set; }
        public User User { get; private set; }
        public Department? Department { get; private set; }
        public Branch? Branch { get; private set; }
        public JobTitle? JobTitle { get; private set; }
        public SalaryStructure? SalaryStructure { get; private set; }
        public EmployeePersonalInfo? PersonalInfo { get; private set; }

        private readonly List<EmployeeEmergencyContact> _emergencyContacts = new List<EmployeeEmergencyContact>();
        public IReadOnlyCollection<EmployeeEmergencyContact> EmergencyContacts => _emergencyContacts.AsReadOnly();

        private readonly List<EmploymentHistory> _employmentHistories = new List<EmploymentHistory>();
        public IReadOnlyCollection<EmploymentHistory> EmploymentHistories => _employmentHistories.AsReadOnly();

        private readonly List<Document> _documents = [];
        public IReadOnlyCollection<Document> Documents => _documents.AsReadOnly();

        private Employee() { }

        public static Employee Create(Guid userId, string employeeCode, DateOnly hireDate)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(employeeCode);

            if (userId == Guid.Empty)
                throw new DomainException("UserId must be a valid Guid.");

            if (hireDate > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new DomainException("Hire date cannot be in the future.");

            var employee = new Employee
            {
                UserId = userId,
                EmployeeCode = employeeCode.Trim().ToUpper(),
                HireDate = hireDate,
                Status = _EmployeeStatus.Active
            };

            employee._employmentHistories.Add(EmploymentHistory.Create(
                employee.Id,
                _ChangeType.Hired,
                null,
                employeeCode,
                hireDate
            ));

            return employee;
        }

        public void AssignDepartment(Guid departmentId)
        {
            if (departmentId == Guid.Empty)
                throw new DomainException("DepartmentId must be a valid Guid.");

            var old = DepartmentId?.ToString();
            DepartmentId = departmentId;

            _employmentHistories.Add(EmploymentHistory.Create(
                Id,
                _ChangeType.Transferred,
                old,
                departmentId.ToString(),
                DateOnly.FromDateTime(DateTime.UtcNow)
            ));
        }

        public void AssignBranch(Guid branchId)
        {
            if (branchId == Guid.Empty)
                throw new DomainException("BranchId must be a valid Guid.");

            var old = BranchId?.ToString();
            this.BranchId = BranchId;
            _employmentHistories.Add(EmploymentHistory.Create(
                Id,
                _ChangeType.Transferred,
                old,
                BranchId.ToString(),
                DateOnly.FromDateTime(DateTime.UtcNow)
            ));
        }

        public void AssignJobTitle(Guid jobTitleId)
        {
            if (jobTitleId == Guid.Empty)
                throw new DomainException("JobTitleId must be a valid Guid.");

            var old = JobTitleId?.ToString();
            JobTitleId = jobTitleId;

            _employmentHistories.Add(EmploymentHistory.Create(
                Id,
                _ChangeType.Promoted,
                old,
                jobTitleId.ToString(),
                DateOnly.FromDateTime(DateTime.UtcNow)
            ));
        }

        public void AssignSalaryStructure(Guid salaryStructureId)
        {
            if (salaryStructureId == Guid.Empty)
                throw new DomainException("SalaryStructureId must be a valid Guid.");

            var old = SalaryStructureId?.ToString();
            SalaryStructureId = salaryStructureId;

            _employmentHistories.Add(EmploymentHistory.Create(
                Id,
                _ChangeType.SalaryChanged,
                old,
                salaryStructureId.ToString(),
                DateOnly.FromDateTime(DateTime.UtcNow)
            ));
        }

        public void Terminate()
        {
            if (Status == _EmployeeStatus.Terminated)
                throw new DomainException("Employee is already terminated.");

            Status = _EmployeeStatus.Terminated;

            _employmentHistories.Add(EmploymentHistory.Create(
                Id,
                _ChangeType.Terminated,
                Status.ToString(),
                _EmployeeStatus.Terminated.ToString(),
                DateOnly.FromDateTime(DateTime.UtcNow)
            ));
        }

        public void SetOnLeave()
        {
            if (Status != _EmployeeStatus.Active)
                throw new DomainException("Only active employees can be set to on leave.");

            Status = _EmployeeStatus.OnLeave;
        }

        public void SetActive()
        {
            Status = _EmployeeStatus.Active;
        }

        public void AddEmergencyContact(EmployeeEmergencyContact contact)
        {
            ArgumentNullException.ThrowIfNull(contact);

            _emergencyContacts.Add(contact);
        }

        public void AddDocument(Document document)
        {
            ArgumentNullException.ThrowIfNull(document);

            _documents.Add(document);
        }
    }
}
