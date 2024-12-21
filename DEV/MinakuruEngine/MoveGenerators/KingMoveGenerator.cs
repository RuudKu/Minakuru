namespace Engine.MoveGenerators;

public class KingMoveGenerator : IMoveGenerator
{
	public IEnumerable<Move> GenerateMove(Board board, Color color)
	{
		var whitePiecesAt = board.WhiteKing | board.WhiteQueens | board.WhiteRooks | board.WhiteBishops | board.WhiteKnights | board.WhitePawns;
		var blackPiecesAt = board.BlackKing | board.BlackQueens | board.BlackRooks | board.BlackBishops | board.BlackKnights | board.BlackPawns;

		var king = color == Color.White ? board.WhiteKing : board.BlackKing;
		var ownPiecesAt = color == Color.White ? whitePiecesAt : blackPiecesAt;
		var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;

		for (byte from = 0; from < 64; from++)
		{
			var filter = (ulong)1 << from;
			if ((king & filter) != 0)
			{
				int fromColumn = from % 8;
				int fromRow = from / 8;

				int toColumn;
				int toRow;

				for (int direction = 0; direction < 8; direction++)
				{
					int deltaColumn = new int[] { -1, -1, -1, 0, 0, +1, +1, +1 }[direction];
					int deltaRow = new int[] { -1, 0, +1, -1, +1, -1, +0, +1 }[direction];
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

				king &= ~filter;
				if (king == 0)
				{
					break;
				}
			}
		}
	}
}
