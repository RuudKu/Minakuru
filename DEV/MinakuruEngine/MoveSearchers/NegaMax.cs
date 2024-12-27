using Minakuru.Engine.Evaluators;
using Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.MoveSearchers;
public class NegaMax(IEvaluator evaluator, IMoveGenerator moveGenerator) : IMoveSearcher
{
	private const int StartDepth = 2;
	private readonly IEvaluator _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
	private readonly IMoveGenerator _moveGenerator = moveGenerator ?? throw new ArgumentNullException(nameof(moveGenerator));

	public NegaMax() : this(new Evaluator(), new LegalMovesGenerator())
	{		
	}

	public Tuple<Move, int> Search(Board board)
	{
		var allMoves = _moveGenerator.GenerateMove(board);
		int max = int.MinValue;
		var bestMove = Move.NullMove;
		foreach (var move in allMoves)
		{
			var newBoard = MoveMaker.MakeMove(board, move);
			int moveScore = -SearchCore(newBoard, StartDepth - 1);
			if (moveScore > max)
			{
				max = moveScore;
				bestMove = move;
			}
		}
		return new Tuple<Move, int>(bestMove, max);
	}

	private int SearchCore(Board board, int depth)
	{
		if (depth == 0)
		{
			return _evaluator.Evaluate(board);
		}

		var allMoves = _moveGenerator.GenerateMove(board);
		if (!allMoves.Any())
		{
			return -1 * _evaluator.Evaluate(board);
		}
		int max = int.MinValue;
		foreach (var move in allMoves)
		{
			var newBoard = MoveMaker.MakeMove(board, move);
			int moveScore = -SearchCore(newBoard, depth - 1);
			if (moveScore > max)
			{
				max = moveScore;
			}
		}
		return max;
	}
}
