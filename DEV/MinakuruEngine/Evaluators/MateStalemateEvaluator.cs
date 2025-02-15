﻿using Minakuru.Engine.MoveGenerators;
using Minakuru.Engine.ThreatCheckers;

namespace Minakuru.Engine.Evaluators;

public class MateStalemateEvaluator(LegalMovesGenerator legalMovesGenerator, IThreatChecker threatChecker) : IEvaluator
{
	private readonly IThreatChecker _threatChecker = threatChecker ?? throw new ArgumentNullException(nameof(threatChecker));
	private readonly LegalMovesGenerator _legalMovesGenerator = legalMovesGenerator ?? throw new ArgumentNullException(nameof(legalMovesGenerator));

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
