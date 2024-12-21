
namespace Engine.UnitTests;

public record ReadableMove(string From, string To, bool Captures, bool Check)
{
	public override string ToString()
	{
		return base.ToString();
	}
}
