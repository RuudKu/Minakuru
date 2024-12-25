namespace Minakuru.Engine.MoveGenerators;

public class BishopMoveGenerator : IMoveGenerator
{
	public IEnumerable<Move> GenerateMove(Board board, Color color)
	{
		var whitePiecesAt = board.WhitePieces;
		var blackPiecesAt = board.BlackPieces;

		var bishops = color == Color.White ? board.WhiteBishops : board.BlackBishops;
		var ownPiecesAt = color == Color.White ? whitePiecesAt : blackPiecesAt;
		var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;

		for (byte from = 0; from < 64; from++)
		{
			var filter = (ulong)1 << from;
			if ((bishops & filter) != 0)
			{
				int fromColumn = from % 8;
				int fromRow = from / 8;

				int toColumn;
				int toRow;

				for (int direction = 0; direction < 4; direction++)
				{
					int deltaColumn = new int[] { -1, -1, +1, +1 }[direction];
					int deltaRow = new int[] { -1, +1, -1, +1 }[direction];
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
							yield return new Move(from, to, isCapture);
						}
						ownPiece = (ownPiecesAt & toFilter) != 0;

						toColumn += deltaColumn;
						toRow += deltaRow;
					}
				}

				bishops &= ~filter;
				if (bishops == 0)
				{
					break;
				}
			}
		}
	}
}
