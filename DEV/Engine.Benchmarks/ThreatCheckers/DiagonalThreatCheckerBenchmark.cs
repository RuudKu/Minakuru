using BenchmarkDotNet.Attributes;
using Baseline = Minakuru.Engine.ThreatCheckers;
using Experimental = Minakuru.Engine.ThreatCheckers.Experimental;

namespace Minakuru.Engine.Benchmarks.ThreatCheckers;

[HtmlExporter]
public class DiagonalThreatCheckerBenchmark
{
	private readonly Baseline.IThreatChecker threatCheckerBaseline = new Baseline.DiagonalThreatChecker();
	private readonly Baseline.IThreatChecker threatCheckerExperimental = new Experimental.DiagonalThreatChecker();

	private const string Fen1 = "6k1/8/5r2/8/3K4/8/1B6/8 w - - 0 1";
	private const string Fen2 = "6K1/8/5R2/8/3k4/8/1b6/8 b - - 0 1";

	bool baselineResult = false;
	bool experimentalResult = false;

	string fenCode;

	[Params("Start w", "Start b")]
	public string FenCode
	{
		get { return fenCode; }
		set
		{
			fenCode = value;
			var fen = "";
			switch (value)
			{
				case "Start w":
					fen = Fen1;
					break;
				case "Start b":
					fen = Fen2;
					break;
			}

			Board = fen.ToBoard();
		}
	}

	public Board Board { get; set; }

	[Benchmark(Baseline = true)]
	public void BaselineMoves()
	{
		baselineResult = threatCheckerBaseline.IsUnderAttack(Board, Field.D4FieldNo, Color.Black);
	}

	[Benchmark]
	public void ExperimentalMoves()
	{
		experimentalResult = threatCheckerExperimental.IsUnderAttack(Board, Field.D4FieldNo, Color.Black);
	}
}
