namespace Pizza.Infrastucture.Exceptions;

public class PizzaNotFoundException : Exception
{
    public PizzaNotFoundException(string message) : base(message) { }
}