using BenchmarkDotNet.Attributes;
using Baseline = Minakuru.Engine.MoveGenerators;
using Experimental = Minakuru.Engine.MoveGenerators;

namespace Minakuru.Engine.Benchmarks.MoveGenerators;

[HtmlExporter]
public class KnightMoveGeneratorBenchmark
{
	private readonly Baseline.IMoveGenerator moveGeneratorBaseline = new Baseline.KnightMoveGenerator();
	private readonly Baseline.IMoveGenerator moveGeneratorExperimental = new Experimental.KnightMoveGenerator();

	private const string Fen1 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
	private const string Fen2 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1";

	private const int Times = 1;

	Move[] baselineResult = [];
	Move[] experimentalResult = [];

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
		for (int i = 0; i < Times; i++)
		{
			baselineResult = moveGeneratorBaseline.GenerateMove(Board).ToArray();
		}
	}

	[Benchmark]
	public void ExperimentalMoves()
	{
		for (int i = 0; i < Times; i++)
		{
			experimentalResult = moveGeneratorExperimental.GenerateMove(Board).ToArray();
		}
	}
}
