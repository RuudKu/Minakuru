using BenchmarkDotNet.Attributes;
using Baseline = Minakuru.Engine.ThreatCheckers;
using Experimental = Minakuru.Engine.ThreatCheckers.Experimental;

namespace Minakuru.Engine.Benchmarks.ThreatCheckers;

[HtmlExporter]
public class KnightThreatCheckerBenchmark
{
	private readonly Baseline.IThreatChecker threatCheckerBaseline = new Baseline.KnightThreatChecker();
	private readonly Baseline.IThreatChecker threatCheckerExperimental = new Experimental.KnightThreatChecker();

	private const string Fen1 = "1n6/8/3n3n/1n2Kn2/8/4n3/8/n3n3 w - - 0 1";
	private const string Fen2 = "r1bqkb1r/pppnpppp/5n2/8/8/2N2N2/PPPPPPPP/R1BQKB1R w KQkq - 0 1";

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
			baselineResult = threatCheckerBaseline.IsUnderAttack(Board, Field.E5FieldNo, Color.Black);
	}

	[Benchmark]
	public void ExperimentalMoves()
	{
		experimentalResult = threatCheckerExperimental.IsUnderAttack(Board, Field.E5FieldNo, Color.Black);
	}
}
