namespace Minakuru.Engine.MoveGenerators.Experimental;

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

	public void GenerateMove(Board board, MoveList moveList)
	{
		foreach (var generator in generators)
		{
			generator.GenerateMove(board, moveList);
		}
	}
}
