
using System.Data;

namespace Engine.MoveGenerators
{
	public interface IMoveGenerator
	{
		public IEnumerable<Move> GenerateMove(Board board, Color color);
	}

	public class BasicMoveGenerator : IMoveGenerator
	{
		private readonly IMoveGenerator knightGenerator = new KnightMoveGenerator();

		public IEnumerable<Move> GenerateMove(Board board, Color color)
		{
			var knightEnumerator = knightGenerator.GenerateMove(board, color).GetEnumerator();
			while (knightEnumerator.MoveNext())
			{
				yield return knightEnumerator.Current;
			}
		}
	}

	public class KnightMoveGenerator : IMoveGenerator
	{
		public IEnumerable<Move> GenerateMove(Board board, Color color)
		{
			var knights = color == Color.White ? board.WhiteKnights : board.BlackKnights;
			for (byte from = 0; from < 64 && knights != 0; from++)
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
							yield return new Move(from, to, false);
						}
					}

					knights &= ~filter;
				}
			}
		}
	}
}
