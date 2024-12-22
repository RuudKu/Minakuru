namespace Engine;

public record Move(byte From, byte To, bool Capture = false, Piece? PromotedTo = null)
{
	
}
