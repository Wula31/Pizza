﻿namespace Pizza.Infrastucture.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException(string message) : base(message) { }
}