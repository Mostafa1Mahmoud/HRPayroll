using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPayroll.Domain.Entities.Organization
{
    public class Branch : AuditableEntity
    {
        public Guid CompanyId { get; private set; }
        public string Name { get; private set; }
        public string? Address { get; private set; }
        public Company Company { get; private set; }

        private Branch() { }

        public static Branch Create(Guid companyId, string name, string? address = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return new Branch
            {
                CompanyId = companyId,
                Name = name.Trim(),
                Address = address?.Trim()
            };
        }

        public void Update(string name, string? address = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            Name = name.Trim();
            Address = address?.Trim();
        }
    }
}
