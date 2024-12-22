
namespace Engine.UnitTests;

public record ReadableMove(string From, string To, bool Captures = false, Piece? PromotedTo = null)
{
}
