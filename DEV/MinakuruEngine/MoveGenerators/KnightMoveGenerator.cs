using System.Numerics;

namespace Minakuru.Engine.MoveGenerators;

public class KnightMoveGenerator : IMoveGenerator
{
	public IEnumerable<Move> GenerateMove(Board board)
	{
		var color = board.ColorToMove;
		var whitePiecesAt = board.WhitePieces;
		var blackPiecesAt = board.BlackPieces;

		var knights = color == Color.White ? board.WhiteKnights : board.BlackKnights;
		var ownPiecesAt = color == Color.White ? whitePiecesAt : blackPiecesAt;
		var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;

		var workingCopy = knights;
		byte from = 0;
		while (workingCopy != 0UL)
		{
			byte trailingZeroes = (byte)BitOperations.TrailingZeroCount(workingCopy);
			from += trailingZeroes;
			workingCopy >>= trailingZeroes;
			var filter = (ulong)1 << from;
			if ((knights & filter) != 0)
			{
				int fromColumn = from % 8;
				int fromRow = from / 8;

				int toColumn;
				int toRow;

				for (int option = 0; option < 8; option++)
				{
					int deltaColumn = new int[] { -2, -2, -1, -1, +1, +1, +2, +2 }[option];
					int deltaRow = new int[] { -1, +1, -2, +2, -2, 2, -1, 1 }[option];
					toColumn = fromColumn + deltaColumn;
					toRow = fromRow + deltaRow;

					if (toColumn >= 0 && toColumn < 8 && toRow >= 0 && toRow < 8)
					{
						byte to = (byte)(8 * toRow + toColumn);
						ulong toFilter = (ulong)1 << to;

						if ((ownPiecesAt & toFilter) == 0)
						{
							bool isCapture = (opponentPiecesAt & toFilter) != 0;
							yield return new Move(from, to, isCapture);
						}
					}
				}
				workingCopy &= ~1UL;
			}
		}
	}
}
