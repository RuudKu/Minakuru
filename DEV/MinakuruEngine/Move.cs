namespace Minakuru.Engine;

public record Move(byte From, byte To, bool Capture = false, Piece PromotedTo = Piece.None)
{
	public static readonly Move NullMove = new(255, 255);
}
