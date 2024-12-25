using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.MoveGenerators;

public class LegalMovesGenerator(PseudoLegalMoveGenerator pseudoLegalMovesGenerator, IThreatChecker threatChecker) : IMoveGenerator
{
	private readonly PseudoLegalMoveGenerator _pseudoLegalMovesGenerator = pseudoLegalMovesGenerator ?? throw new ArgumentNullException(nameof(pseudoLegalMovesGenerator));
	private readonly IThreatChecker _threatChecker = threatChecker ?? throw new ArgumentNullException(nameof(threatChecker));

	public LegalMovesGenerator() : this(new PseudoLegalMoveGenerator(), new ThreatChecker())
	{
	}

	public IEnumerable<Move> GenerateMove(Board board, Color color)
	{
		var enumerator = _pseudoLegalMovesGenerator.GenerateMove(board, color).GetEnumerator();
		while (enumerator.MoveNext())
		{
			var newBoard = MoveMaker.MakeMove(board, enumerator.Current);
			Color colorToMove = board.ColorToMove;
			byte kingFieldNo = newBoard.KingAt(colorToMove);

			bool check = _threatChecker.IsUnderAttack(newBoard, kingFieldNo, colorToMove.OtherColor());
			if (!check)
			{
				yield return enumerator.Current;
			}
		}
	}
}
