using HRPayroll.Domain.Common;


namespace HRPayroll.Domain.Entities.Payroll
{
    public class SalaryStructure: AuditableEntity
    {
        public string Name { get; private set; }

        public List<SalaryStructureComponent> _salaryStructureComponents = new List<SalaryStructureComponent>();
        public IReadOnlyCollection<SalaryStructureComponent> SalaryStructureComponents => _salaryStructureComponents.AsReadOnly();

        private SalaryStructure() { }

        public static SalaryStructure Create(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            return new SalaryStructure
            {
                Name = name.Trim()
            };
        }
    }
}
