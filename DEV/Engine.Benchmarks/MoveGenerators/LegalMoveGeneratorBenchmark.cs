using BenchmarkDotNet.Attributes;
using Minakuru.Engine.MoveGenerators;
using Experiment = Minakuru.Engine.MoveGenerators.Experimental;

namespace Minakuru.Engine.Benchmarks.MoveGenerators;

[HtmlExporter]
public class LegalMoveGeneratorBenchmark
{
	private readonly LegalMovesGenerator legalMovesGeneratorBaseline = MoveGeneratorFactory.Create();
	private readonly Experiment.LegalMovesGenerator legalMovesGeneratorExperimental = Experiment.MoveGeneratorFactory.Create();

	private const string Fen1 = "r1bqk2r/4bp1p/p1np4/1p1Npp2/4P3/N1P5/PP3PPP/R2QKB1R w KQkq - 0 12";
	private const string Fen2 = "r1bqk2r/4bp1p/p1np1p2/1p1Np3/4P3/N1P5/PP3PPP/R2QKB1R b KQkq - 0 11";

	MoveList baselineResult = [];
	MoveList experimentalResult = [];

	[GlobalSetup]
	public void GlobalSetup()
	{
		var fen = string.Empty;
		switch (FenCode)
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

	[Params("Start w", "Start b")]
	public string FenCode
	{
		get; set;
	}

	public Board Board { get; set; }

	[Benchmark(Baseline = true)]
	public void BaselineMoves()
	{
		baselineResult = legalMovesGeneratorBaseline.GenerateMove(Board);
	}

	[Benchmark]
	public void ExperimentalMoves()
	{
		experimentalResult = legalMovesGeneratorExperimental.GenerateMove(Board);
	}
}
