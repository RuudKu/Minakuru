namespace Minakuru.Engine.MoveGenerators;

public class KingMoveGenerator : IMoveGenerator
{
	public IEnumerable<Move> GenerateMove(Board board, Color color)
	{
		var whitePiecesAt = board.WhitePieces;
		var blackPiecesAt = board.BlackPieces;

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

		switch (color)
		{
			case Color.White:
				if (board.WhiteCanCastleShort && board.IsEmpty(Field.F1FieldNo) && board.IsEmpty(Field.G1FieldNo))
				{
					yield return new Move(Field.E1FieldNo, Field.G1FieldNo);
				}
				if (board.WhiteCanCastleLong && board.IsEmpty(Field.D1FieldNo) && board.IsEmpty(Field.C1FieldNo) && board.IsEmpty(Field.B1FieldNo))
				{
					yield return new Move(Field.E1FieldNo, Field.C1FieldNo);
				}
				break;
			case Color.Black:
				if (board.BlackCanCastleShort && board.IsEmpty(Field.F8FieldNo) && board.IsEmpty(Field.G8FieldNo))
				{
					yield return new Move(Field.E8FieldNo, Field.G8FieldNo);
				}
				if (board.BlackCanCastleLong && board.IsEmpty(Field.D8FieldNo) && board.IsEmpty(Field.C8FieldNo) && board.IsEmpty(Field.B8FieldNo))
				{
					yield return new Move(Field.E8FieldNo, Field.C8FieldNo);
				}
				break;
			default:
				throw new NotSupportedException();
		}
	}
}
