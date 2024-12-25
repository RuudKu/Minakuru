namespace Minakuru.Engine.MoveGenerators;

public class PseudoLegalMoveGenerator : IMoveGenerator
{
	private readonly IMoveGenerator[] generators =
		[
			new QueenMoveGenerator(),
			new RookMoveGenerator(),
			new BishopMoveGenerator(),
			new KnightMoveGenerator(),
			new PawnMoveGenerator(),
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
