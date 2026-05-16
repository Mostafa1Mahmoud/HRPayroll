using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.EmployeeInfo;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.Payroll
{
    public class Payslip: AuditableEntity
    {
        public Guid EmployeeId { get; private set; }
        public Guid PayrollRunId { get; private set; }
        public decimal BasicSalary { get; private set; }
        public decimal TotoalAllowance => CalculateTotalAllowance();
        public decimal TotoalDeductions => CalculateTotalDeductions();
        public decimal NetSalary { get; private set; }
        public decimal GrossSalary { get; private set; }

        public Employee Employee { get; private set; }
        public PayrollRun PayrollRun { get; private set; }

        private readonly List<PayslipLine> _payslipLines = new List<PayslipLine>();
        public IReadOnlyCollection<PayslipLine> PayslipLines => _payslipLines.AsReadOnly();

        private Payslip() { }

        public static Payslip Create(Guid employeeId, Guid payrollRunId, decimal basicSalary, decimal totalAllowance, decimal totalDeductions)
        {
            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            if (payrollRunId == Guid.Empty)
                throw new DomainException("PayrollRunId must be a valid Guid.");

            if (basicSalary < 0)
                throw new DomainException("Basic salary cannot be negative.");

            if (totalAllowance < 0)
                throw new DomainException("Total allowance cannot be negative.");

            if (totalDeductions < 0)
                throw new DomainException("Total deductions cannot be negative.");

            var grossSalary = basicSalary + totalAllowance;
            var netSalary = grossSalary - totalDeductions;

            return new Payslip
            {
                EmployeeId = employeeId,
                PayrollRunId = payrollRunId,
                BasicSalary = basicSalary,
                GrossSalary = grossSalary,
                NetSalary = netSalary
            };
        }

        public void RecalculateTotals(decimal basicSalary)
        {
            if (basicSalary < 0)
                throw new DomainException("Basic salary cannot be negative.");

            BasicSalary = basicSalary;
            GrossSalary = basicSalary + TotoalAllowance;
            NetSalary = GrossSalary - TotoalDeductions;
            if (NetSalary < 0)
                throw new DomainException("Net salary cannot be negative. Check deduction values.");
        }

        private decimal CalculateTotalAllowance() => _payslipLines.Where(l => l.ComponentType == _ComponentType.Allowance).Sum(l => l.Amount);

        private decimal CalculateTotalDeductions() => _payslipLines.Where(l => l.ComponentType == _ComponentType.Deduction).Sum(l => l.Amount);

        public void AddPayslipLine(string description, _ComponentType lineType, decimal amount, int diplayOrder)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(description);
            if (amount < 0)
                throw new DomainException("Amount cannot be negative.");

            var payslipLine = PayslipLine.Create(Id, description, lineType, amount, diplayOrder);
            _payslipLines.Add(payslipLine);
        }
    }
}
