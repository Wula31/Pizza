namespace Pizza.Application.Common.Interfaces;

public interface IEntity
{
   int Id { get; set; }
   bool IsDeleted { get; set; }
   DateTime CreatedOn { get; set; }
   DateTime? ModifiedOn { get; set; }
}