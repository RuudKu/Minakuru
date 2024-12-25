namespace Minakuru.Engine;

public class EngineException : Exception
{
	public EngineException() : base()
	{
	}
	public EngineException(string? message) : base(message)
	{
	}
	public EngineException(string? message, Exception innerException) : base(message, innerException)
	{
	}
}
