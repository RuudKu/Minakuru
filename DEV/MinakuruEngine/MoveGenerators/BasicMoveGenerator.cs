namespace Engine.MoveGenerators;

public class BasicMoveGenerator : IMoveGenerator
{
	private readonly IMoveGenerator[] generators =
		[
			new QueenMoveGenerator(),
			new RookMoveGenerator(),
			new BishopMoveGenerator(),
			new KnightMoveGenerator(),
			new KingMoveGenerator()
		];

	public IEnumerable<Move> GenerateMove(Board board, Color color)
	{
		foreach (var generator in generators)
		{
			var generatorEnumerator = generator.GenerateMove(board, color).GetEnumerator();
			while (generatorEnumerator.MoveNext())
			{
				yield return generatorEnumerator.Current;
			}
		}
	}
}
