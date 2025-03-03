namespace Pizza.Infrastucture.Exceptions;

public class UserDeletedException : Exception
{
    public UserDeletedException(string message) : base(message) { }
}