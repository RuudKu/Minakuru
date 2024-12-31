using System.Numerics;

namespace Minakuru.Engine.MoveGenerators;

public class RookMoveGenerator : IMoveGenerator
{
	public MoveList GenerateMove(Board board)
	{
		MoveList moveList = [];

		var color = board.ColorToMove;
		var whitePiecesAt = board.WhitePieces;
		var blackPiecesAt = board.BlackPieces;

		var rooks = color == Color.White ? board.WhiteRooks : board.BlackRooks;
		var workCopy = rooks;
		var ownPiecesAt = color == Color.White ? whitePiecesAt : blackPiecesAt;
		var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;

		byte from = 0;
		while (workCopy != 0L)
		{
			int trailingZeroes = BitOperations.TrailingZeroCount(workCopy);
			from += (byte)trailingZeroes;
			workCopy >>= trailingZeroes;
			var filter = (ulong)1 << from;
			if ((rooks & filter) != 0)
			{
				int fromColumn = from % 8;
				int fromRow = from / 8;

				int toColumn;
				int toRow;

				for (int direction = 0; direction < 4; direction++)
				{
					int deltaColumn = new int[] { -1, +1, +0, +0 }[direction];
					int deltaRow = new int[] { +0, 0, -1, +1 }[direction];
					toColumn = fromColumn + deltaColumn;
					toRow = fromRow + deltaRow;

					bool ownPiece = false;
					bool isCapture = false;
					while (toColumn >= 0 && toColumn < 8 && toRow >= 0 && toRow < 8 && !ownPiece && !isCapture)
					{
						byte to = (byte)(8 * toRow + toColumn);
						ulong toFilter = (ulong)1 << to;

						if ((ownPiecesAt & toFilter) == 0)
						{
							isCapture = (opponentPiecesAt & toFilter) != 0;
							moveList.Add(new Move(from, to, isCapture));
						}
						ownPiece = (ownPiecesAt & toFilter) != 0;

						toColumn += deltaColumn;
						toRow += deltaRow;
					}
				}

				workCopy &= ~1UL;
			}
		}
		return moveList;
	}
}
