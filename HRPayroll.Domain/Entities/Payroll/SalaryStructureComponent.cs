using HRPayroll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPayroll.Domain.Entities.Payroll
{
    public class SalaryStructureComponent
    {
        public Guid SalaryStructureId { get; private set; }
        public Guid SalaryComponentId { get; private set; }
        public decimal Value { get; private set; }
        public int DisplayOrder { get; private set; }

        public SalaryStructure SalaryStructure { get; private set; }
        public SalaryComponent SalaryComponent { get; private set; }

        private SalaryStructureComponent() { }

        internal static SalaryStructureComponent Create(Guid salaryStructureId, Guid salaryComponentId, decimal value, int displayOrder)
        {
            if (salaryStructureId == Guid.Empty)
                throw new DomainException("SalaryStructureId must be a valid Guid.");

            if (salaryComponentId == Guid.Empty)
                throw new DomainException("SalaryComponentId must be a valid Guid.");

            if (value < 0)
                throw new DomainException("Value cannot be negative.");

            if (displayOrder < 0)
                throw new DomainException("Display order cannot be negative.");

            return new SalaryStructureComponent
            {
                SalaryStructureId = salaryStructureId,
                SalaryComponentId = salaryComponentId,
                Value = value,
                DisplayOrder = displayOrder
            };
        }

        public void Update(decimal value)
        {
            if (value < 0)
                throw new DomainException("Value cannot be negative.");

             Value = value;
        }
    }
}
