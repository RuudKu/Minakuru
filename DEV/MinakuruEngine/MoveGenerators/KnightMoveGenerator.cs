namespace Engine.MoveGenerators
{
	public class KnightMoveGenerator : IMoveGenerator
	{
		public IEnumerable<Move> GenerateMove(Board board, Color color)
		{
			var whitePiecesAt = board.WhiteKing | board.WhiteQueens | board.WhiteRooks | board.WhiteBishops | board.WhiteKnights | board.WhitePawns;
			var blackPiecesAt = board.BlackKing | board.BlackQueens | board.BlackRooks | board.BlackBishops | board.BlackKnights | board.BlackPawns;

			var knights = color == Color.White ? board.WhiteKnights : board.BlackKnights;
			var ownPiecesAt = color == Color.White ? whitePiecesAt : blackPiecesAt;
			var opponentPiecesAt = color == Color.White ? blackPiecesAt : whitePiecesAt;

			for (byte from = 0; from < 64; from++)
			{
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

					knights &= ~filter;
					if (knights == 0)
					{
						break;
					}
				}
			}
		}
	}
}
