    using Pizza.Application.Common.Interfaces;

    namespace Pizza.Domain.Entities;


    public abstract class BaseEntity
    {
        public int Id { get; set; } = 1;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedOn { get; set; }
    }