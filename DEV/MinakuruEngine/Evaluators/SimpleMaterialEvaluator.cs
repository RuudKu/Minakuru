namespace Minakuru.Engine.Evaluators;

public class SimpleMaterialEvaluator : AbstractEvaluator
{
	public override int Evaluate(Board board)
	{
		int result = 0;
		result += (board.WhiteQueensCount - board.BlackQueensCount) * EvaluationConstants.QueenWeightFactor;
		result += (board.WhiteRooksCount - board.BlackRooksCount) * EvaluationConstants.RookWeightFactor;
		result += (board.WhiteBishopsCount - board.BlackBishopsCount) * EvaluationConstants.BishopWeightFactor;
		result += (board.WhiteKnightsCount - board.BlackKnightsCount) * EvaluationConstants.KnightWeightFactor;
		result += (board.WhitePawnsCount - board.BlackPawnsCount) * EvaluationConstants.PawnWeightFactor;
		return result * Factor(board);
	}
}

public abstract class AbstractEvaluator : IEvaluator
{
	public abstract int Evaluate(Board board);

	protected static int Factor(Board board)
	{
		return board.ColorToMove == Color.White ? 1 : -1;
	}
}
