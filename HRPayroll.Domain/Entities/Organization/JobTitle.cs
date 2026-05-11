using HRPayroll.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPayroll.Domain.Entities.Organization
{
    public class JobTitle: AuditableEntity
    {
        public Guid GradeId { get; private set; }
        public string Name { get; private set; }
        public Grade Grade { get; private set; }

        private JobTitle() { }

        public static JobTitle Create(Guid gradeId, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            return new JobTitle()
            {
                GradeId = gradeId,
                Name = name.Trim()
            };
        }

        public void Update(Guid gradeId, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            GradeId = gradeId;
            Name = name.Trim();
        }
    }
}
