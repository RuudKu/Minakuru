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

	public MoveList GenerateMove(Board board)
	{
		MoveList moveList = [];
		foreach (var generator in generators)
		{
			var extraMoveList = generator.GenerateMove(board);
			moveList.Merge(extraMoveList);
		}
		return moveList;
	}
}
