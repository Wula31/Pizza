namespace Pizza.Infrastucture.Exceptions;

public class DeleteForbidenExeption : Exception
{
    public DeleteForbidenExeption(string message) : base(message) { }    
}