using HRPayroll.Domain.Common;


namespace HRPayroll.Domain.Entities.Payroll
{
    public class SalaryStructure: AuditableEntity
    {
        public string Name { get; private set; }

        private readonly List<SalaryStructureComponent> _salaryStructureComponents = new List<SalaryStructureComponent>();
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

        public void Update(string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            Name = name.Trim();
        }

        public void AddComponent(SalaryComponent component, decimal value, int displayOrder)
        {
            ArgumentNullException.ThrowIfNull(component);

            if (_salaryStructureComponents.Any(c => c.SalaryComponentId == component.Id))
                throw new DomainException($"Component '{component.Name}' is already part of this structure.");

            if (value < 0)
                throw new DomainException("Component value cannot be negative.");

            _salaryStructureComponents.Add(SalaryStructureComponent.Create(Id, component.Id, value, displayOrder));
        }

        public void RemoveComponent(Guid salaryComponentId)
        {
            var component = _salaryStructureComponents.FirstOrDefault(c => c.SalaryComponentId == salaryComponentId);

            if (component is null)
                throw new DomainException("Component not found in this salary structure.");

            _salaryStructureComponents.Remove(component);
        }
    }
}
