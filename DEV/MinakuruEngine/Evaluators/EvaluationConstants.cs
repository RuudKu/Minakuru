namespace Minakuru.Engine.Evaluators;

public static class EvaluationConstants
{
	public const int Mate = 1_000_000;
	public const int StaleMate = 999_000;

	public const int KingWeightFactor = 20_000;
	public const int QueenWeightFactor = 9_000;
	public const int RookWeightFactor = 5_000;
	public const int BishopWeightFactor = 3_500;
	public const int KnightWeightFactor = 3_000;
	public const int PawnWeightFactor = 1_000;
}
