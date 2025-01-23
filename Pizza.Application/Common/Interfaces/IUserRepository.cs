namespace Pizza.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<bool> UserExistsAsync(int userId);
    
    Task<bool> UserMarkedDeletedAsync(int useId);
}