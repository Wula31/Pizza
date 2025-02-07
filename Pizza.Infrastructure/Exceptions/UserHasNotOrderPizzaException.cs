namespace Pizza.Infrastucture.Exceptions;

public class UserHasNotOrderPizzaException : Exception
{
    public UserHasNotOrderPizzaException(string message) : base(message) { }
}