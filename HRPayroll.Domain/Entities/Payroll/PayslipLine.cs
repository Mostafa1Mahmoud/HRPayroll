using HRPayroll.Domain.Common;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.Payroll
{
    public class PayslipLine : AuditableEntity
    {
        public Guid PayslipId { get; private set; }
        public string ComponentName { get; private set; }
        public _ComponentType ComponentType { get; private set; }
        public decimal Amount { get; private set; }
        public int DisplayOrder { get; private set; }

        public Payslip Payslip { get; private set; }

        private PayslipLine() { }

        internal static PayslipLine Create(Guid payslipId, string componentName, _ComponentType type, decimal amount, int displayOrder)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(componentName);
            if (payslipId == Guid.Empty)
                throw new DomainException("PayslipId must be a valid Guid.");

            if (amount < 0)
                throw new DomainException("Amount cannot be negative.");

            if (displayOrder < 0)
                throw new DomainException("Display order cannot be negative.");

            return new PayslipLine
            {
                PayslipId = payslipId,
                ComponentName = componentName.Trim(),
                ComponentType = type,
                Amount = amount,
                DisplayOrder = displayOrder
            };
        }

        public void Update(string componentName, _ComponentType type, decimal amount, int displayOrder)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(componentName);
            if (amount < 0)
                throw new DomainException("Amount cannot be negative.");

            if (displayOrder < 0)
                throw new DomainException("Display order cannot be negative.");

            ComponentName = componentName.Trim();
            ComponentType = type;
            Amount = amount;
            DisplayOrder = displayOrder;
        }
    }
}
