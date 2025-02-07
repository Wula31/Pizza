namespace Pizza.Infrastucture.Exceptions;

public class UserAndAddressNotRelated : Exception
{
    public UserAndAddressNotRelated(string message) : base(message) { }
}