using HRPayroll.Domain.Common;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.Payroll
{
    public class SalaryComponent: AuditableEntity
    {
        public string Name { get; private set; }
        public _ComponentType ComponentType { get; private set; }
        public _CalculationType CalculationType { get; private set; }

        private readonly List<SalaryStructureComponent> _salaryStructureComponents = new List<SalaryStructureComponent>();
        public IReadOnlyCollection<SalaryStructureComponent> SalaryStructureComponents => _salaryStructureComponents.AsReadOnly();

        private SalaryComponent() { }

        public static SalaryComponent Create(string name, _ComponentType componentType, _CalculationType calculationType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return new SalaryComponent
            {
                Name = name.Trim(),
                ComponentType = componentType,
                CalculationType = calculationType
            };
        }

        public void Update(string name, _ComponentType componentType, _CalculationType calculationType)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            Name = name.Trim();
            ComponentType = componentType;
            CalculationType = calculationType;
        }
    }
}
