namespace Pizza.Infrastucture.Exceptions;

public class AddressDeletedException : Exception
{
    public AddressDeletedException(string message) : base(message) { }
}