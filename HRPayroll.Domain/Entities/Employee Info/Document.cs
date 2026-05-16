using HRPayroll.Domain.Common;
using HRPayroll.Domain.Entities.Identity;
using HRPayroll.Domain.Enums;

namespace HRPayroll.Domain.Entities.EmployeeInfo
{
    public class Document
    {
        public Guid EmployeeId { get; private set; }
        public Guid UploadedBy { get; private set; }
        public _DocumentType Type { get; private set; }
        public string FileName { get; private set; }
        public string FileUrl { get; private set; }
        public DateOnly? ExpiryDate { get; private set; }

        public Employee Employee { get; private set; }
        public User Uploader { get; private set; }

        private Document() { }

        public static Document Create(Guid employeeId, Guid uploadedBy, _DocumentType type, string fileName, string fileUrl, DateOnly? expiryDate = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
            ArgumentException.ThrowIfNullOrWhiteSpace(fileUrl);

            if (employeeId == Guid.Empty)
                throw new DomainException("EmployeeId must be a valid Guid.");

            if (uploadedBy == Guid.Empty)
                throw new DomainException("UploadedBy must be a valid Guid.");

            if (expiryDate.HasValue && expiryDate.Value <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new DomainException("Expiry date must be in the future.");

            return new Document
            {
                EmployeeId = employeeId,
                UploadedBy = uploadedBy,
                Type = type,
                FileName = fileName.Trim(),
                FileUrl = fileUrl.Trim(),
                ExpiryDate = expiryDate
            };
        }

        public void UpdateExpiry(DateOnly expiryDate)
        {
            if (expiryDate <= DateOnly.FromDateTime(DateTime.UtcNow))
                throw new DomainException("Expiry date must be in the future.");

            ExpiryDate = expiryDate;
        }

    }
}
