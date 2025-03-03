namespace Pizza.Infrastucture.Exceptions;

public class PizzaDeletedException : Exception
{
    public PizzaDeletedException(string message) : base(message) { }
}