namespace Engine;

public static class BoardExtensions
{
	public static ColoredPiece PieceAt(this Board board, byte columnNo, byte rowNo)
	{
		ulong fieldFilter = ((ulong)1) << (rowNo * 8 + columnNo);
		if ((board.WhiteKing & fieldFilter) != 0)
		{
			return ColoredPiece.WhiteKing;
		}
		else if ((board.WhiteQueens & fieldFilter) != 0)
		{
			return ColoredPiece.WhiteQueen;
		}
		else if ((board.WhiteRooks & fieldFilter) != 0)
		{
			return ColoredPiece.WhiteRook;
		}
		else if ((board.WhiteBishops & fieldFilter) != 0)
		{
			return ColoredPiece.WhiteBishop;
		}
		else if ((board.WhiteKnights & fieldFilter) != 0)
		{
			return ColoredPiece.WhiteKnight;
		}
		else if ((board.WhitePawns & fieldFilter) != 0)
		{
			return ColoredPiece.WhitePawn;
		}
		if ((board.BlackKing & fieldFilter) != 0)
		{
			return ColoredPiece.BlackKing;
		}
		else if ((board.BlackQueens & fieldFilter) != 0)
		{
			return ColoredPiece.BlackQueen;
		}
		else if ((board.BlackRooks & fieldFilter) != 0)
		{
			return ColoredPiece.BlackRook;
		}
		else if ((board.BlackBishops & fieldFilter) != 0)
		{
			return ColoredPiece.BlackBishop;
		}
		else if ((board.BlackKnights & fieldFilter) != 0)
		{
			return ColoredPiece.BlackKnight;
		}
		else if ((board.BlackPawns & fieldFilter) != 0)
		{
			return ColoredPiece.BlackPawn;
		}
		else return ColoredPiece.Empty;
	}
}
