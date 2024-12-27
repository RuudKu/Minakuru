using Minakuru.Engine.MoveGenerators;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.Evaluators;

public class MateStalemateEvaluator : IEvaluator
{
	private readonly ThreatChecker _threatChecker;
	private readonly LegalMovesGenerator _legalMovesGenerator;

	public MateStalemateEvaluator(LegalMovesGenerator legalMovesGenerator, ThreatChecker threatChecker)
	{
		_legalMovesGenerator = legalMovesGenerator ?? throw new ArgumentNullException(nameof(legalMovesGenerator));
		_threatChecker = threatChecker ?? throw new ArgumentNullException(nameof(threatChecker));
	}

	public MateStalemateEvaluator() : this(new LegalMovesGenerator(), new ThreatChecker())
	{
	}

	public int Evaluate(Board board)
	{
		var legalMoves = _legalMovesGenerator.GenerateMove(board);
		if (legalMoves.Any())
		{
			return 0;
		}
		var kingAt = board.KingAt(board.ColorToMove);
		bool isCheck = _threatChecker.IsUnderAttack(board, kingAt, board.ColorToMove.OtherColor());
		if (!isCheck)
		{
			return EvaluationConstants.StaleMate;
		}
		return board.ColorToMove == Color.White ? -1 * EvaluationConstants.Mate : EvaluationConstants.Mate;
	}
}
