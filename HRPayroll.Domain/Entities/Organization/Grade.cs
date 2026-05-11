using HRPayroll.Domain.Common;

namespace HRPayroll.Domain.Entities.Organization
{
    public class Grade: AuditableEntity
    {
        public string Name { get; private set; }
        public decimal MinSalary { get; private set; }
        public decimal MaxSalary { get; private set; }

        private readonly List<JobTitle> _jobTitles = [];
        public IReadOnlyCollection<JobTitle> JobTitles => _jobTitles.AsReadOnly();

        private Grade() { }

        public static Grade Create(string name, decimal minSalary, decimal maxSalary)
        {
            if(minSalary <= 0)
                throw new DomainException("Minimum salary must be greater than zero.");

            if(maxSalary <= minSalary)
                throw new DomainException("Maximum salary must be greater than minimum salary.");

            return new Grade
            {
                Name = name.Trim(),
                MinSalary = minSalary,
                MaxSalary = maxSalary
            };
        }

        public void Update(string name, decimal minSalary, decimal maxSalary)
        {
            if (minSalary <= 0)
                throw new DomainException("Minimum salary must be greater than zero.");

            if (maxSalary <= minSalary)
                throw new DomainException("Maximum salary must be greater than minimum salary.");
            
            Name = name.Trim();
            MinSalary = minSalary;
            MaxSalary = maxSalary;
        }
    }
}
