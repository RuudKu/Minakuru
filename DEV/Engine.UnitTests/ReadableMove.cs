namespace Minakuru.Engine.UnitTests;

public record ReadableMove(string From, string To, bool Captures = false, Piece PromotedTo = Piece.None)
{
	public Move ToMove()
	{
		byte fromColumnNo = (byte)((From[0] - 'A') % 32);
		byte fromRowNo = (byte)(From[1] - '0' - 1);
		byte fromFieldNo = (byte)(Field.TotalColumns * fromRowNo + fromColumnNo);

		byte toColumnNo = (byte)((To[0] - 'A') % 32);
		byte toRowNo = (byte)(To[1] - '0' - 1);
		byte toFieldNo = (byte)(Field.TotalColumns * toRowNo + toColumnNo);

		return new Move(fromFieldNo, toFieldNo, Captures, PromotedTo);
	}


}
