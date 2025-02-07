namespace Pizza.Infrastucture.Exceptions;

public class FieldNotFoundException : Exception
{
    public FieldNotFoundException(string message) : base(message) { }
}