namespace HRPayroll.Domain.Entities
{
    public abstract class AuditableEntity
    {
        public Guid Id { get; private set; } = Guid.CreateVersion7();
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; private set; }
        public DateTime ModifiedAt { get; private set; } = DateTime.UtcNow;
        public Guid? ModifiedBy { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public void SetCreatedBy(Guid userId)
        {
            CreatedBy = userId;
        }

        public void SetModifiedBy(Guid userId)
        {
            ModifiedBy = userId;
            ModifiedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}
