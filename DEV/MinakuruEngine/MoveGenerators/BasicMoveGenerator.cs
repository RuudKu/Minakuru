
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
}
