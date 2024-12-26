namespace Minakuru.Engine;

public static class PieceExtensons
{
	public static ColoredPiece ToColoredPiece(this Piece piece, Color color)
	{
		return color switch
		{
			Color.White => piece switch
			{
				Piece.Pawn => ColoredPiece.WhitePawn,
				Piece.Knight => ColoredPiece.WhiteKnight,
				Piece.Bishop => ColoredPiece.WhiteBishop,
				Piece.Rook => ColoredPiece.WhiteRook,
				Piece.Queen => ColoredPiece.WhiteQueen,
				Piece.King => ColoredPiece.WhiteKing,
				_ => throw new NotImplementedException()
			},
			Color.Black => piece switch
			{
				Piece.Pawn => ColoredPiece.BlackPawn,
				Piece.Knight => ColoredPiece.BlackKnight,
				Piece.Bishop => ColoredPiece.BlackBishop,
				Piece.Rook => ColoredPiece.BlackRook,
				Piece.Queen => ColoredPiece.BlackQueen,
				Piece.King => ColoredPiece.BlackKing,
				_ => throw new NotImplementedException()
			},
			_ => throw new NotSupportedException(),
		};
	}
}
