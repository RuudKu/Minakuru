using BenchmarkDotNet.Attributes;
using Baseline = Minakuru.Engine.ThreatCheckers;
using Experimental = Minakuru.Engine.ThreatCheckers.Experimental;

namespace Minakuru.Engine.Benchmarks.ThreatCheckers;

[HtmlExporter]
public class StraightLineThreatCheckerBenchmark
{
	private readonly Baseline.IThreatChecker threatCheckerBaseline = new Baseline.StraightLineThreatChecker();
	private readonly Baseline.IThreatChecker threatCheckerExperimental = new Experimental.StraightLineThreatChecker();

	private const string Fen = "6K1/8/8/8/1rk5/8/8/8 w - - 0 1";

	bool baselineResult = false;
	bool experimentalResult = false;

	[Params(Field.C6FieldNo, Field.E6FieldNo, Field.H4FieldNo)]
	public byte PieceAt { get; set; }

	[IterationSetup]
	public void InterationSetup()
	{
		Board = Fen.ToBoard();
		Board.SetColoredPieceAt(PieceAt, ColoredPiece.WhiteRook);
	}

	public Board Board { get; set; }

	// [Benchmark(Baseline = true)]
	public void BaselineMoves()
	{
		baselineResult = threatCheckerBaseline.IsUnderAttack(Board, Field.D4FieldNo, Color.Black);
	}

	// [Benchmark]
	public void ExperimentalMoves()
	{
		experimentalResult = threatCheckerExperimental.IsUnderAttack(Board, Field.D4FieldNo, Color.Black);
	}
}
