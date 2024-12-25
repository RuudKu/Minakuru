namespace Minakuru.Engine;

public class IllegalMoveException : EngineException
{
	public IllegalMoveException() : base()
	{
	}
	public IllegalMoveException(string? message) : base(message)
	{
	}
	public IllegalMoveException(string? message, Exception innerException) : base(message, innerException)
	{
	}
}
