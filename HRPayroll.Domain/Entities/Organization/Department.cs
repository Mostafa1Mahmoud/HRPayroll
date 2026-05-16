using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.Identity;
using HRPayroll.Domain.Entities.EmployeeInfo;

namespace HRPayroll.Domain.Entities.Organization
{
    public class Department : AuditableEntity
    {
        public string Name { get; private set; }
        public Guid CompanyId { get; private set; }
        public Guid? ManagerId { get; private set; }

        public Company Company { get; private set; }
        public Employee? Manager { get; private set; }

        private Department() { }

        public static Department Create(Guid companyId, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return new Department
            {
                CompanyId = companyId,
                Name = name.Trim(),
            };
        }

        public void Update(string name, Guid? managerId = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Name = name.Trim();
            ManagerId = managerId;
        }
        public void AssignManager(Guid managerId)
        {
            if (managerId == Guid.Empty)
                throw new DomainException("ManagerId must be a valid Guid.");

            ManagerId = managerId;
        }

        public void RemoveManager()
        {
            ManagerId = null;
        }

    }
}
