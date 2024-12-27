namespace Minakuru.Engine.Evaluators;

public class SimpleMaterialEvaluator : IEvaluator
{
	private const int QueenWeightFactor = 9000;
	private const int RookWeightFactor = 5000;
	private const int BishopWeightFactor = 3500;
	private const int KnightWeightFactor = 3000;
	private const int PawnWeightFactor = 1000;

	public int Evaluate(Board board)
	{
		int result = 0;
		result += (board.WhiteQueensCount - board.BlackQueensCount) * QueenWeightFactor;
		result += (board.WhiteRooksCount - board.BlackRooksCount) * RookWeightFactor;
		result += (board.WhiteBishopsCount - board.BlackBishopsCount) * BishopWeightFactor;
		result += (board.WhiteKnightsCount - board.BlackKnightsCount) * KnightWeightFactor;
		result += (board.WhitePawnsCount - board.BlackPawnsCount) * PawnWeightFactor;
		return result;
	}
}
